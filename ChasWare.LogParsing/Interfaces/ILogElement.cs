using System;
using ChasWare.LogParsing.Enums;

namespace ChasWare.LogParsing.Interfaces
{
    /// <summary>
    ///     defines record load from log file
    /// </summary>
    public interface ILogElement
    {
        #region public properties

        /// <summary>
        ///     Gets exception text
        /// </summary>
        string Exception { get; }

        /// <summary>
        ///     Gets the level
        /// </summary>
        LoggingLevels Level { get; }

        /// <summary>
        ///     Gets offset in file
        /// </summary>
        long Offset { get; }

        /// <summary>
        ///     Gets timestamp
        /// </summary>
        DateTime? TimeStamp { get; }

        #endregion

        #region indexers

        /// <summary>
        ///     Gets values as string
        /// </summary>
        /// <param name="patternName"></param>
        /// <returns></returns>
        string this[PatternName patternName] { get; }

        #endregion

        #region public methods

        /// <summary>
        ///     has a valid timestamp been set?
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        bool IsValid(string format);

        #endregion
    }
}