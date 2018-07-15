using System.Collections.Generic;
using System.Collections.ObjectModel;
using Autofac;
using ChasWare.MultiLogViewer.Common.Helpers;
using ChasWare.MultiLogViewer.Common.ViewModels;

namespace ChasWare.MultiLogViewer.ViewModels
{
    public class ActiveAppsViewModel : BaseDockableViewModel
    {
        #region Constants and fields 

        private readonly AppDetailsListViewModel _model;

        #endregion

        #region Constructors

        public ActiveAppsViewModel(IComponentContext context) : this(new AppDetailsListViewModel(context))
        {
        }

        public ActiveAppsViewModel() : this(new AppDetailsListViewModel())
        {
        }

        private ActiveAppsViewModel(AppDetailsListViewModel model) : base("Apps")
        {
            _model = model;
            CanClose = false;
            Title = string.Empty;
        }

        #endregion

        #region public properties

        /// <summary>
        ///     Gets or sets command to add new app
        /// </summary>
        public BasicCommand AddAppCommand { get; set; }

        /// <summary>
        ///     Gets list of all app details
        /// </summary>
        public IEnumerable<AppDetailsViewModel> AppDetails => new ObservableCollection<AppDetailsViewModel>(_model.Items);

        #endregion
    }
}