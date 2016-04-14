using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Windows.Input;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Media;
using Expedia.Client.Interfaces;
using Expedia.Client.Utilities;
using Expedia.Client.Views;
using Expedia.Entites;
using Expedia.Entities.Extensions;
using Expedia.Entities.Flights;
using Expedia.Entities.Suggestions;
using Expedia.Services.Interfaces;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.Prism.Commands;

namespace Expedia.Client.ViewModels
{
    public class FlightResultsViewModel : BaseResultViewModel, IFlightResultsViewModel
    {
        private IFlightService _flightService;
        private ISettingsService _settingsService;
        private IPointOfSaleService _pointOfSaleService;
        private ISuggestionService _suggestionService;

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

        private bool _sortByPriceLowToHighChecked;
        public bool SortByPriceLowToHighChecked
        {
            get { return _sortByPriceLowToHighChecked; }
            set
            {
                _sortByPriceLowToHighChecked = value;
                OnPropertyChanged("SortByPriceLowToHighChecked");
            }
        }

        private bool _sortByDurationChecked;
        public bool SortByDurationChecked
        {
            get { return _sortByDurationChecked; }
            set
            {
                _sortByDurationChecked = value;
                OnPropertyChanged("SortByDurationChecked");
            }
        }

        private bool _sortByArrivalChecked;
        public bool SortByArrivalChecked
        {
            get { return _sortByArrivalChecked; }
            set
            {
                _sortByArrivalChecked = value;
                OnPropertyChanged("SortByArrivalChecked");
            }
        }

        private bool _sortByDepartureChecked;
        public bool SortByDepartureChecked
        {
            get { return _sortByDepartureChecked; }
            set
            {
                _sortByDepartureChecked = value;
                OnPropertyChanged("SortByDepartureChecked");
            }
        }

        private bool _isImageLoaded;
        public bool IsImageLoaded
        {
            get { return _isImageLoaded; }
            set
            {
                _isImageLoaded = value;
                OnPropertyChanged("IsImageLoaded");
            }
        }

        private Uri _destinationImageUri;
        public Uri DestinationImageUri
        {
            get { return _destinationImageUri; }
            set
            {
                _destinationImageUri = value;
                IsImageLoaded = value != null;
                OnPropertyChanged("DestinationImageUri");
            }
        }

        private ICommand _sortResultsCommand;
        public ICommand SortResultsCommand
        {
            get { return _sortResultsCommand; }
            set
            {
                _sortResultsCommand = value;
                OnPropertyChanged("SortResultsCommand");
            }
        }

        private FlightFilter[] _airlineFilters;
        public FlightFilter[] AirlineFilters
        {
            get { return _airlineFilters; }
            set
            {
                _airlineFilters = value;
                OnPropertyChanged("AirlineFilters");
            }
        }

        private FlightFilter[] _stopCountFilters;
        public FlightFilter[] StopCountFilters
        {
            get { return _stopCountFilters; }
            set
            {
                _stopCountFilters = value;
                OnPropertyChanged("StopCountFilters");
            }
        }

        private RelayCommand<FlightResultItem> _bookFlight;
        public RelayCommand<FlightResultItem> BookFlight
        {
            get { return _bookFlight; }
            set
            {
                _bookFlight = value;
                OnPropertyChanged("BookFlight");
            }
        }

        private ICommand _changeDepartureFlight;
        public ICommand ChangeDepartureFlight
        {
            get { return _changeDepartureFlight; }
            set
            {
                _changeDepartureFlight = value;
                OnPropertyChanged("ChangeDepartureFlight");
            }
        }


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
            searchCriteria.SelectedDeparture = null; //might be temp
            CurrentSearchCriteria = searchCriteria;
            DestinationName = searchCriteria.ArrivalCityShorterName;
            ReturnName = searchCriteria.DepartureCityShorterName;
            FlightResultItems = null;
            var ct = CancellationTokenManager.Instance().CreateAndSetCurrentToken();
            var results = await _flightService.SearchFlights(ct, searchCriteria);
            DestinationImageUri = new Uri(results.Selection.DestinationPictureUrl);
            StopCountFilters = results.StopCountFilters;
            AirlineFilters = results.AirlineFilters;
            ResultsCount = results.Flights.Count();
            FlightResultItems = results.Flights.ToObservableCollection();
            DestinationPictureUrl = results.Selection.DestinationPictureUrl;

