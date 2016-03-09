using System.Linq;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Navigation;
using Expedia.Client.Interfaces;
using Expedia.Client.ViewModels;
using Expedia.Entities.Hotels;
using Expedia.Injection;

namespace Expedia.Client.Views
{
    public sealed partial class HotelResultsView : Page
    {
        public HotelResultsView()
        {
            this.DataContext = ExpediaKernel.Instance().Get<IHotelResultsViewModel>();
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var hotelParams = e.Parameter as SearchHotelsLocalParameters;
            var context = DataContext as HotelResultsViewModel;
            if (hotelParams.SelectedMapCenter != null)
            {
                var geoPoint = new BasicGeoposition
                {
                    Latitude = hotelParams.SelectedMapCenter.Position.Latitude,
                    Longitude = hotelParams.SelectedMapCenter.Position.Longitude
                };
                context.MapCenter = new Geopoint(geoPoint);
            }
            context.GetHotelResults(hotelParams);
        }

        private void Map_OnLoading(FrameworkElement sender, object args)
        {
            var context = DataContext as HotelResultsViewModel;
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

        private void Map_OnMapElementClick(MapControl sender, MapElementClickEventArgs args)
        {
            var selectedPushPin = args.MapElements.FirstOrDefault(x => x is MapIcon) as MapIcon;
            var context = DataContext as HotelResultsViewModel;
            context.PushPinSelected(selectedPushPin, ResultListView);
        }
    }
}
