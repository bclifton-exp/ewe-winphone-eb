using System;
using Expedia.Client.Extensions;
using Expedia.Client.Interfaces;
using Expedia.Client.Utilities;
using Expedia.Client.Views;
using Expedia.Entities.Cars;
using Expedia.Entities.Suggestions;
using Expedia.Services.Interfaces;
using GalaSoft.MvvmLight.Command;

namespace Expedia.Client.ViewModels
{
    public class SearchCarsViewModel : BaseSearchViewModel, ISearchCarsViewModel
    {
        private ICarService _carService { get; set; }
        private ILocationService _locationService { get; set; }
        private const int SearchRadius = 20;

        private RelayCommand _searchCars;
        public RelayCommand SearchCars
        {
            get { return _searchCars; }
            set
            {
                _searchCars = value;
                OnPropertyChanged("SearchCars");
            }
        }

        private TimeSpan _pickupTime;
        public TimeSpan PickupTime
        {
            get { return _pickupTime; }
            set
            {
                _pickupTime = value;
                OnPropertyChanged("PickupTime");
            }
        }

        private TimeSpan _dropOffTime;
        public TimeSpan DropOffTime
        {
            get { return _dropOffTime; }
            set
            {
                _dropOffTime = value;
                OnPropertyChanged("DropOffTime");
            }
        }

        public SearchCarsViewModel(ICarService carService, ILocationService locationService) : base(SuggestionLob.CARS)
        {
            PickupTime = DateTimeExtensions.RoundUp(DateTime.Now, TimeSpan.FromMinutes(30)).TimeOfDay.Add(new TimeSpan(0, 0, 30, 0));
            DropOffTime = new TimeSpan(18, 0, 0);

            _carService = carService;
            _locationService = locationService;
            GetNearbySuggestions();
            SearchCars = new DependentRelayCommand(ExecuteCarSearch, CanExecuteSearch, this, () => SelectedSearchSuggestion);
        }

        private void ExecuteCarSearch()
        {
            double latitude;
            double.TryParse(SelectedSearchSuggestion.Coordinates.Latitude, out latitude);
            double longitude;
            double.TryParse(SelectedSearchSuggestion.Coordinates.Longitude, out longitude);

            var searchCarsParams = new SearchCarsLocalParameters
            {
                PickupTime = StartDate + PickupTime,
                DropOffTime = EndDate + DropOffTime,
                SearchRadius = SearchRadius,
                PickupLocationLat = latitude.ToString(),
                PickupLocationLon = longitude.ToString(),
                PickupLocationFriendly = SelectedSearchSuggestion.RegionNames.ShortName
            };

            if (SelectedSearchSuggestion.Type == "AIRPORT")
            {
                searchCarsParams.AirportCode = SelectedSearchSuggestion.HierarchyInfo.Airport.AirportCode;
            }

            if (MapControl != null)
                searchCarsParams.SelectedMapCenter = MapControl.Center;

            SearchParamsMemory.Instance().CarParams = searchCarsParams;
            Navigator.Instance().NavigateForward(SuggestionLob.CARS, typeof(CarResultsView), searchCarsParams);
        }

        private bool CanExecuteSearch()
        {
            return SelectedSearchSuggestion != null; //TODO valid date stuff too
        }

        internal void SetExistingTripSearchDetails() //TODO Car params on back nav
        {
            var existingParams = SearchParamsMemory.Instance().CarParams;

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
