using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
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



        public SearchHotelsViewModel(IHotelService hotelService,ILocationService locationService) : base(SuggestionLob.HOTELS)
        {
            _hotelService = hotelService;
            _locationService = locationService;
            GetNearbySuggestions();
        }

        
    }
}
