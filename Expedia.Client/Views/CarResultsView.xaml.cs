using System.Linq;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Expedia.Client.Interfaces;
using Expedia.Client.ViewModels;
using Expedia.Entities.Cars;
using Expedia.Injection;

namespace Expedia.Client.Views
{
    public sealed partial class CarResultsView : Page
    {
        public CarResultsView()
        {
            this.DataContext = ExpediaKernel.Instance().Get<ICarResultsViewModel>();
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var carParams = e.Parameter as SearchCarsLocalParameters;
            var context = DataContext as CarResultsViewModel;
            if (carParams.SelectedMapCenter != null)
            {
                var geoPoint = new BasicGeoposition
                {
                    Latitude = carParams.SelectedMapCenter.Position.Latitude,
                    Longitude = carParams.SelectedMapCenter.Position.Longitude
                };
                context.MapCenter = new Geopoint(geoPoint);
            }
            context.CarResults = null;
            context.CarDetailResults = null;
            context.GetCarCategoryResults(carParams);
        }

        private void Map_OnLoading(FrameworkElement sender, object args)
        {
            var context = DataContext as CarResultsViewModel;
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
            var context = DataContext as CarResultsViewModel;
            context.PushPinSelected(selectedPushPin, ResultListView);
        }

        //private void ImageBrush_OnImageFailed(object sender, ExceptionRoutedEventArgs e)
        //{
        //    var brush = sender as ImageBrush;
        //    var source = brush.ImageSource as BitmapImage;

        //    var context = DataContext as HotelResultsViewModel;
        //    context.ReplaceHotelImageUrl(source.UriSource.AbsoluteUri);
        //}

        //private void MainHotelGrid_OnTapped(object sender, TappedRoutedEventArgs e)
        //{
        //    var grid = sender as Grid;
        //    var hotel = grid.DataContext as HotelResultItem;
        //    var context = DataContext as HotelResultsViewModel;
        //    context.BookHotel.Execute(hotel);
        //}

        //private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        //{
        //    var context = DataContext as CarResultsViewModel;
        //    context.FilterResults();
        //}

        //private void ToggleButton_OnUnchecked(object sender, RoutedEventArgs e)
        //{
        //    var context = DataContext as CarResultsViewModel;
        //    context.FilterResults();
        //}

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

        private void ResultListView_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            FilterDropDown.IsExpanded = false;
            SortDropDown.IsExpanded = false;
            FilterDropDownPhone.IsExpanded = false;
            SortDropDownPhone.IsExpanded = false;
        }

        private void Map_OnMapTapped(MapControl sender, MapInputEventArgs args)
        {
            FilterDropDown.IsExpanded = false;
            SortDropDown.IsExpanded = false;
            FilterDropDownPhone.IsExpanded = false;
            SortDropDownPhone.IsExpanded = false;
        }

        private void ResultListViewDetail_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            var vm = DataContext as CarResultsViewModel;
            vm.CloseOpenOffers();
            vm.SelectedCar.IsOfferExpanded = true;
        }

        private void UIElement_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            var vm = DataContext as CarResultsViewModel;
            vm.CloseOpenOffers(sender);
        }

        private void MapImage_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            var vm = this.DataContext as CarResultsViewModel;
            vm.LaunchMap();
        }

        private void Image_OnImageOpened(object sender, RoutedEventArgs e)
        {
            var vm = this.DataContext as CarResultsViewModel;
            vm.SelectedMapLoaded();
        }
    }
}
