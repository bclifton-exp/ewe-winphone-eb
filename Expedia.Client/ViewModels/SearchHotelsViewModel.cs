using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Windows.Input;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
//using Bing.Maps;
using Expedia.Client.Interfaces;
using Expedia.Client.Utilities;
using Expedia.Entities.Hotels;
using Expedia.Entities.Suggestions;
using Expedia.Services.Interfaces;
using Microsoft.Practices.Prism.Commands;

namespace Expedia.Client.ViewModels
{
    public class SearchHotelsViewModel : BaseViewModel, ISearchHotelsViewModel
    {
        private IHotelService _hotelService { get; set; }
        private ILocationService _locationService { get; set; }

        private ICommand _searchHotels;
        public ICommand SearchHotels
        {
            get { return _searchHotels; }
            set
            {
                _searchHotels = value;
                OnPropertyChanged("SearchHotels");
            }
        }

        private Geoposition _mapCenter;
        public Geoposition MapCenter
        {
            get { return _mapCenter; }
            set
            {
                _mapCenter = value;
                OnPropertyChanged("MapCenter");
            }
        }


        public SearchHotelsViewModel(IHotelService hotelService,ILocationService locationService) : base(SuggestionLob.HOTELS)
        {
            _hotelService = hotelService;
            _locationService = locationService;
            GetNearbySuggestions();
            MapCenter = GeoLocationMemory.Instance().GetCurrentGeoposition();
            SearchHotels = new DelegateCommand(ExecuteHotelSearch);
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
        }

        private void DirectHotelSearch(HotelResultItem hotel, SearchHotelsLocalParameters hotelSearchParams)
        {
            //TODO this was previously a deeplink to a webview.
        }
    }
}
