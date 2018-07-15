using System;
using System.Collections.Generic;
using ChasWare.LogParsing.Enums;

namespace ChasWare.LogParsing.Models
{
    /// <summary>
    ///     aggregation of params for log filtering
    /// </summary>
    public class LogFilter
    {
        #region Constructors

        /// <summary>
        ///     creates new instance
        /// </summary>
        /// <param name="appDetails">details of app </param>
        public LogFilter(AppDetailsModel appDetails)
        {
            AppDetails = appDetails;
        }

        #endregion

        #region public properties

        /// <summary>
        ///     Gets details of app
        /// </summary>
        public AppDetailsModel AppDetails { get; }

        /// <summary>
        ///     Gets or sets option string to look for
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        ///     Gets or sets optional list of LogLevels to filter by
        /// </summary>
        public IEnumerable<LoggingLevels> LogLevels { get; set; }

        /// <summary>
        ///     Gets or sets end of optional time window to look at
        /// </summary>
        public DateTime? WindowEnd { get; set; }

        /// <summary>
        ///     Gets or sets start of optional time window to look at
        /// </summary>
        public DateTime? WindowStart { get; set; }

        #endregion
    }
}