using System;
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

namespace Expedia.Client.Views
{
    public sealed partial class FlightBookingWebView : Page
    {
        public FlightBookingWebView()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ProgressRing.IsActive = true;
            var sourceUri = e.Parameter as Uri;
            WebView.Source = sourceUri;
        }

        private void WebView_OnLoadCompleted(object sender, NavigationEventArgs e)
        {
            ProgressRing.IsActive = false;
        }
    }
}
