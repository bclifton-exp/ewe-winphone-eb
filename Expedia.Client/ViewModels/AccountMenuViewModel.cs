using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Windows.UI.Xaml.Controls;
using Expedia.Client.Interfaces;
using Expedia.Client.Utilities;
using Expedia.Client.Views;
using Expedia.Services.Interfaces;
using GalaSoft.MvvmLight.Command;

namespace Expedia.Client.ViewModels
{
    public class AccountMenuViewModel : BaseMenuViewModel, IAccountMenuViewModel
    {
        private IAuthenticationService _authenticationService;
        private ISettingsService _settingsService;

        private bool _isFacebookLinking;
        public bool IsFacebookLinking
        {
            get { return _isFacebookLinking; }
            set
            {
                _isFacebookLinking = value;
                OnPropertyChanged("IsFacebookLinking");
            }
        }

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged("UserName");
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }

        private string _realName;
        public string RealName
        {
            get { return _realName; }
            set
            {
                _realName = value;
                OnPropertyChanged("RealName");
            }
        }

        private RelayCommand _continueAsGuest;
        public RelayCommand ContinueAsGuest
        {
            get { return _continueAsGuest; }
            set
            {
                _continueAsGuest = value;
                OnPropertyChanged("ContinueAsGuest");
            }
        }

        private RelayCommand _connectWithFacebook;
        public RelayCommand ConnectWithFacebook
        {
            get { return _connectWithFacebook; }
            set
            {
                _connectWithFacebook = value;
                OnPropertyChanged("ConnectWithFacebook");
            }
        }

        private RelayCommand _toCreateAccount;
        public RelayCommand ToCreateAccount
        {
            get { return _toCreateAccount; }
            set
            {
                _toCreateAccount = value;
                OnPropertyChanged("ToCreateAccount");
            }
        }


        //private RelayCommand _signInCommand;
        //public RelayCommand SignInCommand
        //{
        //    get { return _signInCommand; }
        //    set
        //    {
        //        _signInCommand = value;
        //        OnPropertyChanged("SignInCommand");
        //    }
        //}


        public AccountMenuViewModel(ILocationService locationService, ISettingsService settingsService, IPointOfSaleService pointOfSaleService, IAuthenticationService authenticationService) : base(locationService, settingsService, pointOfSaleService)
        {
            _authenticationService = authenticationService;
            _settingsService = settingsService;

            ToCreateAccount = new RelayCommand(() =>
            {
                Navigator.Instance().NavigateToMenuView(typeof(CreateAccountView));
            });

            //SignInCommand = new RelayCommand(() =>
            //{
            //    SignInUser(currentToken);
            //});
        }

        public async void SignInUser()
        {
            var currentToken = CancellationTokenManager.Instance().CreateAndSetCurrentToken();
            IsBusy = true;
            var isSignedIn = await _authenticationService.SignIn(currentToken, UserName, Password);
         
            if (isSignedIn)
            {
                RealName = _settingsService.GetRealName();
                Navigator.Instance().CloseMenu();
            }
        }
    }
}
