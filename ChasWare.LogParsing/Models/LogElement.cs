using System;
using System.Collections.Generic;
using ChasWare.LogParsing.Enums;
using ChasWare.LogParsing.Interfaces;

namespace ChasWare.LogParsing.Models
{
    /// <inheritdoc />
    public class LogElement : ILogElement
    {
        #region Constants and fields 

        private readonly Dictionary<PatternName, string> _values = new Dictionary<PatternName, string>();

        #endregion

        #region public properties

        /// <inheritdoc />
        public string Exception { get; internal set; }

        /// <inheritdoc />
        public LoggingLevels Level { get; internal set; }

        /// <inheritdoc />
        public long Offset { get; internal set; }

        /// <inheritdoc />
        public DateTime? TimeStamp { get; internal set; }

        #endregion

        #region indexers

        /// <inheritdoc />
        public string this[PatternName patternName]
        {
            get
            {
                switch (patternName)
                {
                    case PatternName.Exception:
                        return Exception;
                    case PatternName.Level:
                        return Level.ToString();
                    case PatternName.TimeStamp:
                        return TimeStamp?.ToString();
                    default:
                        return _values.TryGetValue(patternName, out string value) ? value : null;
                }
            }
        }

        #endregion

        #region public methods

        public bool IsValid(string format)
        {
            return TimeStamp != null;
        }

        #endregion

        #region other methods

        internal void AppendValue(PatternName patternName, string value)
        {
            if (_values.TryGetValue(patternName, out string existing))
            {
                _values[patternName] = $"{existing}{Environment.NewLine}{value}";
            }
            else
            {
                _values[patternName] = value;
            }
        }

        internal void SetValue(PatternName patternName, string value)
        {
            _values[patternName] = value;
        }

        #endregion
    }
}