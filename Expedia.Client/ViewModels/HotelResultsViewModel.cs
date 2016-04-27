using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Expedia.Client.Interfaces;
using Expedia.Client.Utilities;
using Expedia.Client.Views;
using Expedia.Entites;
using Expedia.Entities.Extensions;
using Expedia.Entities.Hotels;
using Expedia.Entities.Suggestions;
using Expedia.Services.Interfaces;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.Prism.Commands;

namespace Expedia.Client.ViewModels
{
    public class HotelResultsViewModel : BaseResultViewModel, IHotelResultsViewModel
    {
        #region Properties
        private IHotelService _hotelService { get; set; }
        private ISettingsService _settingsService { get; set; }
        private IPointOfSaleService _pointOfSaleService { get; set; }
        private int SelectedPinCount;

        private ObservableCollection<HotelResultItem> _hotelResultItems;
        public ObservableCollection<HotelResultItem> HotelResultItems
        {
            get { return _hotelResultItems; }
            set
            {
                _hotelResultItems = value;
                OnPropertyChanged("HotelResultItems");
            }
        }

        private HotelResultItem _selectedHotel;
        public HotelResultItem SelectedHotel
        {
            get { return _selectedHotel; }
            set
            {
                _selectedHotel = value;
                OnPropertyChanged("SelectedHotel");
            }
        }

        private SearchHotelsLocalParameters _currentSearchCriteria;
        public SearchHotelsLocalParameters CurrentSearchCriteria
        {
            get { return _currentSearchCriteria; }
            set
            {
                _currentSearchCriteria = value;
                OnPropertyChanged("CurrentSearchCriteria");
            }
        }

        private RelayCommand<HotelResultItem> _bookHotel;
        public RelayCommand<HotelResultItem> BookHotel
        {
            get { return _bookHotel; }
            set
            {
                _bookHotel = value;
                OnPropertyChanged("BookHotel");
            }
        }

        private SearchHotelsLocalParameters _searchInput;
        public SearchHotelsLocalParameters SearchInput
        {
            get { return _searchInput; }
            set
            {
                _searchInput = value;
                OnPropertyChanged("SearchInput");
            }
        }

        private bool _sortByPriceLowToHighChecked;
        public bool SortByPriceLowToHighChecked
        {
            get { return _sortByPriceLowToHighChecked; }
            set
            {
                _sortByPriceLowToHighChecked = value;
                OnPropertyChanged("SortByPriceLowToHighChecked");
            }
        }

        private bool _sortByGuestStarRatingChecked;
        public bool SortByGuestStarRatingChecked
        {
            get { return _sortByGuestStarRatingChecked; }
            set
            {
                _sortByGuestStarRatingChecked = value;
                OnPropertyChanged("SortByGuestStarRatingChecked");
            }
        }

        private bool _sortByStarRatingChecked;
        public bool SortByStarRatingChecked
        {
            get { return _sortByStarRatingChecked; }
            set
            {
                _sortByStarRatingChecked = value;
                OnPropertyChanged("SortByStarRatingChecked");
            }
        }

        private bool _sortByBestDealsChecked;
        public bool SortByBestDealsChecked
        {
            get { return _sortByBestDealsChecked; }
            set
            {
                _sortByBestDealsChecked = value;
                OnPropertyChanged("SortByBestDealsChecked");
            }
        }

        private bool _sortByMostPopularChecked;
        public bool SortByMostPopularChecked
        {
            get { return _sortByMostPopularChecked; }
            set
            {
                _sortByMostPopularChecked = value;
                OnPropertyChanged("SortByMostPopularChecked");
            }
        }

        private ICommand _sortResultsCommand;
        public ICommand SortResultsCommand
        {
            get { return _sortResultsCommand; }
            set
            {
                _sortResultsCommand = value;
                OnPropertyChanged("SortResultsCommand");
            }
        }

        private HotelStarRatingFilter[] _starRatingFilters;
        public HotelStarRatingFilter[] StarRatingFilters
        {
            get { return _starRatingFilters; }
            set
            {
                _starRatingFilters = value;
                OnPropertyChanged("StarRatingFilters");
            }
        }

        private HotelPriceBucketFilter[] _priceFilters;
        public HotelPriceBucketFilter[] PriceFilters
        {
            get { return _priceFilters; }
            set
            {
                _priceFilters = value;
                OnPropertyChanged("PriceFilters");
            }
        }

