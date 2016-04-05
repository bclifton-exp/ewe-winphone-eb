using System;
using Windows.Foundation.Metadata;
using Windows.Phone.UI.Input;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Expedia.Client.ViewModels;
using Expedia.Client.Views;
using Expedia.Entities.Entities;
using Expedia.Entities.Suggestions;

namespace Expedia.Client.Utilities
{
    public class Navigator
    {
        private static Navigator _instance;
        private Frame _currentFrame;
        private Frame _menuFrame;
        private Frame _hotelFrame;
        private Frame _flightFrame;
        private Frame _carFrame;

        private MainPageViewModel _mainViewModel;
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

        public void SetCurrentFrame(LineOfBusiness lob)
        {
            switch (lob)
            {
                case LineOfBusiness.HOTELS:
                    _currentFrame = _hotelFrame;
                    return;

                case LineOfBusiness.FLIGHTS:
                    _currentFrame = _flightFrame;
                    return;

                case LineOfBusiness.CARS:
                    _currentFrame = _carFrame;
                    return;

                default:
                    return;
            }
        }

        public void FirstTimeSetup(Frame menuFrame, Frame hotelFrame, Frame flightFrame, Frame carFrame, MainPageViewModel mainViewModel)
        {
            _hotelFrame = hotelFrame;
            _flightFrame = flightFrame;
            _carFrame = carFrame;
            _menuFrame = menuFrame;
            _mainViewModel = mainViewModel;
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            if (_mainViewModel.IsMenuFrameVisible)
            {
                if (_menuFrame.CurrentSourcePageType == typeof(CreateAccountView))
                {
                    _menuFrame.GoBack();
                    e.Handled = true;
                }
                else
                {
                    _mainViewModel.IsMenuFrameVisible = false;
                    e.Handled = true;
                }
            }
            else
            {
                if (_currentFrame != null && _currentFrame.CanGoBack)
                {
                    CancellationTokenManager.Instance().CancelCurrentToken();
                    _currentFrame.GoBack();
                    e.Handled = true;
                }
            }
        }

        private void CurrentViewOnBackRequested(object sender, BackRequestedEventArgs e)
        {
            if (_mainViewModel.IsMenuFrameVisible)
            {
                if (_menuFrame.CurrentSourcePageType == typeof (CreateAccountView))
                {
                    _menuFrame.GoBack();
                }
                else
                {
                    _mainViewModel.IsMenuFrameVisible = false;
                }
                e.Handled = true;
            }
            else
            {
                if (_currentFrame != null && _currentFrame.CanGoBack)
                {
                    CancellationTokenManager.Instance().CancelCurrentToken();
                    _currentFrame.GoBack();
                    e.Handled = true;
                }
            }
        }

        public void NavigateForward(SuggestionLob lob, Type view, object param = null)
        {
            switch (lob)
            {
                case SuggestionLob.HOTELS:
                    _hotelFrame.Navigate(view, param);
                    _currentFrame = _hotelFrame;
                    break;
                case SuggestionLob.FLIGHTS:
                    _flightFrame.Navigate(view, param);
                    _currentFrame = _flightFrame;
                    break;
                case SuggestionLob.PACKAGES:
                    break;
                case SuggestionLob.CARS:
                    _carFrame.Navigate(view, param);
                    _currentFrame = _carFrame;
                    break;
                case SuggestionLob.NONE:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(lob), lob, null);
            }
        }

        public void NavigateToMenuView(Type view, object param = null)
        {
            _menuFrame.Navigate(view, param);
        }

        public void CloseMenu(bool isSignedIn = false)
        {
            _mainViewModel.IsSignedIn = isSignedIn;
            _mainViewModel.IsMenuFrameVisible = false;
        }
    }
}
