using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Media;
using Expedia.Client.Interfaces;
using Expedia.Client.Utilities;
using Expedia.Entities.Extensions;
using Expedia.Entities.Flights;
using Expedia.Services.Interfaces;

namespace Expedia.Client.ViewModels
{
    public class FlightResultsViewModel : BaseResultViewModel, IFlightResultsViewModel
    {
        private IFlightService _flightService;
        private ISettingsService _settingsService;
        private IPointOfSaleService _pointOfSaleService;
        private ISuggestionService _suggestionService;

        private SearchFlightsLocalParameters _searchInput;
        public SearchFlightsLocalParameters SearchInput
        {
            get { return _searchInput; }
            set
            {
                _searchInput = value;
                OnPropertyChanged("SearchInput");
            }
        }

        private SearchFlightsLocalParameters _currentSearchCriteria;
        public SearchFlightsLocalParameters CurrentSearchCriteria
        {
            get { return _currentSearchCriteria; }
            set
            {
                _currentSearchCriteria = value;
                OnPropertyChanged("CurrentSearchCriteria");
            }
        }

        private ObservableCollection<FlightResultItem> _flightResultItems;
        public ObservableCollection<FlightResultItem> FlightResultItems
        {
            get { return _flightResultItems; }
            set
            {
                _flightResultItems = value;
                OnPropertyChanged("FlightResultItems");
            }
        }

        private string _destinationPictureUrl;
        public string DestinationPictureUrl
        {
            get { return _destinationPictureUrl; }
            set
            {
                _destinationPictureUrl = value;
                OnPropertyChanged("DestinationPictureUrl");
            }
        }

        private string _returnName;
        public string ReturnName
        {
            get { return _returnName; }
            set
            {
                _returnName = value;
                OnPropertyChanged("ReturnName");
            }
        }

        private string _destinationName;
        public string DestinationName
        {
            get { return _destinationName; }
            set
            {
                _destinationName = value;
                OnPropertyChanged("DestinationName");
            }
        }

        private FlightResultItem _selectedDeparture;
        public FlightResultItem SelectedDeparture
        {
            get { return _selectedDeparture; }
            set
            {
                _selectedDeparture = value;
                OnPropertyChanged("SelectedDeparture");
            }
        }

        private FlightResultItem _selectedArrival;
        public FlightResultItem SelectedArrival
        {
            get { return _selectedArrival; }
            set
            {
                _selectedArrival = value;
                OnPropertyChanged("SelectedArrival");
            }
        }

        private FlightResultItem _selectedFlight;
        public FlightResultItem SelectedFlight
        {
            get { return _selectedFlight; }
            set
            {
                _selectedFlight = value;
                GetAirportLegCoordinates(value);
                OnPropertyChanged("SelectedFlight");
            }
        }

        //public FlightFilter[] AirlineFilters { get; set; }
        //public FlightFilter[] StopCountFilters { get; set; }


        public FlightResultsViewModel(IFlightService flightService, ISettingsService settingsService,
            IPointOfSaleService pointOfSaleService, ISuggestionService suggestionService)
        {
            _flightService = flightService;
            _settingsService = settingsService;
            _suggestionService = suggestionService;
            _pointOfSaleService = pointOfSaleService;
        }

        public async void GetFlightResults(SearchFlightsLocalParameters searchCriteria)
        {
            SearchInput = searchCriteria;
            FlightResultItems = null;
            
            CurrentSearchCriteria = searchCriteria;
            var ct = CancellationTokenManager.Instance().CreateAndSetCurrentToken();
            var results = await _flightService.SearchFlights(ct, searchCriteria);
            //StarRatingFilters = results.StarRatingFilters;
            //PriceFilters = results.PriceFilters;
            //NeighborhoodFilters = results.NeighborhoodFilters;
            //AmenityFilters = results.AmenityFilters;
            //AccessibilityFilters = results.AccessibilityFilters;
            //ResultsCount = results.Hotels.Count();
            FlightResultItems = results.Flights.ToObservableCollection();
            DestinationPictureUrl = results.Selection.DestinationPictureUrl;
            DestinationName = results.Selection.DestinationName;
            ReturnName = results.Selection.ReturnName;

            

            //BookHotel = new RelayCommand<HotelResultItem>(BuildAndNavigateToHotelUri);
            //SortResultsCommand = new DelegateCommand(SortResults);
        }

        internal async void GetAirportLegCoordinates(FlightResultItem SelectedFlight)
        {
            var ct = CancellationTokenManager.Instance().CreateAndSetCurrentToken();
            var legGeoPoints = new List<BasicGeoposition> {SearchInput.DepartureAirportPosition};
            var legAiportCodes = new List<string> {SearchInput.DepartureAirportCode};

            foreach (var segment in SelectedFlight.ListOfSegments.Skip(1))
            {
                var coordinates = await _suggestionService.GetAirportCoordinates(ct, segment);
                var position = new BasicGeoposition
                {
                    Latitude = double.Parse(coordinates.Latitude),
                    Longitude = double.Parse(coordinates.Longitude)
                };
                legGeoPoints.Add(position);
                legAiportCodes.Add(segment);
            }

            legGeoPoints.Add(SearchInput.ArrivalAirportPosition);
            legAiportCodes.Add(SearchInput.ArrivalAirportCode);

            DrawFlightLine(legGeoPoints, legAiportCodes);
        }

        internal void DrawFlightLine(List<BasicGeoposition> points, List<string> airportCodes)
        {
            if (MapControl != null)
            {
                var mapLines =
                    MapControl.MapElements.Where(element => element.GetType().GetProperty("StrokeDashed") != null);
                if (mapLines.Any())
                {
                    MapControl.MapElements.Clear();
                }

                foreach (var point in points)
                {
                    var pointOne = point;
                    var nextIndex = points.IndexOf(pointOne) + 1;

                    if (points.Count - 1 >= nextIndex)
                    {
                        var nextPoint = points[nextIndex];

                        if (pointOne.Latitude != 0 && nextPoint.Latitude != 0)
                        {
                            if (pointOne.Latitude == nextPoint.Latitude && pointOne.Longitude == nextPoint.Longitude)
                                return;

                            var geodesic = Geodesic.ToGeodesic(pointOne, nextPoint);

                            var line = new MapPolyline {Path = new Geopath(geodesic)};
                            var brush = Application.Current.Resources["LogoYellowBrush"] as SolidColorBrush;
                            line.StrokeColor = brush.Color;
                            line.StrokeThickness = 5;
                            line.StrokeDashed = true;
                            line.ZIndex = 0;

                            var newMapCenter = geodesic[geodesic.Count/2];
                            MapControl.Center = new Geopoint(newMapCenter);
                            MapControl.MapElements.Add(line);
                        }
                    }
                }

                for (int i = 0; i < airportCodes.Count; i++)
                {
                    var mapIcon = new MapIcon
                    {
                        Title = airportCodes[i],
                        NormalizedAnchorPoint = new Point(0.5, 0.9),
                        ZIndex = 1
                    };
                    if (i == 0 || i == airportCodes.Count - 1)
                    {
                        mapIcon.Image =
                            RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/blue_pin.png"));
                    }
                    else
                    {
                        mapIcon.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/gray_pin.png"));
                    }

                    var geoPoint = new BasicGeoposition
                    {
                        Latitude = points[i].Latitude,
                        Longitude = points[i].Longitude
                    };

                    mapIcon.Location = new Geopoint(geoPoint);
                    MapControl.MapElements.Add(mapIcon);
                }
            }
        }
    }
}
