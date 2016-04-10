using System;
using System.Collections.Generic;
using System.Text;
using Expedia.Client.Extensions;
using Expedia.Client.Interfaces;
using Expedia.Entities.Suggestions;
using Expedia.Services.Interfaces;
using GalaSoft.MvvmLight.Command;

namespace Expedia.Client.ViewModels
{
    public class SearchCarsViewModel : BaseSearchViewModel, ISearchCarsViewModel
    {
        private ICarService _carService { get; set; }
        private ILocationService _locationService { get; set; }

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

        public SearchCarsViewModel(ICarService carService, ILocationService locationService) : base(SuggestionLob.CARS)
        {
            _carService = carService;
            _locationService = locationService;
            GetNearbySuggestions();
            SearchCars = new DependentRelayCommand(ExecuteCarSearch, CanExecuteSearch, this, () => SelectedSearchSuggestion);
        }

        private void ExecuteCarSearch()
        {

        }

        private bool CanExecuteSearch()
        {
            return SelectedSearchSuggestion != null; //TODO valid date stuff too
        }
    }
}