        private HotelNeighborhoodFilter[] _neighborhoodFilters;
        public HotelNeighborhoodFilter[] NeighborhoodFilters
        {
            get { return _neighborhoodFilters; }
            set
            {
                _neighborhoodFilters = value;
                OnPropertyChanged("NeighborhoodFilters");
            }
        }

        private HotelFilter[] _amenityFilters;
        public HotelFilter[] AmenityFilters
        {
            get { return _amenityFilters; }
            set
            {
                _amenityFilters = value;
                OnPropertyChanged("AmenityFilters");
            }
        }

        private HotelFilter[] _accessibilityFilters;
        public HotelFilter[] AccessibilityFilters
        {
            get { return _accessibilityFilters; }
            set
            {
                _accessibilityFilters = value;
                OnPropertyChanged("AccessibilityFilters");
            }
        }

        private Geopoint _selectedPinLocation;
        public Geopoint SelectedPinLocation
        {
            get { return _selectedPinLocation; }
            set
            {
                _selectedPinLocation = value;
                OnPropertyChanged("SelectedPinLocation");
            }
        }

        private bool _isPinSelected;
        public bool IsPinSelected
        {
            get { return _isPinSelected; }
            set
            {
                _isPinSelected = value;
                OnPropertyChanged("IsPinSelected");
            }
        }

        private ICommand _hotelFlyoutCommand;
        public ICommand HotelFlyoutCommand
        {
            get { return _hotelFlyoutCommand; }
            set
            {
                _hotelFlyoutCommand = value;
                OnPropertyChanged("HotelFlyoutCommand");
            }
        }

        #endregion

        public HotelResultsViewModel(IHotelService hotelService, ISettingsService settingsService, IPointOfSaleService pointOfSaleService)
        {
            _hotelService = hotelService;
            _settingsService = settingsService;
            _pointOfSaleService = pointOfSaleService;
        }

        public async void GetHotelResults(SearchHotelsLocalParameters searchCriteria)
        {
            SearchInput = searchCriteria;
            HotelResultItems = null;
            ClearPushPins();
            CurrentSearchCriteria = searchCriteria;
            var ct = CancellationTokenManager.Instance().CreateAndSetCurrentToken();
            var results = await _hotelService.SearchHotels(ct, searchCriteria);
            StarRatingFilters = results.StarRatingFilters;
            PriceFilters = results.PriceFilters;
            NeighborhoodFilters = results.NeighborhoodFilters;
            AmenityFilters = results.AmenityFilters;
            AccessibilityFilters = results.AccessibilityFilters;
            ResultsCount = results.Hotels.Count();
            HotelResultItems = results.Hotels.ToObservableCollection();

            BuildPushpins(HotelResultItems);

            BookHotel = new RelayCommand<HotelResultItem>(BuildAndNavigateToHotelUri);
            SortResultsCommand = new DelegateCommand(SortResults);
            HotelFlyoutCommand = new DelegateCommand(() =>BuildAndNavigateToHotelUri(SelectedHotel)); //TODO TEMP until hotel details page
        }

        private async void BuildAndNavigateToHotelUri(HotelResultItem hotel) //Gone after Native - Web View Connector
        {
            var hostname = _settingsService.GetCurrentDomain();
            var childrenString = CurrentSearchCriteria.ChildrenAges.Length > 0
                ? CurrentSearchCriteria.ChildrenAges.Select(_ => ":c{0}".InvariantCultureFormat(_)).JoinBy(String.Empty)
                : string.Empty;
            var pos = await _pointOfSaleService.GetCurrentPointOfSale();

            var hotelDeeplink = new Uri("https://www.{0}/h{1}.Hotel-Information?chkin={2}&chkout={3}&rm1=a{4}{5}&forceNoRedir=1&brandcid=App.WindowsUWP.Native"
                .InvariantCultureFormat(
                    hostname,
                    hotel.HotelId,
                    CurrentSearchCriteria.CheckInDate
                        .ToString(pos.DateFormatHotel, DateTimeFormatInfo.InvariantInfo),
                    CurrentSearchCriteria.CheckOutDate
                        .ToString(pos.DateFormatHotel, DateTimeFormatInfo.InvariantInfo),
                    CurrentSearchCriteria.AdultsCount,
                    childrenString));

            Navigator.Instance().NavigateForward(SuggestionLob.HOTELS, typeof(HotelBookingWebView), hotelDeeplink);
        }

