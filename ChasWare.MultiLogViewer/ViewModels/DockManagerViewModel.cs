using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ChasWare.MultiLogViewer.Common.ViewModels;
using ChasWare.MultiLogViewer.Common.ViewModels.ChasWare.Utils.ViewModels;

namespace ChasWare.MultiLogViewer.ViewModels
{
    public class DockManagerViewModel : BaseViewModel
    {
        #region Constants and fields 

        private LogViewerViewModel _activeDocument;

        #endregion

        #region Constructors

        public DockManagerViewModel(IEnumerable<BaseDockableViewModel> dockWindowViewModels)
        {
            Files = new ObservableCollection<BaseDockableViewModel>();
            Tools = new ObservableCollection<object>();
            foreach (BaseDockableViewModel document in dockWindowViewModels)
            {
                document.PropertyChanged += DockWindowViewModel_PropertyChanged;
                if (!document.IsClosed)
                {
                    Files.Add(document);
                }
            }
        }

        #endregion

        #region public events, delegates and enums

        public event EventHandler ActiveDocumentChanged;

        #endregion

        #region public properties

        public LogViewerViewModel ActiveDocument
        {
            get => _activeDocument;
            set
            {
                if (_activeDocument != value)
                {
                    _activeDocument = value;
                    FirePropertyChanged();
                    FireActiveDocumentChanged();
                }
            }
        }

        public ObservableCollection<BaseDockableViewModel> Files { get; }

        public ObservableCollection<object> Tools { get; }

        #endregion

        #region other methods

        protected virtual void FireActiveDocumentChanged()
        {
            ActiveDocumentChanged?.Invoke(this, EventArgs.Empty);
        }

        private void DockWindowViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is BaseDockableViewModel document)
            {
                if (e.PropertyName == nameof(BaseDockableViewModel.IsClosed))
                {
                    if (!document.IsClosed)
                    {
                        Files.Add(document);
                    }
                    else
                    {
                        Files.Remove(document);
                    }
                }
            }
        }

        #endregion
    }
}