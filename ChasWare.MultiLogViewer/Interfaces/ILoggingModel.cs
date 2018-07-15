using System.Collections.Generic;
using ChasWare.LogParsing.Enums;

namespace ChasWare.MultiLogViewer.Interfaces
{
    /// <summary>
    ///     model to manage logging levels
    /// </summary>
    public interface ILoggingModel
    {
        #region public properties

        IList<LoggingLevel> Levels { get; }

        #endregion
    }

    /// <summary>
    ///     encapsulates logging level
    /// </summary>
    public class LoggingLevel
    {
        #region Constructors

        public LoggingLevel(LoggingLevels level, bool enabled)
        {
            Level = level;
            Enabled = enabled;
        }

        #endregion

        #region public properties

        /// <summary>
        ///     Gets or sets flag indicating whether we are interested in seeing this level
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        ///     Gets name of logging level
        /// </summary>
        public LoggingLevels Level { get; }

        #endregion
    }
}