            BookFlight = new RelayCommand<FlightResultItem>(BuildAndNavigateToFlightUri);
            SortResultsCommand = new DelegateCommand(SortResults);
            ChangeDepartureFlight = new DelegateCommand(PickNewDepartureFlight);
        }

        private async void PickNewDepartureFlight()
        {
            if (FlightResultItems == null)
                return;

            SelectedDeparture = null;
            FlightResultItems = null;
            CurrentSearchCriteria.SelectedDeparture = null;

            var ct = CancellationTokenManager.Instance().CreateAndSetCurrentToken();
            var results = await _flightService.SearchFlights(ct, CurrentSearchCriteria);
            FlightResultItems = results.Flights.ToObservableCollection();
            StopCountFilters = results.StopCountFilters;
            AirlineFilters = results.AirlineFilters;
            ResultsCount = results.Flights.Count();
            ClearMapPins();
            BuildDeparturePushPin(CurrentSearchCriteria);
        }

        private async void BuildAndNavigateToFlightUri(FlightResultItem flight) //Gone after Native - Web View Connector
        {
            if (SelectedDeparture == null && !CurrentSearchCriteria.IsOneWay)
            {
                SelectedDeparture = flight;
                CurrentSearchCriteria.SelectedDeparture = flight;

                FlightResultItems = null;
                var ct = CancellationTokenManager.Instance().CreateAndSetCurrentToken();
                var results = await _flightService.SearchFlights(ct, CurrentSearchCriteria);
                FlightResultItems = results.Flights.ToObservableCollection();
                StopCountFilters = results.StopCountFilters;
                AirlineFilters = results.AirlineFilters;
                ResultsCount = results.Flights.Count();
                ClearMapPins();
                BuildArrivalPushPin(CurrentSearchCriteria);
            }
            else
            {
                var hostname = _settingsService.GetCurrentDomain();
                var infantInLap = "N";

                const string unifiedPageUriTemplate = "https://www.{0}/pubspec/scripts/eap.asp?GOTO=UDP&piid={1}&departTLA=L1:{2}&arrivalTLA=L1:{3}&departDate=L1:{4}&nAdults={5}&tripType={6}&nChildren={7}&infantInLap={8}&productType=air&brandcid=App.WindowsUWP.Native";

                var ddte = "{0}"
                    .InvariantCultureFormat(
                        flight.DepartureTimeRaw.Date
                            .ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));

                if (!CurrentSearchCriteria.IsOneWay)
                {
                    // The date set above is the return leg. We append in front the real departure flight and the above's "L2:" prefix.
                    ddte = "{0}|L2:{1}"
                        .InvariantCultureFormat(
                            CurrentSearchCriteria.SelectedDeparture.DepartureTimeRaw.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                            ddte);
                }

                var tripType = CurrentSearchCriteria.IsOneWay ? "OneWay" : "RoundTrip";

                var flightDeepLink = new Uri(unifiedPageUriTemplate
                    .InvariantCultureFormat(
                        hostname,
                        flight.ProductKey,
                        CurrentSearchCriteria.DepartureAirportCode,
                        CurrentSearchCriteria.ArrivalAirportCode,
                        ddte,
                        CurrentSearchCriteria.AdultsCount,
                        tripType,
                        CurrentSearchCriteria.ChildCount,
                        infantInLap));

                Navigator.Instance().NavigateForward(SuggestionLob.FLIGHTS, typeof(FlightBookingWebView), flightDeepLink);
            }
        }

        internal void ClearMapPins()
        {
            if (MapControl != null)
            {
                MapControl.MapElements.Clear();
            }
        }

        internal void SortResults()
        {
            if (SortByArrivalChecked)
            {
                GetSortedFlightResults(SortFlightsByType.ArrivalTime);
            }

            if (SortByDepartureChecked)
            {
                GetSortedFlightResults(SortFlightsByType.DepartureTime);
            }

            if (SortByDurationChecked)
            {
                GetSortedFlightResults(SortFlightsByType.Duration);
            }

            if (SortByPriceLowToHighChecked)
            {
                GetSortedFlightResults(SortFlightsByType.PricesLowToHigh);
            }
        }

        internal async void FilterResults()
        {
            try
            {
                if (SelectedDeparture == null)
                {
                    CurrentSearchCriteria.DepartureAirlineFilters = AirlineFilters;
                    CurrentSearchCriteria.DepartureStopCountFilters = StopCountFilters;
                }
                else
                {
                    CurrentSearchCriteria.ReturnAirlineFilters = AirlineFilters;
                    CurrentSearchCriteria.ReturnStopCountFilters = StopCountFilters;
                }

                FlightResultItems = null;

                var ct = CancellationTokenManager.Instance().CreateAndSetCurrentToken();
                var filteredResults = await _flightService.SearchFlights(ct, CurrentSearchCriteria);
                FlightResultItems = filteredResults.Flights.ToObservableCollection();

                AirlineFilters = filteredResults.AirlineFilters;
                StopCountFilters = filteredResults.StopCountFilters;
                ResultsCount = filteredResults.Flights.Count();
            }
            catch (Exception ex)
            {
                //eating exceptions for when filters are clicked too quickly, doesn't matter
            }
        }

        private async void GetSortedFlightResults(SortFlightsByType sortType)
        {
            FlightResultItems = null;
            CurrentSearchCriteria.SortBy = sortType;
            var ct = CancellationTokenManager.Instance().CreateAndSetCurrentToken();
            var sortedResults = await _flightService.SearchFlights(ct, CurrentSearchCriteria);
            FlightResultItems = sortedResults.Flights.ToObservableCollection();
        }

        internal async void GetAirportLegCoordinates(FlightResultItem SelectedFlight)
        {
            if (SelectedFlight != null)
            {
                var ct = CancellationTokenManager.Instance().CreateAndSetCurrentToken();
                var legGeoPoints = new List<BasicGeoposition> { CurrentSearchCriteria.DepartureAirportPosition };
                var legAiportCodes = new List<string> { CurrentSearchCriteria.DepartureAirportCode };

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

                legGeoPoints.Add(CurrentSearchCriteria.ArrivalAirportPosition);
                legAiportCodes.Add(CurrentSearchCriteria.ArrivalAirportCode);

                DrawFlightLine(legGeoPoints, legAiportCodes);
            }
        }

        internal void BuildDeparturePushPin(SearchFlightsLocalParameters airport)
        {
            if (MapControl != null)
            {
                var mapElement = MapControl.MapElements.FirstOrDefault(element => ((dynamic)element).MapTabIndex == 0);
                if (mapElement != null)
                {
                    var indexToRemove = MapControl.MapElements.IndexOf(mapElement);
                    MapControl.MapElements.RemoveAt(indexToRemove);
                }

                var mapIcon = new MapIcon
                {
                    Title = airport.DepartureCityShortName,
                    Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/blue_pin.png")),
                    MapTabIndex = 0,
                    NormalizedAnchorPoint = new Point(0.5, 0.9),
                    ZIndex = 1
                };

                var geoPoint = new BasicGeoposition
                {
                    Latitude = airport.DepartureAirportPosition.Latitude,
                    Longitude = airport.DepartureAirportPosition.Longitude
                };

                mapIcon.Location = new Geopoint(geoPoint);
                MapControl.Center = mapIcon.Location;
                MapControl.MapElements.Add(mapIcon);
            }
        }

        internal void BuildArrivalPushPin(SearchFlightsLocalParameters airport)
        {
            if (MapControl != null)
            {
                var mapElement = MapControl.MapElements.FirstOrDefault(element => ((dynamic)element).MapTabIndex == 0);
                if (mapElement != null)
                {
                    var indexToRemove = MapControl.MapElements.IndexOf(mapElement);
                    MapControl.MapElements.RemoveAt(indexToRemove);
                }

                var mapIcon = new MapIcon
                {
                    Title = airport.ArrivalCityShortName,
                    Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/blue_pin.png")),
                    MapTabIndex = 0,
                    NormalizedAnchorPoint = new Point(0.5, 0.9),
                    ZIndex = 1
                };

                var geoPoint = new BasicGeoposition
                {
                    Latitude = airport.ArrivalAirportPosition.Latitude,
                    Longitude = airport.ArrivalAirportPosition.Longitude
                };

                mapIcon.Location = new Geopoint(geoPoint);
                MapControl.Center = mapIcon.Location;
                MapControl.MapElements.Add(mapIcon);
            }
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

                            var midPoint = Geodesic.MidPoint(points.First(), points.Last());
                            MapControl.Center = new Geopoint(midPoint);
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
