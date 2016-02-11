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
using Expedia.Injection;

namespace Expedia.Client.Views
{
    public sealed partial class HotelResultsView : Page
    {
        public HotelResultsView()
        {
            this.DataContext = ExpediaKernel.Instance().Get<IHotelResultsViewModel>();
            this.InitializeComponent();
        }
    }
}