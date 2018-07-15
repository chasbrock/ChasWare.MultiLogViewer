using System.Collections.Generic;
using ChasWare.LogParsing.Enums;
using ChasWare.LogParsing.Interfaces;
using ChasWare.LogParsing.Models;

namespace ChasWare.MultiLogViewer.Models
{
    public class Log4NetLoggingModel : ILoggingModel
    {
        #region Constructors

        public Log4NetLoggingModel()
        {
            Levels = new List<LoggerLevels>
                {
                    new LoggerLevels(LoggingLevels.Debug, false),
                    new LoggerLevels(LoggingLevels.Info, false),
                    new LoggerLevels(LoggingLevels.Warn, true),
                    new LoggerLevels(LoggingLevels.Error, true),
                    new LoggerLevels(LoggingLevels.Fatal, true)
                };
        }

        #endregion

        #region public properties

        public IList<LoggerLevels> Levels { get; }

        #endregion
    }
}