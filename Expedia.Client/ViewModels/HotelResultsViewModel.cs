using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using Windows.Devices.Geolocation;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls.Maps;
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

            ClearPushPins();
            BuildPushpins(HotelResultItems);
        }

        private void ClearPushPins()
        {
            MapControl.MapElements.Clear();
        }

        private void BuildPushpins(ObservableCollection<HotelResultItem> hotelResultItems)
        {
            foreach (var hotel in hotelResultItems)
            {
                var mapIcon = new MapIcon
                {
                    Title = hotel.HotelName + "(" + "$" + hotel.Price + ")",
                    Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/pushpin_scaled.png"))
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

        private void SetPushPinFocus(HotelResultItem selectedHotel)
        {
            if (selectedHotel != null)
            {
                var geoPoint = new BasicGeoposition
                {
                    Latitude = selectedHotel.Latitude,
                    Longitude = selectedHotel.Longitude
                };
                MapControl.Center = new Geopoint(geoPoint);
            }
        }
    }
}
