using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace ChasWare.MultiLogViewer.Views
{
    /// <summary>
    ///     Interaction logic for LogFileControlView.xaml
    /// </summary>
    public partial class AppDetailsControlView : UserControl
    {
        #region Constructors

        public AppDetailsControlView()
        {
            DataContextChanged += meh;
            InitializeComponent();
        }

        #endregion

        #region other methods

        private void meh(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            Debug.WriteLine(dependencyPropertyChangedEventArgs.NewValue);
        }

        #endregion
    }
}