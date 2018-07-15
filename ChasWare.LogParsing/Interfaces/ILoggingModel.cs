using System.Collections.Generic;
using ChasWare.LogParsing.Models;

namespace ChasWare.LogParsing.Interfaces
{
    /// <summary>
    ///     model to manage logging levels
    /// </summary>
    public interface ILoggingModel
    {
        #region public properties

        IList<LoggerLevels> Levels { get; }

        #endregion
    }
}