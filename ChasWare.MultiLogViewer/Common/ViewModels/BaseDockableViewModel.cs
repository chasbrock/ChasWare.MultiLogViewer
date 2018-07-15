using System.Windows.Input;
using ChasWare.MultiLogViewer.Common.Helpers;
using ChasWare.MultiLogViewer.Common.ViewModels.ChasWare.Utils.ViewModels;

namespace ChasWare.MultiLogViewer.Common.ViewModels
{
    public class BaseDockableViewModel : BaseViewModel
    {
        #region Constants and fields 

        private bool _canClose;
        private ICommand _closeCommand;
        private bool _isClosed;
        private bool _isVisible;
        private string _title;

        #endregion

        #region Constructors

        public BaseDockableViewModel(string name)
        {
            Name = name;
            Title = name;
            CanClose = true;
            IsClosed = false;
        }

        #endregion

        #region public properties

        public bool CanClose
        {
            get => _canClose;
            set => SetField(ref _canClose, value);
        }

        public ICommand CloseCommand
        {
            get { return _closeCommand ?? (_closeCommand = new SimpleCommand(call => Close())); }
        }

        public bool IsClosed
        {
            get => _isClosed;
            set => SetField(ref _isClosed, value);
        }

        public bool IsVisible
        {
            get => _isVisible;
            set => SetField(ref _isVisible, value);
        }

        public string Name { get; }

        public string Title
        {
            get => _title;
            set => SetField(ref _title, value);
        }

        #endregion

        #region public methods

        public void Close()
        {
            IsClosed = true;
        }

        #endregion
    }
}