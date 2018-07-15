using System.Collections.Generic;
using System.Linq;
using Autofac;
using ChasWare.MultiLogViewer.Common.ViewModels;
using ChasWare.MultiLogViewer.Common.ViewModels.ChasWare.Utils.ViewModels;
using ChasWare.MultiLogViewer.Interfaces;

namespace ChasWare.MultiLogViewer.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Constructors

        public MainWindowViewModel(IComponentContext context)
        {
            ActiveApps = new ActiveAppsViewModel(context);
            ToolBar = new ToolBarViewModel(context);
            IEnumerable<LogViewerViewModel> apps = context.Resolve<IAppDetailsService>().Load().Select(ad => new LogViewerViewModel(ad));
            DockManager = new DockManagerViewModel(apps);
        }

        public MainWindowViewModel()
        {
            ActiveApps = new ActiveAppsViewModel();
            ToolBar = new ToolBarViewModel();
            DockManager = new DockManagerViewModel(new List<BaseDockableViewModel> {new BaseDockableViewModel("Dummy")});
        }

        #endregion

        #region public properties

        /// <summary>
        ///     Gets ViewModel to manage App Details
        /// </summary>
        public ActiveAppsViewModel ActiveApps { get; }

        /// <summary>
        ///     ViewModel to provide docking functionality
        /// </summary>
        public DockManagerViewModel DockManager { get; }

        /// <summary>
        ///     Gets ViewModel to manage App MainMenu
        /// </summary>
        public ToolBarViewModel ToolBar { get; }

        #endregion
    }
}