using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Expedia.Client.Interfaces;
using Expedia.Client.ViewModels;
using Ninject.Modules;

namespace Expedia.Injection
{
    public class ViewModelModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IMainPageViewModel>().To<MainPageViewModel>();
            Bind<ISearchHotelsViewModel>().To<SearchHotelsViewModel>();
            Bind<IHotelResultsViewModel>().To<HotelResultsViewModel>();
            Bind<ISearchFlightsViewModel>().To<SearchFlightsViewModel>();
        }
    }
}
