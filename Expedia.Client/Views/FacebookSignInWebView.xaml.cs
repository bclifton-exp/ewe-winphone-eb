using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Expedia.Client.Utilities;

namespace Expedia.Client.Views
{
    public sealed partial class FacebookSignInWebView : Page
    {
        private string _accessToken;

        public FacebookSignInWebView()
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
            if (args.Uri.ToString().StartsWith("https://www.facebook.com/connect/login_success.html"))
            {
                _accessToken = args.Uri.Fragment.Split('&')[0].Replace("#access_token=", "");
                Navigator.Instance().NavigateToMenuView(typeof(LinkFacebookAccountView),_accessToken);
            }
        }

        private void WebView_OnLoadCompleted(object sender, NavigationEventArgs e)
        {
            ProgressRing.IsActive = false;
        }
    }
}
