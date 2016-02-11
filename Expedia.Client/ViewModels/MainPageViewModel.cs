using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Input;
using Windows.Devices.Geolocation;
using Expedia.Client.Interfaces;
using Expedia.Entities.Suggestions;
using Expedia.Injection;
using Expedia.Services.Interfaces;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.Prism.Commands;

namespace Expedia.Client.ViewModels
{
    public class MainPageViewModel : BaseSearchViewModel, IMainPageViewModel
    {
        private ISettingsService _settingsService { get; set; }
        private ILocationService _locationService { get; set; }

        private RelayCommand _hamburgerClick;
        public RelayCommand HamburgerClick
        {
            get { return _hamburgerClick; }
            set
            {
                _hamburgerClick = value;
                OnPropertyChanged("HamburgerClick");
            }
        }

        private bool _isFlyoutMenuOpen;
        public bool IsFlyoutMenuOpen
        {
            get { return _isFlyoutMenuOpen; }
            set
            {
                _isFlyoutMenuOpen = value;
                OnPropertyChanged("IsFlyoutMenuOpen");
            }
        }



        public MainPageViewModel(ISettingsService settingsService, ILocationService locationService) : base(SuggestionLob.NONE)
        {
            _settingsService = settingsService;
            _locationService = locationService;

            HamburgerClick = new RelayCommand(() =>
            {
                IsFlyoutMenuOpen = !IsFlyoutMenuOpen;
            });
        }

    }
}
