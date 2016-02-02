using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.Phone.UI.Input;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Expedia.Client.Contracts;
using Expedia.Client.Interfaces;
using Expedia.Client.ViewModels;
using Expedia.Client.Views;
using Expedia.Entities.Hotels;
using Expedia.Entities.Suggestions;
using Expedia.Entities.User;
using Expedia.Injection;
using Expedia.Services;
using Expedia.Services.Interfaces;

namespace Expedia
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.DataContext = ExpediaKernel.Instance().Get<IMainPageViewModel>();

            var currentView = SystemNavigationManager.GetForCurrentView();
            currentView.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            currentView.BackRequested += CurrentViewOnBackRequested;

            var isHardwareButtonsApiPresent = ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons");
            if (isHardwareButtonsApiPresent)
            {
                HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            }

        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                e.Handled = true;
                this.Frame.GoBack();
            }
            else
            {
                Application.Current.Exit();
            }
        }

        private void CurrentViewOnBackRequested(object sender, BackRequestedEventArgs backRequestedEventArgs)
        {
            if (Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
            else
            {
                Application.Current.Exit();
            }
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof (SearchHotelsView));
        }

        private void ButtonBase_OnClick1(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
