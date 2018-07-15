using System.Collections.Generic;
using ChasWare.LogParsing.Common;
using ChasWare.LogParsing.Interfaces;
using ChasWare.LogParsing.Models;

namespace ChasWare.LogParsing.Services
{
    /// <inheritdoc />
    public class AppDetailsService : IAppDetailsService
    {
        #region Constants and fields 

        private IList<AppDetailsModel> _details;

        #endregion

        #region public properties

        /// <inheritdoc />
        public string Name { get; set; }

        #endregion

        #region public methods

        /// <inheritdoc />
        public IEnumerable<AppDetailsModel> Load()
        {
            return _details ?? (_details = new JSONSerialiser<AppDetailsModel>(Name).Load());
        }

        /// <inheritdoc />
        public void Save()
        {
            if (_details != null)
            {
                new JSONSerialiser<AppDetailsModel>(Name).Save(_details);
            }
        }

        #endregion
    }
}