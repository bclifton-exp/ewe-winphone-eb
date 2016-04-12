using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Expedia.Entities.Cars;
using Expedia.Entities.Hotels;
using Expedia.Services.Interfaces;
using Microsoft.Practices.Prism.Commands;

namespace Expedia.Client.ViewModels
{
    public class CarResultsViewModel : BaseResultViewModel, ICarResultsViewModel
    {
        private ICarService _carService;
        private ISettingsService _settingsService;
        private IPointOfSaleService _pointOfSaleService;
        private int SelectedPinCount;

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

        private CarResults _baseResults;
        public CarResults BaseResults
        {
            get { return _baseResults; }
            set
            {
                _baseResults = value;
                OnPropertyChanged("BaseResults");
            }
        }

        private List<Offer> _carDetailResults;
        public List<Offer> CarDetailResults
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


        public CarResultsViewModel(ICarService carService, ISettingsService settingsService, IPointOfSaleService pointOfSaleService)
        {
            _carService = carService;
            _settingsService = settingsService;
            _pointOfSaleService = pointOfSaleService;

            ChangeCarCategory = new DelegateCommand(ReturnToCarCategorySelection);
        }

        public async void GetCarCategoryResults(SearchCarsLocalParameters carParams)
        {
            SearchInput = carParams;
            var ct = CancellationTokenManager.Instance().CreateAndSetCurrentToken();
            var results = await _carService.Search(ct, SearchInput);
            results.CarCategoryResults.RemoveAll(c => c.CarCategory == null);

            HasResults = results.AllCars.Count > 0;
            CarResults = results;
            BaseResults = results;
            ResultsCount = results.AllCars.Count;
        }

        private void GetSelectedCars(CarResults carResults, CarCategoryResult selectedCategory)
        {
            CarDetailResults = carResults.AllCars.Where(c => c.vehicleInfo.carCategoryDisplayLabel == selectedCategory.CarCategory && c.vehicleInfo.type == selectedCategory.CarType).ToList();
            BuildPushpins(CarDetailResults);
        }

        private void ReturnToCarCategorySelection()
        {
            CarDetailResults = null;
            SelectedCarCategory = null;
            SelectedCar = null;
            ClearPushPins();
        }

        public void SelectedMapLoaded()
        {
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
                if (offer.productKey != SelectedCar.productKey)
                    offer.IsOfferExpanded = false;
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

        private void BuildPushpins(List<Offer> cars)
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
