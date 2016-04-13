using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Phone.UI.Input;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Expedia.Client.Interfaces;
using Expedia.Client.Utilities;
using Expedia.Client.Views;
using Expedia.Entites;
using Expedia.Entities.Cars;
using Expedia.Entities.Extensions;
using Expedia.Entities.Hotels;
using Expedia.Entities.Suggestions;
using Expedia.Services.Interfaces;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.Prism.Commands;

namespace Expedia.Client.ViewModels
{
    public class CarResultsViewModel : BaseResultViewModel, ICarResultsViewModel
    {
        private ICarService _carService;
        private ISettingsService _settingsService;
        private IPointOfSaleService _pointOfSaleService;
        private int SelectedPinCount;
        const string Automatic = "AUTOMATIC";
        const string Manual = "MANUAL";
        const string All = "ALL";

        private CarResults _carResults;
        public CarResults CarResults
        {
            get { return _carResults; }
            set
            {
                _carResults = value;
                OnPropertyChanged("CarResults");
            }
        }

        private ObservableCollection<Offer> _baseDetailResults;
        public ObservableCollection<Offer> BaseDetailResults
        {
            get { return _baseDetailResults; }
            set
            {
                _baseDetailResults = value;
                OnPropertyChanged("BaseDetailResults");
            }
        }

        private ObservableCollection<Offer> _carDetailResults;
        public ObservableCollection<Offer> CarDetailResults
        {
            get { return _carDetailResults; }
            set
            {
                _carDetailResults = value;
                OnPropertyChanged("CarDetailResults");
            }
        }

        private Offer _selectedCar;
        public Offer SelectedCar
        {
            get { return _selectedCar; }
            set
            {
                _selectedCar = value;
                SetPushPinFocus(_selectedCar);
                OnPropertyChanged("SelectedCar");
            }
        }

        private CarCategoryResult _selectedCarCategory;
        public CarCategoryResult SelectedCarCategory
        {
            get { return _selectedCarCategory; }
            set
            {
                _selectedCarCategory = value;
                if (value != null)
                {
                    GetSelectedCars(CarResults, value);
                }
                OnPropertyChanged("SelectedCarCategory");
            }
        }

