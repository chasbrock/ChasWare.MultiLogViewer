using ChasWare.LogParsing.Enums;

namespace ChasWare.LogParsing.Models
{
    /// <summary>
    ///     encapsulates logging level
    /// </summary>
    public class LoggerLevels
    {
        #region Constructors

        public LoggerLevels(LoggingLevels level, bool enabled)
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