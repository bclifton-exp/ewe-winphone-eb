using System;
using System.Collections.Generic;
using System.Text;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Expedia.Client.Utilities
{
    public class PhoneUIHelper
    {
        private static PhoneUIHelper _instance;
        private Button _burgerMenuButton;

        public static PhoneUIHelper Instance()
        {
            return _instance ?? (_instance = new PhoneUIHelper());
        }

        public void Initialize(Button burgerMenu)
        {
            _burgerMenuButton = burgerMenu;
        }

        public void HideBurgerMenu()
        {
            if(IsPhonePlatform())
                _burgerMenuButton.Visibility = Visibility.Collapsed;
        }

        public void ShowBurgerMenu()
        {
            if(_burgerMenuButton != null)
                _burgerMenuButton.Visibility = Visibility.Visible;
        }

        public static bool IsPhonePlatform()
        {
            var isHardwareButtonsApiPresent = ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons");

            return isHardwareButtonsApiPresent;
        }
    }
}
