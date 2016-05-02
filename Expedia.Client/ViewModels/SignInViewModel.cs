using System;
using System.Threading;
using System.Windows.Input;
using Expedia.Client.Extensions;
using Expedia.Client.Interfaces;
using Expedia.Client.Utilities;
using Expedia.Client.Views;
using Expedia.Entites;
using Expedia.Entities.User;
using Expedia.Services.Interfaces;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.Prism.Commands;

namespace Expedia.Client.ViewModels
{
    public class SignInViewModel : BaseMenuViewModel, ISignInViewModel
    {
        #region Properties

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

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged("LastName");
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

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set
            {
                _confirmPassword = value;
                ObservePasswordChange();
                OnPropertyChanged("ConfirmPassword");
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

        private string _initials;
        public string Initials
        {
            get { return _initials; }
            set
            {
                _initials = value;
                OnPropertyChanged("Initials");
            }
        }

        private DelegateCommand _continueAsGuest;
        public DelegateCommand ContinueAsGuest
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

        private ICommand _forgotPassword;
        public ICommand ForgotPassword
        {
            get { return _forgotPassword; }
            set
            {
                _forgotPassword = value;
                OnPropertyChanged("ForgotPassword");
            }
        }

        private DelegateCommand _signInCommand;
        public DelegateCommand SignInCommand
        {
            get { return _signInCommand; }
            set
            {
                _signInCommand = value;
                OnPropertyChanged("SignInCommand");
            }
        }

        private RelayCommand _nextCommand;
        public RelayCommand NextCommand
        {
            get { return _nextCommand; }
            set
            {
                _nextCommand = value;
                OnPropertyChanged("NextCommand");
            }
        }

        private RelayCommand _createAccount;
        public RelayCommand CreateAccount
        {
            get { return _createAccount; }
            set
            {
                _createAccount = value;
                OnPropertyChanged("CreateAccount");
            }
        }

        private bool _inSignInMode;
        public bool InSignInMode
        {
            get { return _inSignInMode; }
            set
            {
                _inSignInMode = value;
                OnPropertyChanged("InSignInMode");
            }
        }

        private bool _inPasswordCreationMode;
        public bool InPasswordCreationMode
        {
            get { return _inPasswordCreationMode; }
            set
            {
                _inPasswordCreationMode = value;
                OnPropertyChanged("InPasswordCreationMode");
            }
        }

        private bool _inCreationMode;
        public bool InCreationMode
        {
            get { return _inCreationMode; }
            set
            {
                _inCreationMode = value;
                OnPropertyChanged("InCreationMode");
            }
        }

        private bool _passwordsMatch;
        public bool PasswordsMatch
        {
            get { return _passwordsMatch; }
            set
            {
                _passwordsMatch = value;
                OnPropertyChanged("PasswordsMatch");
            }
        }

        private bool _passwordLengthMet;
        public bool PasswordLengthMet
        {
            get { return _passwordLengthMet; }
            set
            {
                _passwordLengthMet = value;
                OnPropertyChanged("PasswordLengthMet");
            }
        }

        #endregion

        public SignInViewModel(ILocationService locationService, ISettingsService settingsService, IPointOfSaleService pointOfSaleService, IAuthenticationService authenticationService) : base(locationService, settingsService, pointOfSaleService)
        {
            InSignInMode = true;
            _authenticationService = authenticationService;
            _settingsService = settingsService;
            var currentToken = CancellationTokenManager.Instance().CreateAndSetCurrentToken();
            ObservePasswordChange();

            ConnectFacebook = new DelegateCommand(GetUriAndLoginToFacebook);

            SignInCommand = new DelegateCommand(() =>
            {
                IsBusy = true;
                SignInUser(currentToken);
                IsBusy = false;
            });

            ToCreateAccount = new RelayCommand(() =>
            {
                InSignInMode = false;
                InCreationMode = true;
            });

            ContinueAsGuest = new DelegateCommand(() =>
            {
                Navigator.Instance().NavigateToMainPage();
            });

            ForgotPassword = new DelegateCommand(() =>
            {
                Navigator.Instance().NavigateToForgotPassword(new Uri("https://www.expedia.com/user/forgotpassword"));
            });

            NextCommand = new DependentRelayCommand(ExecuteAccountInfoEntered, CanExecuteAccountInfoEntered, this, ()=> UserName, ()=> FirstName, ()=> LastName);

            CreateAccount = new DependentRelayCommand(ExecuteCreateAccount, CanExecuteCreateAccount, this, ()=> PasswordsMatch);
        }

        private async void ExecuteCreateAccount()
        {
            IsBusy = true;
            var ct = CancellationTokenManager.Instance().CreateAndSetCurrentToken();
            var accountParams = new AccountCreationParams(UserName, Password, FirstName, LastName, true);

            IsBusy = true;
            var response = await _authenticationService.CreateAccount(ct, accountParams);
            IsBusy = false;

            if (response.Errors == null)
            {
                Navigator.Instance().NavigateToMainPage();
            }
            IsBusy = false;
            //TODO error catch
        }

        private bool CanExecuteCreateAccount()
        {
            return PasswordsMatch && Password != null && ConfirmPassword != null && PasswordLengthMet;
        }

        private void ObservePasswordChange()
        {
            PasswordsMatch = Password == ConfirmPassword;
            PasswordLengthMet = Password != null && Password.Length > 5;

            if (string.IsNullOrEmpty(ConfirmPassword))
            {
                PasswordsMatch = true;
                PasswordLengthMet = true;
            }
        }

        private void ExecuteAccountInfoEntered()
        {
            InCreationMode = false;
            InPasswordCreationMode = true;
            Initials = FirstName.Substring(0, 1).ToUpper() + LastName.Substring(0, 1).ToUpper();
        }

        private bool CanExecuteAccountInfoEntered()
        {
            return !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName);
        }

        private void GetUriAndLoginToFacebook() //LINKING FB
        {
            var callback = new Uri(Constants.Facebook.RedirectUrl, UriKind.RelativeOrAbsolute);

            var uri = new Uri(
             Constants.Facebook.ConnectUrl.InvariantCultureFormat(
                 Constants.Facebook.ExpediaClientAppId,
                 Uri.EscapeDataString(callback.ToString()),
                 Constants.Facebook.ResponseType,
                 Constants.Facebook.Scope),
                 UriKind.Absolute);

            Navigator.Instance().NavigateToFacebookView(typeof(FacebookSignInWebView), uri);
        }

        public async void SignInUser(CancellationToken ct)
        {
            IsBusy = true;
            var response = await _authenticationService.SignIn(ct, UserName, Password);
            IsBusy = false;

            if (response.IsSuccess)
            {
                RealName = _settingsService.GetRealName();
                Navigator.Instance().NavigateToMainPage();
            }
            else
            {
                var errorText = response.Errors.ErrorInfo;
                //TODO display this somewhere
            }
        }

        public void BackPressed()
        {
            if (InPasswordCreationMode)
            {
                InPasswordCreationMode = false;
                InCreationMode = true;
            }
            else
            {
                if (InCreationMode)
                {
                    InCreationMode = false;
                    UserName = null;
                    InSignInMode = true;
                }
            }
        }

    }
}