        internal async void FilterResults()
        {
            try
            {
                HotelResultItems = null;
                CurrentSearchCriteria.StarRatingFilters = StarRatingFilters;
                CurrentSearchCriteria.AmenityFilters = AmenityFilters;
                CurrentSearchCriteria.AccessibilityFilters = AccessibilityFilters;
                CurrentSearchCriteria.PriceFilters = PriceFilters;
                CurrentSearchCriteria.NeighborhoodFilters = NeighborhoodFilters;

                var filteredRegion = CurrentSearchCriteria.NeighborhoodFilters.FirstOrDefault(n => n.IsFilterChecked);
                if (filteredRegion != null)
                    CurrentSearchCriteria.LocationRegionId = filteredRegion.Id;

                ClearPushPins();
                var ct = CancellationTokenManager.Instance().CreateAndSetCurrentToken();
                var filteredResults = await _hotelService.SearchHotels(ct, CurrentSearchCriteria);
                HotelResultItems = filteredResults.Hotels.ToObservableCollection();
                BuildPushpins(HotelResultItems);

                StarRatingFilters = filteredResults.StarRatingFilters;
                PriceFilters = filteredResults.PriceFilters;
                NeighborhoodFilters = filteredResults.NeighborhoodFilters;
                AmenityFilters = filteredResults.AmenityFilters;
                AccessibilityFilters = filteredResults.AccessibilityFilters;
                ResultsCount = filteredResults.Hotels.Count();
            }
            catch (Exception ex)
            {
                //eating exceptions for when filters are clicked too quickly
            }
        }

        internal void SortResults()
        {
            if (SortByBestDealsChecked)
            {
                GetSortedHotelResults(SortHotelsByType.MobileDeals);
            }

            if (SortByGuestStarRatingChecked)
            {
                GetSortedHotelResults(SortHotelsByType.GuestStarRatingDesc);
            }

            if (SortByMostPopularChecked)
            {
                GetSortedHotelResults(SortHotelsByType.ExpertPicks);
            }

            if (SortByPriceLowToHighChecked)
            {
                GetSortedHotelResults(SortHotelsByType.PriceAsc);
            }

            if (SortByStarRatingChecked)
            {
                GetSortedHotelResults(SortHotelsByType.StarRatingDesc);
            }
        }

        private async void GetSortedHotelResults(SortHotelsByType sortType)
        {
            HotelResultItems = null;
            CurrentSearchCriteria.SortBy = sortType;
            var ct = CancellationTokenManager.Instance().CreateAndSetCurrentToken();
            var sortedResults = await _hotelService.SearchHotels(ct, CurrentSearchCriteria);
            HotelResultItems = sortedResults.Hotels.ToObservableCollection();
        }

        internal async void ReplaceHotelImageUrl(string badUrl)//Workaround for bad hotel images from the API
        {
            try
            {
                if (HotelResultItems != null)
                {
                    var badHotel = HotelResultItems.First(h => h.ImageUrl == badUrl);

                    HotelResultItems.Remove(badHotel);

                    var hotelInfo = await _hotelService.GetHotelInformation(new CancellationToken(false), badHotel.HotelId);

                    if (hotelInfo.Photos.Any())
                    {
                        var goodUrl = Constants.HotelImagesUrl + hotelInfo.Photos.First().Url;

                        badHotel.ImageUrl = goodUrl;
                        HotelResultItems.Add(badHotel);
                    }
                }
            }
            catch (Exception ex)
            {
                //eating exceptions for when filters are clicked too quickly
            }
        }

        internal void PushPinSelected(MapIcon selectedPushPin, ListView resultListView)
        {
            SelectedPinLocation = selectedPushPin.Location;
            SelectedHotel = HotelResultItems.First(h=>h.Latitude == selectedPushPin.Location.Position.Latitude && h.Longitude == selectedPushPin.Location.Position.Longitude);
            IsPinSelected = true;
        }

        private void ClearPushPins()
        {
            MapControl?.MapElements.Clear();
            SelectedPinCount = 0;
            IsPinSelected = false;
        }

        private void BuildPushpins(ObservableCollection<HotelResultItem> hotelResultItems)
        {
            if (MapControl != null)
            {
                foreach (var hotel in hotelResultItems)
                {
                    var mapIcon = new MapIcon
                    {
                        Title = "$" + hotel.Price, //TODO PoS based currency symbol
                        Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/blue_pushpin.png")),
                        NormalizedAnchorPoint = new Point(0.5, 1),
                        ZIndex = 0
                    };

                    var geoPoint = new BasicGeoposition
                    {
                        Latitude = hotel.Latitude,
                        Longitude = hotel.Longitude
                    };
                    mapIcon.Location = new Geopoint(geoPoint);

                    MapControl.MapElements.Add(mapIcon);
                }
            }
        }
    }
}
