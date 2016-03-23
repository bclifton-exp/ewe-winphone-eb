using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Expedia.Client.Interfaces;
using Expedia.Client.Utilities;
using Expedia.Entities.Extensions;
using Expedia.Entities.Flights;
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

        //private FlightResultSelection _flightResultSelection;
        //public FlightResultSelection FlightResultSelection
        //{
        //    get { return _flightResultSelection; }
        //    set
        //    {
        //        _flightResultSelection = value;
        //        OnPropertyChanged("FlightResultSelection");
        //    }
        //}

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



        //public FlightResultSelection Selection { get; set; }
        //public FlightResultItem[] Flights { get; set; }

        //public FlightFilter[] AirlineFilters { get; set; }
        //public FlightFilter[] StopCountFilters { get; set; }



        public FlightResultsViewModel(IFlightService flightService, ISettingsService settingsService, IPointOfSaleService pointOfSaleService)
        {
            _flightService = flightService;
            _settingsService = settingsService;
            _pointOfSaleService = pointOfSaleService;
        }

        public async void GetFlightResults(SearchFlightsLocalParameters searchCriteria)
        {
            SearchInput = searchCriteria;
            FlightResultItems = null;
            //ClearPushPins();
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


            //TODO set duration style and then build out layover times

            //BuildPushpins(HotelResultItems);

            //BookHotel = new RelayCommand<HotelResultItem>(BuildAndNavigateToHotelUri);
            //SortResultsCommand = new DelegateCommand(SortResults);
        }

    }
}
