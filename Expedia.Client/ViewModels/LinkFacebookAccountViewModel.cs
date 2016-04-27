using System.Windows.Input;
using Expedia.Client.Interfaces;
using Expedia.Client.Utilities;
using Expedia.Client.Views;
using Expedia.Entities.User;
using Expedia.Services.Interfaces;
using Microsoft.Practices.Prism.Commands;

namespace Expedia.Client.ViewModels
{
    public class LinkFacebookAccountViewModel : BaseMenuViewModel, ILinkFacebookAccountViewModel
    {
        private IAuthenticationService _authenticationService;

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

        private string _accessToken;
        public string AccessToken
        {
            get { return _accessToken; }
            set
            {
                _accessToken = value;
                ProcessFacebookSignIn();
                OnPropertyChanged("AccessToken");
            }
        }

        private bool _isCreateNew;
        public bool IsCreateNew
        {
            get { return _isCreateNew; }
            set
            {
                _isCreateNew = value;
                OnPropertyChanged("IsCreateNew");
            }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                OnPropertyChanged("IsLoading");
            }
        }

        private string _errorText;
        public string ErrorText
        {
            get { return _errorText; }
            set
            {
                _errorText = value;
                OnPropertyChanged("ErrorText");
            }
        }

        private FacebookAccount _facebookAccount;
        public FacebookAccount FacebookAccount
        {
            get { return _facebookAccount; }
            set
            {
                _facebookAccount = value;
                OnPropertyChanged("FacebookAccount");
            }
        }

        private ICommand _linkAccounts;
        public ICommand LinkAccounts
        {
            get { return _linkAccounts; }
            set
            {
                _linkAccounts = value;
                OnPropertyChanged("LinkAccounts");
            }
        }

        private ICommand _createAccount;
        public ICommand CreateAccount
        {
            get { return _createAccount; }
            set
            {
                _createAccount = value;
                OnPropertyChanged("CreateAccount");
            }
        }

        private ICommand _noThanks;
        public ICommand NoThanks
        {
            get { return _noThanks; }
            set
            {
                _noThanks = value;
                OnPropertyChanged("NoThanks");
            }
        }


        public LinkFacebookAccountViewModel(IAuthenticationService authenticationService, ILocationService locationService, ISettingsService settingsService, IPointOfSaleService pointOfSaleService) : base(locationService, settingsService, pointOfSaleService)
        {
            IsLoading = true;
            _authenticationService = authenticationService;

            NoThanks = new DelegateCommand(() =>
            {
                //Navigator.Instance().NavigateToMenuView(typeof(AccountMenuView));
            });

            CreateAccount = new DelegateCommand(CompleteFacebookSignIn);

            LinkAccounts = new DelegateCommand(() =>
            {
                ErrorText = null;
                CompleteFacebookSignIn(UserName, Password);
            });
        }

        private async void ProcessFacebookSignIn()
        {
            IsBusy = true;
            var ct = CancellationTokenManager.Instance().CreateAndSetCurrentToken();

            FacebookAccount = await _authenticationService.SignInWithFacebook(ct, AccessToken);

            IsCreateNew = FacebookAccount.Linking == FacebookLinking.NotLinked;

            IsLoading = false;
            IsBusy = false;
        }

        private async void CompleteFacebookSignIn()
        {
            IsBusy = true;
            var ct = CancellationTokenManager.Instance().CreateAndSetCurrentToken();

            var response = await _authenticationService.CompleteSignInWithFacebook(ct, FacebookAccount);

            if (response.IsSuccess)
            {
                Navigator.Instance().CloseMenu(response.IsSuccess);
            }
            else
            {
                //Navigator.Instance().NavigateToMenuView(typeof(AccountMenuView));
                //TODO error message
            }

            IsBusy = false;
        }

        private async void CompleteFacebookSignIn(string expediaEmail, string expediaPassword)
        {
            IsBusy = true;
            var ct = CancellationTokenManager.Instance().CreateAndSetCurrentToken();

            var response = await _authenticationService.CompleteSignInWithFacebook(ct, FacebookAccount, expediaEmail, expediaPassword);

            if (response.IsSuccess)
            {
                Navigator.Instance().CloseMenu(response.IsSuccess);
            }
            else
            {
                ErrorText = response.Errors.ErrorInfo;
            }

            IsBusy = false;
        }
    }
}
