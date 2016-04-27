using Expedia.Client.Interfaces;
using Expedia.Client.ViewModels;
using Ninject.Modules;

namespace Expedia.Injection
{
    public class ViewModelModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISignInViewModel>().To<SignInViewModel>();
            Bind<IMainPageViewModel>().To<MainPageViewModel>();
            Bind<ISearchCarsViewModel>().To<SearchCarsViewModel>();
            Bind<ICarResultsViewModel>().To<CarResultsViewModel>();
            Bind<ISearchHotelsViewModel>().To<SearchHotelsViewModel>();
            Bind<IHotelResultsViewModel>().To<HotelResultsViewModel>();
            Bind<ISettingsMenuViewModel>().To<SettingsMenuViewModel>();
            Bind<IHotelDetailsViewModel>().To<HotelDetailsViewModel>();
            Bind<IFlightResultsViewModel>().To<FlightResultsViewModel>();
            Bind<ISearchFlightsViewModel>().To<SearchFlightsViewModel>();
            Bind<ILinkFacebookAccountViewModel>().To<LinkFacebookAccountViewModel>();
        }
    }
}
