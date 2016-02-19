using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Controls;
using Expedia.Client.Interfaces;
using Expedia.Services.Interfaces;
using GalaSoft.MvvmLight.Command;

namespace Expedia.Client.ViewModels
{
    public class AccountMenuViewModel : BaseMenuViewModel, IAccountMenuViewModel
    {
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


        public AccountMenuViewModel(ILocationService locationService, ISettingsService settingsService, IPointOfSaleService pointOfSaleService) : base(locationService, settingsService, pointOfSaleService)
        {
           
        }
    }
}
