using System.Linq;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Expedia.Client.Interfaces;
using Expedia.Client.ViewModels;
using Expedia.Entities.Entities;
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

        //private void Map_OnMapElementClick(MapControl sender, MapElementClickEventArgs args)
        //{
        //    var selectedPushPin = args.MapElements.FirstOrDefault(x => x is MapIcon) as MapIcon;
        //    var context = DataContext as HotelResultsViewModel;
        //    context.PushPinSelected(selectedPushPin, ResultListView);
        //}

        private void ImageBrush_OnImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            var brush = sender as ImageBrush;
            var source = brush.ImageSource as BitmapImage;

            if (source != null)
            {
                var context = DataContext as HotelResultsViewModel;
                context.ReplaceHotelImageUrl(source.UriSource.AbsoluteUri);
            }
        }

        private void MainHotelGrid_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            var grid = sender as Grid;
            var hotel = grid.DataContext as HotelResultItem;
            var context = DataContext as HotelResultsViewModel;
            context.BookHotel.Execute(hotel);
        }

        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            var context = DataContext as HotelResultsViewModel;
            context.FilterResults();
        }

        private void ToggleButton_OnUnchecked(object sender, RoutedEventArgs e)
        {
            var context = DataContext as HotelResultsViewModel;
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

            var context = DataContext as HotelResultsViewModel;
            context.CloseHotelFlyouts();
        }

        private void Pin_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            var context = DataContext as HotelResultsViewModel;
            var canvas = sender as Canvas;
            var stackPanel = canvas?.Children.First() as StackPanel;
            var image = stackPanel?.Children.First() as Image;
            var mapIcon = image?.DataContext as MapPushPin;

            context.SetHotelFlyout(mapIcon);
        }
    }
}
