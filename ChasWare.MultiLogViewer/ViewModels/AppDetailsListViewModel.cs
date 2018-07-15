using System.Collections.Generic;
using System.Linq;
using Autofac;
using ChasWare.MultiLogViewer.Common.ViewModels.ChasWare.Utils.ViewModels;
using ChasWare.MultiLogViewer.Interfaces;

namespace ChasWare.MultiLogViewer.ViewModels
{
    public class AppDetailsListViewModel : BaseViewModel
    {
        #region Constructors

        public AppDetailsListViewModel()
        {
            Items = new List<AppDetailsViewModel>();
        }

        public AppDetailsListViewModel(IComponentContext context)
        {
            Items = context.Resolve<IAppDetailsService>().Load().Select(ad => new AppDetailsViewModel(ad)).ToList();
        }

        #endregion

        #region public properties

        public List<AppDetailsViewModel> Items { get; }

        #endregion
    }
}