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

            //BuildPushpins(HotelResultItems);

            //BookHotel = new RelayCommand<HotelResultItem>(BuildAndNavigateToHotelUri);
            //SortResultsCommand = new DelegateCommand(SortResults);
        }

    }
}
