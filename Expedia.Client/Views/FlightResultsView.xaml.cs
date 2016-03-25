using Windows.Devices.Geolocation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Navigation;
using Expedia.Client.Interfaces;
using Expedia.Client.ViewModels;
using Expedia.Entities.Flights;
using Expedia.Injection;


namespace Expedia.Client.Views
{
    public sealed partial class FlightResultsView : Page
    {
        public FlightResultsView()
        {
            this.DataContext = ExpediaKernel.Instance().Get<IFlightResultsViewModel>();
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var flightParams = e.Parameter as SearchFlightsLocalParameters;
            var context = DataContext as FlightResultsViewModel;
            if (flightParams.SelectedMapCenter != null)
            {
                var geoPoint = new BasicGeoposition
                {
                    Latitude = flightParams.SelectedMapCenter.Position.Latitude,
                    Longitude = flightParams.SelectedMapCenter.Position.Longitude
                };
                context.MapCenter = new Geopoint(geoPoint);
            }
            context.GetFlightResults(flightParams);
        }

        private void Map_OnLoading(FrameworkElement sender, object args)
        {
            var context = DataContext as FlightResultsViewModel;
            var map = sender as MapControl;
            context.MapControl = map;
            if (context.MapCenter != null)
            {
                var geoPoint = new BasicGeoposition
                {
                    Latitude = context.MapCenter.Position.Latitude,
                    Longitude = context.MapCenter.Position.Longitude
                };
                map.Center = new Geopoint(geoPoint);
            }
            else
            {
                map.Center = new Geopoint(new BasicGeoposition());
            }
        }
    }
}
