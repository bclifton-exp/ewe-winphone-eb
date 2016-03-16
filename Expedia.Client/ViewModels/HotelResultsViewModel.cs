using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Input;
using Windows.Devices.Geolocation;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
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
        private IHotelService _hotelService { get; set; }
        private ISettingsService _settingsService { get; set; }
        private IPointOfSaleService _pointOfSaleService { get; set; }

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
                SetPushPinFocus(_selectedHotel);
                OnPropertyChanged("SelectedHotel");
            }
        }

        private Geopoint _mapCenter;
        public Geopoint MapCenter
        {
            get { return _mapCenter; }
            set
            {
                _mapCenter = value;
                OnPropertyChanged("MapCenter");
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
            HotelResultItems = results.Hotels.ToObservableCollection();

            BuildPushpins(HotelResultItems);

            BookHotel = new RelayCommand<HotelResultItem>(BuildAndNavigateToHotelUri);
            SortResultsCommand = new DelegateCommand(SortResults);
        }

        private async void BuildAndNavigateToHotelUri(HotelResultItem hotel) //Gone after Native - Web View Connector
        {
            var hostname = _settingsService.GetCurrentDomain();
            var childrenString = CurrentSearchCriteria.ChildrenAges.Length > 0
                ? CurrentSearchCriteria.ChildrenAges.Select(_ => ":c{0}".InvariantCultureFormat(_)).JoinBy(String.Empty)
                : string.Empty;
            var pos = await _pointOfSaleService.GetCurrentPointOfSale();

            var hotelDeeplink = new Uri("https://www.{0}/h{1}.Hotel-Information?chkin={2}&chkout={3}&rm1=a{4}{5}&forceNoRedir=1&brandcid=App.Windows.Native"
                .InvariantCultureFormat(
                    hostname,
                    hotel.HotelId,
                    CurrentSearchCriteria.CheckInDate
                        .ToString(pos.DateFormatHotel, DateTimeFormatInfo.InvariantInfo),
                    CurrentSearchCriteria.CheckOutDate
                        .ToString(pos.DateFormatHotel, DateTimeFormatInfo.InvariantInfo),
                    CurrentSearchCriteria.AdultsCount,
                    childrenString));

            Navigator.Instance().NavigateForward(SuggestionLob.HOTELS,typeof(HotelBookingWebView),hotelDeeplink);
        }

        internal void FilterResults()
        {
            //TODO
        }

        internal void SortResults()
        {
            //TODO
        }

        internal async void ReplaceHotelImageUrl(string badUrl)//Workaround for bad hotel images from the API
        {
            var badHotel = HotelResultItems.First(h => h.ImageUrl == badUrl);

            HotelResultItems.Remove(badHotel);

            var hotelInfo = await _hotelService.GetHotelInformation(new CancellationToken(false), badHotel.HotelId);

            var goodUrl = Constants.HotelImagesUrl + hotelInfo.Photos.First().Url;

            badHotel.ImageUrl = goodUrl;
            HotelResultItems.Add(badHotel);
        }

        internal void PushPinSelected(MapIcon selectedPushPin, ListView resultListView)
        {
            selectedPushPin.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/blue_pin.png"));

            SelectedHotel = HotelResultItems.First(h => selectedPushPin.Title.Contains(h.HotelName));
            resultListView.ScrollIntoView(SelectedHotel, ScrollIntoViewAlignment.Leading);
        }

        private void ClearPushPins()
        {
            MapControl?.MapElements.Clear();
        }

        private void BuildPushpins(ObservableCollection<HotelResultItem> hotelResultItems)
        {
            if (MapControl != null)
            {
                foreach (var hotel in hotelResultItems)
                {
                    var mapIcon = new MapIcon
                    {
                        Title = hotel.HotelName + "(" + "$" + hotel.Price + ")", //TODO PoS based currency symbol
                        Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/yellow_pin.png")),
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

        private void SetPushPinFocus(HotelResultItem selectedHotel)
        {
            if (selectedHotel != null && MapControl != null)
            {
                var geoPoint = new BasicGeoposition
                {
                    Latitude = selectedHotel.Latitude,
                    Longitude = selectedHotel.Longitude
                };
                MapControl.Center = new Geopoint(geoPoint);

                foreach (MapElement selected in MapControl.MapElements.Where(selected => ((dynamic) selected).Title.Contains(selectedHotel.HotelName)))
                {
                    ((dynamic) selected).Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/blue_pin.png"));
                }
            }
        }
    }
}
