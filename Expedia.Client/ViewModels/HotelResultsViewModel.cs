using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using Expedia.Client.Interfaces;
using Expedia.Client.Utilities;
using Expedia.Entities.Extensions;
using Expedia.Entities.Hotels;
using Expedia.Services.Interfaces;

namespace Expedia.Client.ViewModels
{
    public class HotelResultsViewModel : BaseResultViewModel, IHotelResultsViewModel
    {
        private IHotelService _hotelService { get; set; }

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


        public HotelResultsViewModel(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        public async void GetHotelResults(SearchHotelsLocalParameters searchCriteria)
        {
            HotelResultItems = null;
            var ct = CancellationTokenManager.Instance().CreateAndSetCurrentToken();
            var results = await _hotelService.SearchHotels(ct, searchCriteria);
            HotelResultItems = results.Hotels.ToObservableCollection();
        }
    }
}
