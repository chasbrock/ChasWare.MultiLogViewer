using ChasWare.MultiLogViewer.Common.Helpers;
using ChasWare.MultiLogViewer.Common.ViewModels.ChasWare.Utils.ViewModels;
using ChasWare.MultiLogViewer.Models;

namespace ChasWare.MultiLogViewer.ViewModels
{
    public class AppDetailsViewModel : BaseViewModel
    {
        #region Constants and fields 

        private AppDetailsModel _model;

        #endregion

        #region Constructors

        public AppDetailsViewModel(AppDetailsModel model)
        {
            Model = model;
        }

        #endregion

        #region public properties

        public string AppName
        {
            get => _model.AppName;
            set => SetField(_model, value);
        }

        public BasicCommand BringToFrontCommand { get; set; }

        public string Format
        {
            get => _model.Format;
            set => SetField(_model, value);
        }

        public bool IsOpen
        {
            get => _model.IsOpen;
            set => SetField(_model, value);
        }

        public string LogPath
        {
            get => _model.LogPath;
            set => SetField(_model, value);
        }

        public AppDetailsModel Model
        {
            get => _model;
            set => SetField(ref _model, value);
        }

        public bool OpenOnStartup
        {
            get => _model.OpenOnStartup;
            set => SetField(_model, value);
        }

        public BasicCommand OpenSettingsCommand { get; set; }
        public BasicCommand ToggleOpenStatusCommand { get; set; }

        #endregion

        #region public methods

        /// <inheritdoc />
        public override string ToString()
        {
            return _model.ToString();
        }

        #endregion
    }
}