using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Expedia.Client.Interfaces;
using Expedia.Client.Utilities;
using Expedia.Entities.Cars;
using Expedia.Services.Interfaces;

namespace Expedia.Client.ViewModels
{
    public class CarResultsViewModel : BaseResultViewModel, ICarResultsViewModel
    {
        private ICarService _carService;
        private ISettingsService _settingsService;
        private IPointOfSaleService _pointOfSaleService;

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

        public CarResultsViewModel(ICarService carService, ISettingsService settingsService, IPointOfSaleService pointOfSaleService)
        {
            _carService = carService;
            _settingsService = settingsService;
            _pointOfSaleService = pointOfSaleService;
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
    }
}
