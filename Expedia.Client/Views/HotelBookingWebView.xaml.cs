using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Expedia.Client.Views
{
    public sealed partial class HotelBookingWebView : Page
    {
        public HotelBookingWebView()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ProgressRing.IsActive = true;
            var sourceUri = e.Parameter as Uri;
            WebView.Source = sourceUri;
        }

        private void WebView_OnNavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
           
        }

        private void WebView_OnLoadCompleted(object sender, NavigationEventArgs e)
        {
            ProgressRing.IsActive = false;
        }
    }
}
