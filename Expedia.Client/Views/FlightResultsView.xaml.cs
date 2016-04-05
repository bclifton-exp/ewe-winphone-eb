using Windows.Devices.Geolocation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Expedia.Client.Interfaces;
using Expedia.Client.ViewModels;
using Expedia.Entities.Flights;
using Expedia.Entities.Hotels;
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
            context.BuildDeparturePushPin(context.CurrentSearchCriteria);
        }

        private void MainHotelGrid_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            var grid = sender as Grid;
            var flight = grid.DataContext as FlightResultItem;
            var context = DataContext as FlightResultsViewModel;
            context.BookFlight.Execute(flight);
        }

        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            var context = DataContext as FlightResultsViewModel;
            context.FilterResults();
        }

        private void ToggleButton_OnUnchecked(object sender, RoutedEventArgs e)
        {
            var context = DataContext as FlightResultsViewModel;
            context.FilterResults();
        }

        private void UIElement_OnLostFocus(object sender, RoutedEventArgs e)
        {
            FilterDropDown.IsExpanded = false;
            SortDropDown.IsExpanded = false;
            FilterDropDownPhone.IsExpanded = false;
            SortDropDownPhone.IsExpanded = false;
        }

        private void UIElement_OnLostFocusPhone(object sender, RoutedEventArgs e)
        {
            FilterDropDownPhone.IsExpanded = false;
            SortDropDownPhone.IsExpanded = false;
            FilterDropDown.IsExpanded = false;
            SortDropDown.IsExpanded = false;
        }

        private void Map_OnMapTapped(MapControl sender, MapInputEventArgs args)
        {
            FilterDropDown.IsExpanded = false;
            SortDropDown.IsExpanded = false;
            FilterDropDownPhone.IsExpanded = false;
            SortDropDownPhone.IsExpanded = false;
        }

        private void ResultListView_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            FilterDropDown.IsExpanded = false;
            SortDropDown.IsExpanded = false;
            FilterDropDownPhone.IsExpanded = false;
            SortDropDownPhone.IsExpanded = false;
        }
    }
}
