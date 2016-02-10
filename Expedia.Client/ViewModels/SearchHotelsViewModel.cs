using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Windows.Input;
using Windows.Devices.Geolocation;
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
            var check1 = StartDate;
            var check2 = EndDate;

            var check3 = AdultCount;
            var check4 = ChildCount;

            var check5 = SelectedSearchSuggestion;

            var check6 = SelectedSearchSuggestion.HotelId;

            var check7 = ChildAges;

            var hotelSearchParams = new SearchHotelsLocalParameters
            {
                //LocationName = locationName,
                //LocationRegionId = regionId,
                //NearestAirportCode = nearestAirportCode,
                //CheckInDate = checkInDate,
                //CheckOutDate = checkOutDate,
                //AdultsCount = adultsCount,
                //ChildrenAges = childrenAges,
                //RoomsCount = roomsCount //this is always one until we have a control to pick
            };
        }
    }
}
