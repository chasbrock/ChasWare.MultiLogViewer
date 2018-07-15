using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using ChasWare.LogParsing.Enums;
using ChasWare.LogParsing.Interfaces;
using ChasWare.LogParsing.Models;

namespace ChasWare.LogParsing.Services
{
    /// <inheritdoc cref="ILogParser" />
    /// <summary>
    ///     implementation od ILogParser suitable for Log4Net
    /// </summary>
    public class Log4NetParser : ILogParser, IDisposable
    {
        #region Constants and fields 

        private const string Iso8061Date = "yyyy-MM-dd HH:mm:ss,fff";
        private readonly List<Pattern> _patterns = new List<Pattern>();
        private LogElement _currentElement;
        private int _currentOffset;
        private string _dateFormat = Iso8061Date;
        private LogFilter _logFilter;
        private LogElement _priorElement;
        private bool _readingException;
        private StreamReader _stream;

        #endregion

        #region public properties

        /// <inheritdoc />
        public long Offset { get; private set; }

        #endregion

        #region public methods

        public void Close()
        {
            _stream?.Dispose();
            _stream = null;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Close();
        }

        /// <inheritdoc />
        public IEnumerator<LogElement> GetEnumerator()
        {
            while (_stream != null && !_stream.EndOfStream)
            {
                string line;
                while ((line = _stream.ReadLine()) != null)
                {
                    if (string.IsNullOrEmpty(line))
                    {
                        continue;
                    }

                    _currentOffset = 0;
                    _currentElement = new LogElement();
                    if (ReadElement(line))
                    {
                        // we can publish prior record now as we have finished
                        // reading exception strings (if any)
                        if (_priorElement != null)
                        {
                            Offset += _currentOffset;
                            yield return _priorElement;
                        }

                        _priorElement = _currentElement;
                    }
                }

                // publish last record
                if (_currentElement?.IsValid(_dateFormat) ?? false)
                {
                    yield return _currentElement;
                }
            }
        }

