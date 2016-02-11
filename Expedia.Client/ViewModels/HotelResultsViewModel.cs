using System;
using System.Collections.Generic;
using System.Text;
using Expedia.Client.Interfaces;
using Expedia.Services.Interfaces;

namespace Expedia.Client.ViewModels
{
    public class HotelResultsViewModel : BaseResultViewModel, IHotelResultsViewModel
    {
        public HotelResultsViewModel(IHotelService hotelService)
        {
            
        }
    }
}
