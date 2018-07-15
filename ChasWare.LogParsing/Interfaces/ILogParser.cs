using System.Collections.Generic;
using ChasWare.LogParsing.Models;

namespace ChasWare.LogParsing.Interfaces
{
    /// <inheritdoc />
    /// <summary>
    ///     defines records that will be extracted from log file
    /// </summary>
    public interface ILogParser : IEnumerable<LogElement>
    {
        #region public properties

        /// <summary>
        ///     position in relation to start of stream
        /// </summary>
        long Offset { get; }

        #endregion

        #region public methods

        /// <summary>
        ///     close log file
        /// </summary>
        void Close();

        /// <summary>
        ///     open log file ready for reading
        /// </summary>
        /// <param name="logFilter">provides details to use  </param>
        void Open(LogFilter logFilter);

        #endregion
    }
}