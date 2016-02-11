using System;
using Windows.Foundation.Metadata;
using Windows.Phone.UI.Input;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Expedia.Entities.Suggestions;

namespace Expedia.Client.Utilities
{
    public class Navigator
    {
        private static Navigator _instance;
        private Frame _currentFrame;
        private Frame _hotelFrame;
        private Frame _flightFrame;
        private Frame _carFrame;
        //private Frame _activitiesFrame;
        //private Frame _packagesFrame;

        protected Navigator()
        {
            var currentView = SystemNavigationManager.GetForCurrentView();
            currentView.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            currentView.BackRequested += CurrentViewOnBackRequested;

            var isHardwareButtonsApiPresent = ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons");
            if (isHardwareButtonsApiPresent)
            {
                HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            }
        }

        public static Navigator Instance()
        {
            return _instance ?? (_instance = new Navigator());
        }

        public void FirstTimeSetup(Frame hotelFrame, Frame flightFrame, Frame carFrame)
        {
            _hotelFrame = hotelFrame;
            _flightFrame = flightFrame;
            _carFrame = carFrame;
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            if (_currentFrame != null && _currentFrame.CanGoBack)
            {
                _currentFrame.GoBack();
                e.Handled = true;
            }
        }

        private void CurrentViewOnBackRequested(object sender, BackRequestedEventArgs e)
        {
            if (_currentFrame != null && _currentFrame.CanGoBack)
            {
                _currentFrame.GoBack();
            }
        }

        public void NavigateForward(SuggestionLob lob, Type view)
        {
            switch (lob)
            {
                case SuggestionLob.HOTELS:
                    _hotelFrame.Navigate(view);
                    _currentFrame = _hotelFrame;
                    break;
                case SuggestionLob.FLIGHTS:
                    _flightFrame.Navigate(view);
                    _currentFrame = _flightFrame;
                    break;
                case SuggestionLob.PACKAGES:
                    break;
                case SuggestionLob.CARS:
                    _carFrame.Navigate(view);
                    _currentFrame = _carFrame;
                    break;
                case SuggestionLob.NONE:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(lob), lob, null);
            }
        }
    }
}
