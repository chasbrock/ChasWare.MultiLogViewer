using System;
using ChasWare.LogParsing.Enums;
using ChasWare.LogParsing.Interfaces;

namespace ChasWare.LogParsing.Services
{
    /// <inheritdoc />
    public class Pattern : IPattern
    {
        #region Constructors

        internal Pattern(PatternName patternName, string format, int length)
        {
            PatternName = patternName;
            Format = format;
            Length = length;
        }

        #endregion

        #region public properties

        public string Format { get; }

        public int Length { get; }

        /// <inheritdoc />
        public PatternName PatternName { get; }

        #endregion

        #region public methods

        public string ReadFixedLength(string line, ref int offset)
        {
            if (offset >= line.Length)
            {
                return null;
            }

            int length = line.Length < offset + Length ? Length - offset : Length;
            if (length < 1)
            {
                return null;
            }

            string value = line.Substring(offset, length).Trim();
            offset += length;
            return value;
        }

        public string ReadToNextPadding(string line, ref int offset, Pattern nextPattern)
        {
            if (offset >= line.Length)
            {
                return null;
            }

            int length;
            if (nextPattern != null)
            {
                length = line.IndexOf(nextPattern.Format, offset, StringComparison.Ordinal) - offset;
            }
            else
            {
                length = line.Length - offset;
            }

            if (length < 1)
            {
                return null;
            }

            string value = line.Substring(offset, length).Trim();
            offset += length;
            return value;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return PatternName.ToString();
        }

        #endregion
    }
}