namespace ChasWare.MultiLogViewer.Interfaces
{
    /// <summary>
    ///     service to create logging models. Implement for each loggong package
    ///     (ie log4net etc.)
    /// </summary>
    internal interface ILoggingDetailService
    {
        #region public methods

        ILoggingModel GetLoggingModel();

        #endregion
    }
}