        private bool _hasResults;
        public bool HasResults
        {
            get { return _hasResults; }
            set
            {
                _hasResults = value;
                OnPropertyChanged("HasResults");
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

        private bool _sortByPassengersChecked;
        public bool SortByPassengersChecked
        {
            get { return _sortByPassengersChecked; }
            set
            {
                _sortByPassengersChecked = value;
                OnPropertyChanged("SortByPassengersChecked");
            }
        }

        private bool _sortByDoorsChecked;
        public bool SortByDoorsChecked
        {
            get { return _sortByDoorsChecked; }
            set
            {
                _sortByDoorsChecked = value;
                OnPropertyChanged("SortByDoorsChecked");
            }
        }

        private bool _sortByBagsChecked;
        public bool SortByBagsChecked
        {
            get { return _sortByBagsChecked; }
            set
            {
                _sortByBagsChecked = value;
                OnPropertyChanged("SortByBagsChecked");
            }
        }

        private bool _isACFilterApplied;
        public bool IsACFilterApplied
        {
            get { return _isACFilterApplied; }
            set
            {
                _isACFilterApplied = value;
                ApplyFilters();
                OnPropertyChanged("IsACFilterApplied");
            }
        }

        private bool _isMileageFilterApplied;
        public bool IsMileageFilterApplied
        {
            get { return _isMileageFilterApplied; }
            set
            {
                _isMileageFilterApplied = value;
                ApplyFilters();
                OnPropertyChanged("IsMileageFilterApplied");
            }
        }

        private ObservableCollection<CarTransmissionFilter> _transmissionFilters;
        public ObservableCollection<CarTransmissionFilter> TransmissionFilters
        {
            get { return _transmissionFilters; }
            set
            {
                _transmissionFilters = value;
                OnPropertyChanged("TransmissionFilters");
            }
        }

        private ObservableCollection<CarCompanyFilter> _carCompanyFilters;
        public ObservableCollection<CarCompanyFilter> CarCompanyFilters
        {
            get { return _carCompanyFilters; }
            set
            {
                _carCompanyFilters = value;
                OnPropertyChanged("CarCompanyFilters");
            }
        }

        private SearchCarsLocalParameters _searchInput;
        public SearchCarsLocalParameters SearchInput
        {
            get { return _searchInput; }
            set
            {
                _searchInput = value;
                OnPropertyChanged("SearchInput");
            }
        }

        private ICommand _changeCarCategory;
        public ICommand ChangeCarCategory
        {
            get { return _changeCarCategory; }
            set
            {
                _changeCarCategory = value;
                OnPropertyChanged("ChangeCarCategory");
            }
        }

        private RelayCommand<Offer> _bookRentalCar;
        public RelayCommand<Offer> BookRentalCar
        {
            get { return _bookRentalCar; }
            set
            {
                _bookRentalCar = value;
                OnPropertyChanged("BookRentalCar");
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

        private ICommand _selectFilter;
        public ICommand SelectFilter
        {
            get { return _selectFilter; }
            set
            {
                _selectFilter = value;
                OnPropertyChanged("SelectFilter");
            }
        }
    

        public CarResultsViewModel(ICarService carService, ISettingsService settingsService, IPointOfSaleService pointOfSaleService)
        {
            _carService = carService;
            _settingsService = settingsService;
            _pointOfSaleService = pointOfSaleService;

            ChangeCarCategory = new DelegateCommand(ReturnToCarCategorySelection);
            BookRentalCar = new RelayCommand<Offer>(BuildAndNavigateToCarUri);
            SortResultsCommand = new DelegateCommand(SortResults);
            SelectFilter = new DelegateCommand(ApplyFilters);
        }

        private async void BuildAndNavigateToCarUri(Offer car) //Gone after Native - Web View Connector
        {
            var hostname = _settingsService.GetCurrentDomain();
            var pos = await _pointOfSaleService.GetCurrentPointOfSale();

            var carDeeplink = new Uri("https://www.{0}/carsearch/book?piid={1}&totalPriceShown={2}"
                .InvariantCultureFormat(
                    hostname,
                    car.productKey,
                    car.fare.total.amount));

            Navigator.Instance().NavigateForward(SuggestionLob.CARS, typeof(CarBookingWebView), carDeeplink);
        }

        public async void GetCarCategoryResults(SearchCarsLocalParameters carParams)
        {
            SearchInput = carParams;
            var ct = CancellationTokenManager.Instance().CreateAndSetCurrentToken();
            var results = await _carService.Search(ct, SearchInput);
            results.CarCategoryResults.RemoveAll(c => c.CarCategory == null);

            HasResults = results.AllCars.Count > 0;
            CarResults = results;
            ResultsCount = results.AllCars.Count;

            TransmissionFilters = new ObservableCollection<CarTransmissionFilter> { new CarTransmissionFilter { TransmissionType = Automatic, Title = Automatic.ToTitleCase(), IsFilterEnabled = true},
                    new CarTransmissionFilter {TransmissionType = Manual, Title = Manual.ToTitleCase(), IsFilterEnabled = true}};

            CarCompanyFilters = new ObservableCollection<CarCompanyFilter>();
        }

        private void GetSelectedCars(CarResults carResults, CarCategoryResult selectedCategory)
        {
            var carDetails = carResults.AllCars.Where(c => c.vehicleInfo.carCategoryDisplayLabel == selectedCategory.CarCategory && c.vehicleInfo.type == selectedCategory.CarType).ToObservableCollection();
            BaseDetailResults = carDetails;
            SetCarCompanyFilters();
            CarDetailResults = carDetails;
            BuildPushpins(CarDetailResults);
        }

        private void SetCarCompanyFilters()
        {
            var companies = BaseDetailResults.Select(c => c.vendor.name).ToList().Distinct();
            var filters = new ObservableCollection<CarCompanyFilter>();
            foreach (var company in companies)
            {
                filters.Add(new CarCompanyFilter { CompanyName = company, IsFilterEnabled = true, Title = company });
            }

            CarCompanyFilters = filters;
        }

        private void ReturnToCarCategorySelection()
        {
            CarDetailResults = null;
            SelectedCarCategory = null;
            SelectedCar = null;
            ClearPushPins();
        }

        internal void SortResults()
        {
            if (SortByBagsChecked)
            {
                CarDetailResults = CarDetailResults.OrderBy(c => c.vehicleInfo.largeLuggageCapacity).ToObservableCollection();
            }

            if (SortByDoorsChecked)
            {
                CarDetailResults = CarDetailResults.OrderBy(c => c.vehicleInfo.maxDoors).ToObservableCollection();
            }

            if (SortByPassengersChecked)
            {
                CarDetailResults = CarDetailResults.OrderBy(c => c.vehicleInfo.adultCapacity).ToObservableCollection();
            }

            if (SortByPriceLowToHighChecked)
            {
                CarDetailResults = CarDetailResults.OrderBy(c => double.Parse(c.fare.rate.amount)).ToObservableCollection();
            }
        }

        private void ApplyFilters()
        {
            CarDetailResults = BaseDetailResults;

            if (CarCompanyFilters != null)
            {
                var companyFiltersToApply = CarCompanyFilters.Where(f => f.IsFilterChecked);

                if (companyFiltersToApply.Any())
                {
                    CarDetailResults = CarDetailResults.Where(c => companyFiltersToApply.Select(n => n.CompanyName).ToList().Contains(c.vendor.name)).ToObservableCollection();
                }
            }

            if (TransmissionFilters != null)
            {
                var transmissionFilterToApply = TransmissionFilters.Where(f => f.IsFilterChecked);

                if (transmissionFilterToApply.Any())
                {
                    if (transmissionFilterToApply.First().TransmissionType == All)
                    {
                        CarDetailResults =
                            CarDetailResults.Where(
                                c =>
                                    c.vehicleInfo.transmission.Contains(Automatic) ||
                                    c.vehicleInfo.transmission.Contains(Manual)).ToObservableCollection();
                    }
                    else
                    {
                        CarDetailResults = CarDetailResults.Where(c => c.vehicleInfo.transmission.Contains(transmissionFilterToApply.First().TransmissionType)).ToObservableCollection();
                    }
                }
            }

            if (IsMileageFilterApplied)
            {
                CarDetailResults = CarDetailResults.Where(c => c.hasUnlimitedMileage).ToObservableCollection();
            }

            if (IsACFilterApplied)
            {
                CarDetailResults = CarDetailResults.Where(c => c.vehicleInfo.hasAirConditioning).ToObservableCollection();
            }

            ResultsCount = CarDetailResults.Count;
        }

        public void SelectedMapLoaded()
        {
            if(SelectedCar != null)
                SelectedCar.IsImageLoading = false;
        }

        public void CloseOpenOffers(object sender)
        {
            var control = sender as ContentControl;
            SelectedCar = control.DataContext as Offer;

            foreach (var offer in CarDetailResults)
            {
                if (offer.productKey != SelectedCar.productKey)
                    offer.IsOfferExpanded = false;
            }
        }

        public void CloseOpenOffers()
        {
            foreach (var offer in CarDetailResults)
            {
                if (SelectedCar != null)
                {
                    if (offer.productKey != SelectedCar.productKey)
                        offer.IsOfferExpanded = false;
                }
            }
        }

        internal void PushPinSelected(MapIcon selectedPushPin, ListView resultListView)
        {
            selectedPushPin.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/gray_pin.png"));

            SelectedCar = CarDetailResults.First(h => selectedPushPin.Title.Contains(h.vendor.name));//? TODO
            resultListView.ScrollIntoView(SelectedCar, ScrollIntoViewAlignment.Leading);
            SelectedCar.IsOfferExpanded = true;
        }

        private void ClearPushPins()
        {
            MapControl?.MapElements.Clear();
            SelectedPinCount = 0;
        }

        private void BuildPushpins(ObservableCollection<Offer> cars)
        {
            if (MapControl != null)
            {
                foreach (var car in cars)
                {
                    var mapIcon = new MapIcon
                    {
                        Title = car.vendor.name + "(" + car.fare.rate.formattedWholePrice + ")",
                        Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/purple_pin.png")),
                        NormalizedAnchorPoint = new Point(0.5, 1),
                        ZIndex = 0
                    };

                    var geoPoint = new BasicGeoposition
                    {
                        Latitude = car.pickUpLocation.latitude,
                        Longitude = car.pickUpLocation.longitude
                    };
                    mapIcon.Location = new Geopoint(geoPoint);

                    MapControl.MapElements.Add(mapIcon);
                }
            }
        }

        private void SetPushPinFocus(Offer selectedCar)
        {
            if (selectedCar != null && MapControl != null)
            {
                SelectedPinCount++;
                var geoPoint = new BasicGeoposition
                {
                    Latitude = selectedCar.pickUpLocation.latitude,
                    Longitude = selectedCar.pickUpLocation.longitude
                };
                MapControl.Center = new Geopoint(geoPoint);

                foreach (MapElement selected in MapControl.MapElements.Where(selected => ((dynamic)selected).Title.Contains(selectedCar.vendor.name)))
                {
                    ((dynamic)selected).Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/gray_pin.png"));
                    ((dynamic)selected).ZIndex = SelectedPinCount;
                }
            }
        }
    }
}
