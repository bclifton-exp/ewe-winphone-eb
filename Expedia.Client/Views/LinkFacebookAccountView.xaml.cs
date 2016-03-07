using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Expedia.Client.Interfaces;
using Expedia.Client.ViewModels;
using Expedia.Injection;

namespace Expedia.Client.Views
{
    public sealed partial class LinkFacebookAccountView : Page
    {
        public LinkFacebookAccountView()
        {
            this.InitializeComponent();
            this.DataContext = ExpediaKernel.Instance().Get<ILinkFacebookAccountViewModel>();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var accessToken = e.Parameter;
            var viewModel = this.DataContext as LinkFacebookAccountViewModel;
            if (accessToken != null) viewModel.AccessToken = accessToken.ToString();
        }
    }
}
