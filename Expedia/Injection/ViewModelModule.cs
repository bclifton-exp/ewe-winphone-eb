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
            Bind<IAccountMenuViewModel>().To<AccountMenuViewModel>();
            Bind<ISearchHotelsViewModel>().To<SearchHotelsViewModel>();
            Bind<IHotelResultsViewModel>().To<HotelResultsViewModel>();
            Bind<ISettingsMenuViewModel>().To<SettingsMenuViewModel>();
            Bind<ISearchFlightsViewModel>().To<SearchFlightsViewModel>();
        }
    }
}
