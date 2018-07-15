using ChasWare.LogParsing.Models;
using ChasWare.MultiLogViewer.Common.ViewModels;

namespace ChasWare.MultiLogViewer.ViewModels
{
    public class LogViewerViewModel : BaseDockableViewModel
    {
        #region Constants and fields 

        private bool _isActive;

        #endregion

        #region Constructors

        public LogViewerViewModel(AppDetailsModel model) : base(model.AppName)
        {
            Model = model;
            Title = model.AppName;
        }

        #endregion

        #region public properties

        public bool IsActive
        {
            get => _isActive;
            set => SetField(ref _isActive, value);
        }

        public AppDetailsModel Model { get; }

        #endregion
    }
}