using System;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.Devices.Geolocation;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls.Maps;
using Expedia.Client.Interfaces;
using Expedia.Client.Utilities;
using Expedia.Entities.Suggestions;
using Expedia.Services.Interfaces;
using GalaSoft.MvvmLight.Command;

namespace Expedia.Client.ViewModels
{
    public class SearchFlightsViewModel : BaseSearchViewModel, ISearchFlightsViewModel
    {
        private IFlightService _flightService { get; set; }
        private ILocationService _locationService { get; set; }

        private RelayCommand _searchFlights;
        public RelayCommand SearchFlights
        {
            get { return _searchFlights; }
            set
            {
                _searchFlights = value;
                OnPropertyChanged("SearchFlights");
            }
        }


        public SearchFlightsViewModel(IFlightService flightService, ILocationService locationService) : base(SuggestionLob.FLIGHTS)
        {
            _flightService = flightService;
            _locationService = locationService;
            GetNearbySuggestions();
            GetNearbySuggestions2();
        }

        internal void BuildDeparturePushPin(SuggestionResult airportSuggestion)
        {
            if (MapControl != null)
            {
                var mapElement = MapControl.MapElements.FirstOrDefault(element => ((dynamic) element).MapTabIndex == 0);
                if (mapElement != null)
                {
                    var indexToRemove = MapControl.MapElements.IndexOf(mapElement);
                    MapControl.MapElements.RemoveAt(indexToRemove);
                }

                var mapIcon = new MapIcon
                {
                    Title = airportSuggestion.Display,
                    Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/yellow_pin.png")),
                    MapTabIndex = 0
                };

                var geoPoint = new BasicGeoposition
                {
                    Latitude = double.Parse(airportSuggestion.Coordinates.Latitude),
                    Longitude = double.Parse(airportSuggestion.Coordinates.Longitude)
                };

                mapIcon.Location = new Geopoint(geoPoint);
                MapControl.MapElements.Add(mapIcon);
            }
        }

        internal void BuildArrivalPushPin(SuggestionResult airportSuggestion)
        {
            if (MapControl != null)
            {
                var mapElement = MapControl.MapElements.FirstOrDefault(element => ((dynamic)element).MapTabIndex == 1);
                if (mapElement != null)
                {
                    var indexToRemove = MapControl.MapElements.IndexOf(mapElement);
                    MapControl.MapElements.RemoveAt(indexToRemove);
                }

                var mapIcon = new MapIcon
                {
                    Title = airportSuggestion.Display,
                    Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/yellow_pin.png")),
                    MapTabIndex = 1
                };

                var geoPoint = new BasicGeoposition
                {
                    Latitude = double.Parse(airportSuggestion.Coordinates.Latitude),
                    Longitude = double.Parse(airportSuggestion.Coordinates.Longitude)
                };

                mapIcon.Location = new Geopoint(geoPoint);
                MapControl.MapElements.Add(mapIcon);
            }
        }

        internal void SetExistingTripSearchDetails() //TODO flight params on back nav
        {
            var existingParams = SearchParamsMemory.Instance().FlightParams;

            if (existingParams != null)
            {
                //StartDate = existingParams.CheckInDate.DateTime;
                //EndDate = existingParams.CheckOutDate.DateTime;
                //AdultCount = existingParams.AdultsCount;
                //var childAges = new ObservableCollection<ChildAgeItem>();
                //for (int i = 0; i < existingParams.ChildrenAges.Length; i++)
                //{
                //    childAges.Add(new ChildAgeItem(i) { Age = existingParams.ChildrenAges[i] });
                //}
                //ChildCount = childAges.Count;
                //ChildAges = childAges;
            }
        }
    }
}
