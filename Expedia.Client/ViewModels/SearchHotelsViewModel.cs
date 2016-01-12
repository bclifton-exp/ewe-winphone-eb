using System;
using System.Collections.Generic;
using System.Text;
using Expedia.Client.Interfaces;
using Expedia.Services.Interfaces;

namespace Expedia.Client.ViewModels
{
    public class SearchHotelsViewModel : ISearchHotelsViewModel
    {
        private IHotelService _hotelService { get; set; }

        public SearchHotelsViewModel(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }
    }
}
