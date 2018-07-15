using System.IO;
using System.Reflection;
using System.Windows;
using Autofac;
using ChasWare.MultiLogViewer.Interfaces;
using ChasWare.MultiLogViewer.Services;
using ChasWare.MultiLogViewer.ViewModels;
using ChasWare.MultiLogViewer.Views;

namespace ChasWare.MultiLogViewer
{
    /// <inheritdoc />
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Constants and fields 

        private readonly IContainer _container;

        #endregion

        #region Constructors

        public App(params string[] args)
        {
            _container = RegisterServices();
            string configFileName = Path.ChangeExtension(Assembly.GetEntryAssembly().GetName().Name, "json");
            if (args.Length > 1)
            {
                configFileName = args[1];
            }

            var applicationDetailsService = _container.Resolve<IAppDetailsService>();
            applicationDetailsService.FileName = configFileName;
        }

        #endregion

        #region other methods

        private static IContainer RegisterServices()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<AppDetailsService>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<Log4NetLoggingDetailService>().AsImplementedInterfaces().SingleInstance();
            return builder.Build();
        }

        private void OnAppStartup(object sender, StartupEventArgs e)
        {
            var view = new MainWindow {DataContext = new MainWindowViewModel(_container)};
            view.Show();
        }

        #endregion
    }
}