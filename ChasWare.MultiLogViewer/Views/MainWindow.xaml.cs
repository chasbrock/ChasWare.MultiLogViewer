using System.IO;
using System.Windows;
using System.Windows.Input;
using ChasWare.MultiLogViewer.Common.Helpers;
using Xceed.Wpf.AvalonDock.Layout.Serialization;

namespace ChasWare.MultiLogViewer.Views
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constants and fields 

        public const string ConfigFileName = @".\AvalonDock.Layout.config";

        private SimpleCommand _loadLayoutCommand;
        private SimpleCommand _saveLayoutCommand;

        #endregion

        #region Constructors

        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region public properties

        public ICommand LoadLayoutCommand => _loadLayoutCommand ?? (_loadLayoutCommand = new SimpleCommand(OnLoadLayout, CanLoadLayout));
        public ICommand SaveLayoutCommand => _saveLayoutCommand ?? (_saveLayoutCommand = new SimpleCommand(OnSaveLayout));

        #endregion

        #region other methods

        private static bool CanLoadLayout(object parameter)
        {
            return File.Exists(ConfigFileName);
        }

        private void OnLoadLayout(object parameter)
        {
            var layoutSerializer = new XmlLayoutSerializer(DockManager);
            layoutSerializer.Deserialize(ConfigFileName);
        }

        private void OnSaveLayout(object parameter)
        {
            var layoutSerializer = new XmlLayoutSerializer(DockManager);
            layoutSerializer.Serialize(ConfigFileName);
        }

        #endregion
    }
}