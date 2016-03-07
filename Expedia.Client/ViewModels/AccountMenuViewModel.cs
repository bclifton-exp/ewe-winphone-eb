using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using Expedia.Client.Interfaces;
using Expedia.Client.Utilities;
using Expedia.Client.Views;
using Expedia.Entites;
using Expedia.Services.Interfaces;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.Prism.Commands;

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

        private ICommand _connectFacebook;
        public ICommand ConnectFacebook
        {
            get { return _connectFacebook; }
            set
            {
                _connectFacebook = value;
                OnPropertyChanged("ConnectFacebook");
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


        private ICommand _signInCommand;
        public ICommand SignInCommand
        {
            get { return _signInCommand; }
            set
            {
                _signInCommand = value;
                OnPropertyChanged("SignInCommand");
            }
        }


        public AccountMenuViewModel(ILocationService locationService, ISettingsService settingsService, IPointOfSaleService pointOfSaleService, IAuthenticationService authenticationService) : base(locationService, settingsService, pointOfSaleService)
        {
            _authenticationService = authenticationService;
            _settingsService = settingsService;
            var currentToken = CancellationTokenManager.Instance().CreateAndSetCurrentToken();

            ConnectFacebook = new DelegateCommand(GetUriAndLoginToFacebook);

            SignInCommand = new DelegateCommand(() =>
            {
                SignInUser(currentToken);
            });

            ToCreateAccount = new RelayCommand(() =>
            {
                Navigator.Instance().NavigateToMenuView(typeof(CreateAccountView));
            });

            ContinueAsGuest = new RelayCommand(() =>
            {
                Navigator.Instance().CloseMenu();
            });
        }

        private void GetUriAndLoginToFacebook()
        {
            var callback = new Uri(Constants.Facebook.RedirectUrl, UriKind.RelativeOrAbsolute);

            var uri = new Uri(
             Constants.Facebook.ConnectUrl.InvariantCultureFormat(
                 Constants.Facebook.ExpediaClientAppId,
                 Uri.EscapeDataString(callback.ToString()),
                 Constants.Facebook.ResponseType,
                 Constants.Facebook.Scope),
                 UriKind.Absolute);

            Navigator.Instance().NavigateToMenuView(typeof(FacebookSignInWebView), uri);
        }

        public async void SignInUser(CancellationToken ct)
        {
            IsBusy = true;
            var response = await _authenticationService.SignIn(ct, UserName, Password);
            IsBusy = false;

            if (response.IsSuccess)
            {
                RealName = _settingsService.GetRealName();
                Navigator.Instance().CloseMenu(response.IsSuccess);
            }
            else
            {
                var errorText = response.Errors.ErrorInfo;
                //TODO display this somewhere
            }
        }
    }
}
