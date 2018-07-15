using System.Collections.Generic;
using ChasWare.LogParsing.Models;

namespace ChasWare.LogParsing.Interfaces
{
    /// <summary>
    ///     service to load app details from storage
    /// </summary>
    public interface IAppDetailsService
    {
        #region public properties

        /// <summary>
        ///     Gets or sets filename where app details are stored
        /// </summary>
        string Name { get; set; }

        #endregion

        #region public methods

        /// <summary>
        ///     loads the data (if not already loaded)
        /// </summary>
        /// <returns>list of app details</returns>
        IEnumerable<AppDetailsModel> Load();

        /// <summary>
        ///     save current list to file
        /// </summary>
        void Save();

        #endregion
    }
}