using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Expedia.Client.Views
{
    public sealed partial class ForgotPasswordWebView : Page
    {
        private string _accessToken;

        public ForgotPasswordWebView()
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
            ProgressRing.IsActive = false;
        }
    }
}
