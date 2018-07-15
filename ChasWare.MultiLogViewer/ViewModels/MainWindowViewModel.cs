using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Autofac;
using ChasWare.LogParsing.Interfaces;
using ChasWare.MultiLogViewer.Common.Helpers;
using ChasWare.MultiLogViewer.Common.ViewModels;
using ChasWare.MultiLogViewer.Common.ViewModels.ChasWare.Utils.ViewModels;
using Xceed.Wpf.AvalonDock;
using Xceed.Wpf.AvalonDock.Layout.Serialization;

namespace ChasWare.MultiLogViewer.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Constants and fields 

        public TypedCommand<DockingManager> DeserializeDockManager { get; } = new TypedCommand<DockingManager>(dm =>
            {
                string fileName = GetLayoutFilePath();
                if (File.Exists(fileName))
                {
                    var layoutSerializer = new XmlLayoutSerializer(dm);
                    using (var reader = new StreamReader(fileName))
                    {
                        layoutSerializer.Deserialize(reader);
                    }
                }
            }
        );

        public TypedCommand<DockingManager> SerializeDockManager { get; } = new TypedCommand<DockingManager>(dm =>
            {
                var layoutSerializer = new XmlLayoutSerializer(dm);
                using (var writer = new StreamWriter(GetLayoutFilePath()))
                {
                    layoutSerializer.Serialize(writer);
                }
            }
        );

        #endregion

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

        #region other methods

        private static string GetLayoutFilePath()
        {
            return Path.ChangeExtension(Assembly.GetEntryAssembly().GetName().Name, "layout");
        }

        #endregion
    }
}