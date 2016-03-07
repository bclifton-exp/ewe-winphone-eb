using Expedia.Client.Interfaces;
using Expedia.Client.Utilities;
using Expedia.Client.Views;
using Expedia.Entities.Suggestions;
using Expedia.Services.Interfaces;
using GalaSoft.MvvmLight.Command;

namespace Expedia.Client.ViewModels
{
    public class MainPageViewModel : BaseSearchViewModel, IMainPageViewModel
    {
        private ISettingsService _settingsService { get; set; }
        private ILocationService _locationService { get; set; }
        private IAuthenticationService _authenticationService { get; set; }

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

        private bool _isSignedIn;
        public bool IsSignedIn
        {
            get { return _isSignedIn; }
            set
            {
                _isSignedIn = value;
                OnPropertyChanged("IsSignedIn");
            }
        }

        private bool _isMenuFrameVisible;
        public bool IsMenuFrameVisible
        {
            get { return _isMenuFrameVisible; }
            set
            {
                _isMenuFrameVisible = value;
                OnPropertyChanged("IsMenuFrameVisible");
            }
        }

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

        private RelayCommand _goToSettings;
        public RelayCommand GoToSettings
        {
            get { return _goToSettings; }
            set
            {
                _goToSettings = value;
                OnPropertyChanged("GoToSettings");
            }
        }

        private RelayCommand _goToAccount;
        public RelayCommand GoToAccount
        {
            get { return _goToAccount; }
            set
            {
                _goToAccount = value;
                OnPropertyChanged("GoToAccount");
            }
        }


        public MainPageViewModel(ISettingsService settingsService, ILocationService locationService, IAuthenticationService authenticationService) : base(SuggestionLob.NONE)
        {
            _settingsService = settingsService;
            _locationService = locationService;
            _authenticationService = authenticationService;

            IsSignedIn = !string.IsNullOrEmpty(_settingsService.GetUserToken());

            HamburgerClick = new RelayCommand(() =>
            {
                IsFlyoutMenuOpen = !IsFlyoutMenuOpen;
            });

            GoToSettings = new RelayCommand(() =>
            {
                IsFlyoutMenuOpen = false;
                Navigator.Instance().NavigateToMenuView(typeof(SettingsMenuView));
                IsMenuFrameVisible = true;
            });

            GoToAccount = new RelayCommand(() =>
            {
                IsFlyoutMenuOpen = false;

                if (IsSignedIn)
                {
                    _authenticationService.SignOut();
                    IsSignedIn = !string.IsNullOrEmpty(_settingsService.GetUserToken());
                }
                else
                {
                    Navigator.Instance().NavigateToMenuView(typeof(AccountMenuView));
                    IsMenuFrameVisible = true;
                }
            });

        }

    }
}
