using ChasWare.LogParsing.Interfaces;
using ChasWare.MultiLogViewer.Models;

namespace ChasWare.MultiLogViewer.Services
{
    /// <summary>
    ///     logging service, can be overrided for different Logging engines
    /// </summary>
    internal class Log4NetLoggingDetailService : ILoggingDetailService
    {
        #region public methods

        public ILoggingModel GetLoggingModel()
        {
            return new Log4NetLoggingModel();
        }

        #endregion
    }
}