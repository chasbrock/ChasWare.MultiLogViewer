using System.Collections.Generic;
using ChasWare.MultiLogViewer.Common.Helpers;
using ChasWare.MultiLogViewer.Interfaces;
using ChasWare.MultiLogViewer.Models;

namespace ChasWare.MultiLogViewer.Services
{
    /// <inheritdoc />
    internal class AppDetailsService : IAppDetailsService
    {
        #region Constants and fields 

        private IList<AppDetailsModel> _details;

        #endregion

        #region public properties

        /// <inheritdoc />
        public string FileName { get; set; }

        #endregion

        #region public methods

        /// <inheritdoc />
        public IEnumerable<AppDetailsModel> Load()
        {
            return _details ?? (_details = new JSONSerialiser<AppDetailsModel>(FileName).Load());
        }

        /// <inheritdoc />
        public void Save()
        {
            if (_details != null)
            {
                new JSONSerialiser<AppDetailsModel>(FileName).Save(_details);
            }
        }

        #endregion
    }
}