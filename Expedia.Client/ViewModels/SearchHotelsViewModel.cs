using System;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.Devices.Geolocation;
using Expedia.Client.Extensions;
//using Bing.Maps;
using Expedia.Client.Interfaces;
using Expedia.Client.Utilities;
using Expedia.Client.Views;
using Expedia.Entities.Entities;
using Expedia.Entities.Hotels;
using Expedia.Entities.Suggestions;
using Expedia.Services.Interfaces;
using GalaSoft.MvvmLight.Command;

namespace Expedia.Client.ViewModels
{
    public class SearchHotelsViewModel : BaseSearchViewModel, ISearchHotelsViewModel
    {
        private IHotelService _hotelService { get; set; }
        private ILocationService _locationService { get; set; }

        private RelayCommand _searchHotels;
        public RelayCommand SearchHotels
        {
            get { return _searchHotels; }
            set
            {
                _searchHotels = value;
                OnPropertyChanged("SearchHotels");
            }
        }

        public SearchHotelsViewModel(IHotelService hotelService,ILocationService locationService) : base(SuggestionLob.HOTELS)
        {
            _hotelService = hotelService;
            _locationService = locationService;
            GetNearbySuggestions();
            SearchHotels = new DependentRelayCommand(ExecuteHotelSearch, CanExecuteSearch, this, () => SelectedSearchSuggestion);
        }

        private void ExecuteHotelSearch()
        {
            var childrenAges = ChildAges.Select(c => c.Age).ToArray();
            double latitude;
            double.TryParse(SelectedSearchSuggestion.Coordinates.Latitude, out latitude);
            double longitude;
            double.TryParse(SelectedSearchSuggestion.Coordinates.Longitude, out longitude);

            var hotelSearchParams = new SearchHotelsLocalParameters
            {
                LocationName = SelectedSearchSuggestion.RegionNames.ShortName,
                LocationRegionId = SelectedSearchSuggestion.Id,
                NearestAirportCode = SelectedSearchSuggestion.HierarchyInfo.Airport?.AirportCode,
                LocationLatitude = latitude,
                LocationLongitude = longitude,
                CheckInDate = StartDate,
                CheckOutDate = EndDate,
                AdultsCount = AdultCount,
                ChildrenAges = childrenAges,
                RoomsCount = 1 //this is always one until we have a control to pick this value
            };

            if(MapControl != null)
                hotelSearchParams.SelectedMapCenter = MapControl.Center;

            if (SelectedSearchSuggestion.HotelId != null)
            {
                var hotel = new HotelResultItem { HotelId = SelectedSearchSuggestion.HotelId };
                DirectHotelSearch(hotel, hotelSearchParams);
            }

            //if (!double.IsNaN(latitude) && !double.IsNaN(longitude))
            //{
            //    hotelSearchParams.LocationLatitude = latitude;
            //    hotelSearchParams.LocationLongitude = longitude;
            //}

            SearchParamsMemory.Instance().HotelParams = hotelSearchParams;
            Navigator.Instance().NavigateForward(SuggestionLob.HOTELS, typeof(HotelResultsView), hotelSearchParams);
        }

        internal void SetExistingTripSearchDetails()
        {
            var existingParams = SearchParamsMemory.Instance().HotelParams;

            if (existingParams != null)
            {
                StartDate = existingParams.CheckInDate.DateTime;
                EndDate = existingParams.CheckOutDate.DateTime;
                AdultCount = existingParams.AdultsCount;
                var childAges = new ObservableCollection<ChildAgeItem>();
                for (int i = 0; i < existingParams.ChildrenAges.Length; i++)
                {
                    childAges.Add(new ChildAgeItem(i) { Age = existingParams.ChildrenAges[i] });
                }
                ChildCount = childAges.Count;
                ChildAges = childAges;
            }
        }

        private bool CanExecuteSearch()
        {
            return SelectedSearchSuggestion != null; //TODO valid date stuff too
        }


        private void DirectHotelSearch(HotelResultItem hotel, SearchHotelsLocalParameters hotelSearchParams)
        {
            //TODO this was previously a deeplink to a webview.
        }
    }
}
