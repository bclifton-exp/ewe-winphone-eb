﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Expedia.Client.Interfaces;
using Expedia.Client.ViewModels;
using Expedia.Injection;

namespace Expedia.Client.Views
{
    public sealed partial class AccountMenuView : Page
    {
        public AccountMenuView()
        {
            this.DataContext = ExpediaKernel.Instance().Get<IAccountMenuViewModel>();
            this.InitializeComponent();
        }

        private void SignIn_OnClick(object sender, RoutedEventArgs e)
        {
            var context = DataContext as AccountMenuViewModel;
            context.SignInUser();
        }

        
    }
}