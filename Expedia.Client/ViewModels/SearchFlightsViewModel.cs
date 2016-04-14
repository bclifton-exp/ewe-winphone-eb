using System;
using System.Linq;
using System.Reflection;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Media;
using Expedia.Client.Extensions;
using Expedia.Client.Interfaces;
using Expedia.Client.Utilities;
using Expedia.Client.Views;
using Expedia.Entities.Flights;
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

        private BasicGeoposition _startPoint;
        public BasicGeoposition StartPoint
        {
            get { return _startPoint; }
            set
            {
                _startPoint = value;
                DrawFlightLine();
                OnPropertyChanged("StartPoint");
            }
        }

        private BasicGeoposition _endPoint;
        public BasicGeoposition EndPoint
        {
            get { return _endPoint; }
            set
            {
                _endPoint = value;
                DrawFlightLine();
                OnPropertyChanged("EndPoint");
            }
        }

        private bool _isRoundTrip;
        public bool IsRoundTrip
        {
            get { return _isRoundTrip; }
            set
            {
                _isRoundTrip = value;
                OnPropertyChanged("IsRoundTrip");
            }
        }

        private bool _isOneWay;
        public bool IsOneWay
        {
            get { return _isOneWay; }
            set
            {
                _isOneWay = value;
                OnPropertyChanged("IsOneWay");
            }
        }

        public SearchFlightsViewModel(IFlightService flightService, ILocationService locationService) : base(SuggestionLob.FLIGHTS)
        {
            _flightService = flightService;
            _locationService = locationService;
            IsRoundTrip = true;
            GetNearbySuggestions();
            GetNearbySuggestions2();
            SearchFlights = new DependentRelayCommand(ExecuteFlightSearch, CanExecuteSearch, this, () => SelectedSearchSuggestion, () => SelectedSearchSuggestion2);
        }

        private void ExecuteFlightSearch()
        {
            var childrenAges = ChildAges.Select(c => c.Age).ToArray();

            var flightSearchParams = new SearchFlightsLocalParameters
            {
                DepartureDate = StartDate,
                DepartureAirportCode = SelectedSearchSuggestion.HierarchyInfo.Airport.AirportCode,
                DepartureCityShortName = SelectedSearchSuggestion.RegionNames.ShortName,
                ArrivalAirportCode = SelectedSearchSuggestion2.HierarchyInfo.Airport.AirportCode,
                ArrivalCityShortName = SelectedSearchSuggestion2.RegionNames.ShortName,
                AdultsCount = AdultCount,
                ChildrenAges = childrenAges,
                IsOneWay = IsOneWay,
                DepartureAirportPosition = StartPoint,
                ArrivalAirportPosition = EndPoint
            };

            if (!IsOneWay)
            {
                flightSearchParams.ReturnDate = EndDate;
            }

            if (MapControl != null)
                flightSearchParams.SelectedMapCenter = MapControl.Center;

            SearchParamsMemory.Instance().FlightParams = flightSearchParams;
            Navigator.Instance().NavigateForward(SuggestionLob.FLIGHTS, typeof(FlightResultsView), flightSearchParams);
        }

        private bool CanExecuteSearch()
        {
            if (IsRoundTrip)
            {
                var tripOkay = SelectedSearchSuggestion != null && SelectedSearchSuggestion2 != null && SelectedSearchSuggestion.HierarchyInfo.Airport.AirportCode != SelectedSearchSuggestion2.HierarchyInfo.Airport.AirportCode;
                return tripOkay;
            }
            return SelectedSearchSuggestion != null;

            //TODO valid date stuff too
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
                    Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/blue_pin.png")),
                    MapTabIndex = 0,
                    NormalizedAnchorPoint = new Point(0.5,0.9),
                    ZIndex = 1
                };

                var geoPoint = new BasicGeoposition
                {
                    Latitude = double.Parse(airportSuggestion.Coordinates.Latitude),
                    Longitude = double.Parse(airportSuggestion.Coordinates.Longitude)
                };

                StartPoint = geoPoint;

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
                    Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/blue_pin.png")),
                    MapTabIndex = 1,
                    NormalizedAnchorPoint = new Point(0.5,0.9),
                    ZIndex = 1
                };

                var geoPoint = new BasicGeoposition
                {
                    Latitude = double.Parse(airportSuggestion.Coordinates.Latitude),
                    Longitude = double.Parse(airportSuggestion.Coordinates.Longitude)
                };

                EndPoint = geoPoint;

                mapIcon.Location = new Geopoint(geoPoint);
                MapControl.MapElements.Add(mapIcon);
            }
        }

        internal void DrawFlightLine()
        {
            if (StartPoint.Latitude != 0 && EndPoint.Latitude != 0)
            {
                var mapLine = MapControl.MapElements.FirstOrDefault(element => element.GetType().GetProperty("StrokeDashed") != null);
                if (mapLine != null)
                {
                    var indexToRemove = MapControl.MapElements.IndexOf(mapLine);
                    MapControl.MapElements.RemoveAt(indexToRemove);
                }

                if (StartPoint.Latitude == EndPoint.Latitude && StartPoint.Longitude == EndPoint.Longitude)
                    return;
                
                var geodesic = Geodesic.ToGeodesic(StartPoint, EndPoint);
                var midPoint = Geodesic.MidPoint(StartPoint, EndPoint);

                var line = new MapPolyline {Path = new Geopath(geodesic)};
                var brush = Application.Current.Resources["LogoYellowBrush"] as SolidColorBrush;
                line.StrokeColor = brush.Color;
                line.StrokeThickness = 5;
                line.StrokeDashed = true;
                line.ZIndex = 0;

                MapControl.Center = new Geopoint(midPoint);
                MapControl.MapElements.Add(line);
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
