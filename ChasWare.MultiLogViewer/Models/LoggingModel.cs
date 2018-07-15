using System.Collections.Generic;
using ChasWare.LogParsing.Enums;
using ChasWare.MultiLogViewer.Interfaces;

namespace ChasWare.MultiLogViewer.Models
{
    public class Log4NetLoggingModel : ILoggingModel
    {
        #region Constructors

        public Log4NetLoggingModel()
        {
            Levels = new List<LoggingLevel>
                {
                    new LoggingLevel(LoggingLevels.Debug, false),
                    new LoggingLevel(LoggingLevels.Info, false),
                    new LoggingLevel(LoggingLevels.Warn, true),
                    new LoggingLevel(LoggingLevels.Error, true),
                    new LoggingLevel(LoggingLevels.Fatal, true)
                };
        }

        #endregion

        #region public properties

        public IList<LoggingLevel> Levels { get; }

        #endregion
    }
}