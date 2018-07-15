using System;
using System.Globalization;
using System.Windows.Data;
using ChasWare.MultiLogViewer.ViewModels;

namespace ChasWare.MultiLogViewer.Converters
{
    internal class ActiveDocumentConverter : IValueConverter
    {
        #region public methods

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is LogViewerViewModel ? value : Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is LogViewerViewModel ? value : Binding.DoNothing;
        }

        #endregion
    }
}