        /// <inheritdoc />
        public void Open(LogFilter logFilter)
        {
            if (_logFilter != logFilter)
            {
                _logFilter = logFilter;
                DecodePattern(logFilter.AppDetails.Pattern);
            }

            _stream = GetStream();
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region other methods

        private void AddPattern(string patternName, string format)
        {
            var length = 0;
            if (patternName.StartsWith("-"))
            {
                var limit = new StringBuilder();
                var i = 1;
                while (i < patternName.Length)
                {
                    if (!char.IsDigit(patternName[i]))
                    {
                        break;
                    }

                    limit.Append(patternName[i++]);
                }

                patternName = patternName.Substring(i);
                length = int.Parse(limit.ToString());
            }

            switch (patternName)
            {
                case "a":
                case "appdomain":
                    _patterns.Add(new Pattern(PatternName.AppDomain, format, length));
                    break;

                case "C":
                case "class":
                case "type":
                    _patterns.Add(new Pattern(PatternName.TypeName, format, length));
                    break;

                case "d":
                case "date":
                case "utcdate":
                    _dateFormat = string.IsNullOrWhiteSpace(format) ? Iso8061Date : format;
                    length = DateTime.Now.ToString(_dateFormat).Length;
                    _patterns.Add(new Pattern(PatternName.TimeStamp, format, length));
                    break;

                case "m":
                case "message":
                    _patterns.Add(new Pattern(PatternName.Message, format, length));
                    break;
                case "l":
                case "location":
                    _patterns.Add(new Pattern(PatternName.Location, format, length));
                    break;
                case "L":
                case "line":
                    _patterns.Add(new Pattern(PatternName.Line, format, length));
                    break;
                case "M":
                case "method":
                    _patterns.Add(new Pattern(PatternName.Method, format, length));
                    break;
                case "n":
                case "newline":
                    _patterns.Add(new Pattern(PatternName.NewLine, format, length));
                    break;
                case "t":
                case "thread":
                    _patterns.Add(new Pattern(PatternName.Thread, format, length));
                    break;
                case "stacktrace":
                case "stacktracedetail":
                    _patterns.Add(new Pattern(PatternName.StackTrace, format, length));
                    break;
                case "P":
                case "level":
                    _patterns.Add(new Pattern(PatternName.Level, format, length));
                    break;
            }
        }

        private void DecodePattern(string pattern)
        {
            var readingPattern = false;
            var readingFormat = false;
            var readingPadding = false;
            var patternName = new StringBuilder();
            var format = new StringBuilder();
            foreach (char c in pattern)
            {
                switch (c)
                {
                    case ' ':
                    case ']':
                        if (readingPattern)
                        {
                            Apply();
                        }

                        readingPadding = true;
                        patternName.Append(c);
                        continue;
                    case '%':
                        if (readingPattern || readingPadding)
                        {
                            Apply();
                        }

                        readingPattern = true;
                        break;
                    case '{':
                        if (readingPattern)
                        {
                            readingFormat = true;
                        }
                        else
                        {
                            patternName.Append(c);
                            readingPadding = true;
                        }

                        break;
                    case '}':
                        if (readingFormat)
                        {
                            Apply();
                        }
                        else
                        {
                            patternName.Append(c);
                            readingPadding = true;
                        }

                        break;

                    default:
                        if (readingFormat)
                        {
                            format.Append(c);
                            break;
                        }

                        patternName.Append(c);
                        if (!readingPattern)
                        {
                            readingPadding = true;
                        }

                        break;
                }

                void Apply()
                {
                    if (readingPattern)
                    {
                        AddPattern(patternName.ToString(), format.ToString());
                    }
                    else if (readingPadding)
                    {
                        _patterns.Add(new Pattern(PatternName.Padding, patternName.ToString(), patternName.Length));
                    }

                    readingPattern = false;
                    readingPadding = false;
                    readingFormat = false;
                    patternName = new StringBuilder();
                    format = new StringBuilder();
                }
            }
        }

        private StreamReader GetStream()
        {
            if (_stream == null)
            {
                try
                {
                    var fileStream = new FileStream(_logFilter.AppDetails.LogPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    _stream = new StreamReader(fileStream, Encoding.ASCII);
                }
                catch
                {
                    _stream = null;
                }
            }

            return _stream;
        }

        private bool ReadElement(string line)
        {
            for (var i = 0; i < _patterns.Count; i++)
            {
                Pattern pattern = _patterns[i];
                string value;
                switch (pattern.PatternName)
                {
                    case PatternName.Padding:
                        _currentOffset += pattern.Length;
                        break;

                    case PatternName.NewLine:
                        line = _stream.ReadLine();
                        _currentOffset++;
                        break;

                    case PatternName.Level:
                        value = ReadField(line, pattern, i);
                        _currentElement.Level = Log4NetLevelParser.Parse(value);

                        // are we interested in this level
                        if (!_logFilter.LogLevels.Contains(_currentElement.Level))
                        {
                            return false;
                        }

                        break;

                    case PatternName.TimeStamp:
                        int localOffset = _currentOffset;
                        value = pattern.ReadFixedLength(line, ref localOffset);

                        // check to see if we have a valid timestamp
                        if (!DateTime.TryParseExact(value, _dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime timeStamp))
                        {
                            _readingException = true;
                            _priorElement?.AppendValue(PatternName.Exception, line);
                            return false;
                        }
                        else if (_readingException)
                        {
                            // we have finished reading exception
                            // _priorElement != null should never be the case 
                            if (_priorElement != null)
                            {
                                _priorElement.Offset += _priorElement.Exception?.Length ?? 0;
                            }
                        }

                        // is this element within time windows
                        if (_logFilter.WindowEnd < timeStamp || _logFilter.WindowStart > timeStamp)
                        {
                            return false;
                        }

                        _readingException = false;
                        _currentElement.TimeStamp = timeStamp;
                        _currentOffset = localOffset;

                        break;

                    default:
                        value = ReadField(line, pattern, i);
                        _currentElement.SetValue(pattern.PatternName, value);
                        break;
                }
            }

            return true;
        }

        private string ReadField(string line, Pattern pattern, int patternIndex)
        {
            if (pattern.Length == 0)
            {
                Pattern next = patternIndex < _patterns.Count - 1 ? _patterns[patternIndex + 1] : null;
                return pattern.ReadToNextPadding(line, ref _currentOffset, next);
            }

            return pattern.ReadFixedLength(line, ref _currentOffset);
        }

        #endregion
    }
}