using System;
using System.Collections.Generic;
using System.Text;
using Expedia.Client.Extensions;
using Expedia.Client.Interfaces;
using Expedia.Client.Utilities;
using Expedia.Entities.User;
using Expedia.Services.Interfaces;
using GalaSoft.MvvmLight.Command;

namespace Expedia.Client.ViewModels
{
    public class CreateAccountViewModel : BaseMenuViewModel, ICreateAccountViewModel
    {
        private IAuthenticationService _authenticationService { get; set; }

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

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged("Email");
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                ObservePasswordChange();
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

        private bool _termsAgreed;
        public bool TermsAgreed
        {
            get { return _termsAgreed; }
            set
            {
                _termsAgreed = value;
                OnPropertyChanged("TermsAgreed");
            }
        }

        private bool _emailOptIn;
        public bool EmailOptIn
        {
            get { return _emailOptIn; }
            set
            {
                _emailOptIn = value;
                OnPropertyChanged("EmailOptIn");
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


        public CreateAccountViewModel(IAuthenticationService authenticationService, ILocationService locationService, ISettingsService settingsService, IPointOfSaleService pointOfSaleService) : base(locationService, settingsService, pointOfSaleService)
        {
            _authenticationService = authenticationService;
            EmailOptIn = true;

            CreateAccount = new DependentRelayCommand(CreateNewAccount, CanExecuteCreateAccount, this, ()=>Email, ()=>FirstName, ()=>LastName, ()=>TermsAgreed, ()=>Password, ()=>PasswordsMatch);
        }

        private async void CreateNewAccount()
        {
            var ct = CancellationTokenManager.Instance().CreateAndSetCurrentToken();
            var accountParams = new AccountCreationParams(Email, Password, FirstName, LastName, EmailOptIn);

            IsBusy = true;
            var response = await _authenticationService.CreateAccount(ct, accountParams);
            IsBusy = false;
            //TODO error catching etc.
            Navigator.Instance().CloseMenu();
        }

        private bool CanExecuteCreateAccount()
        {
            return !string.IsNullOrEmpty(Email) 
                && PasswordsMatch && Password.Length > 5 
                && !string.IsNullOrEmpty(FirstName) 
                && !string.IsNullOrEmpty(LastName) 
                && TermsAgreed;
        }

        private void ObservePasswordChange()
        {
            PasswordsMatch = Password == ConfirmPassword;
        }
    }
}
