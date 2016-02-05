using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Windows.Input;
using Windows.Devices.Geolocation;
//using Bing.Maps;
using Expedia.Client.Interfaces;
using Expedia.Client.Utilities;
using Expedia.Entities.Suggestions;
using Expedia.Services.Interfaces;

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
        }
    }
}
