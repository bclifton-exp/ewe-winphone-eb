using System;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Expedia.Client.Interfaces;
using Expedia.Client.Utilities;
using Expedia.Client.ViewModels;
using Expedia.Entities.Cars;
using Expedia.Injection;

namespace Expedia.Client.Views
{
    public sealed partial class SignInView : Page
    {
        public SignInView()
        {
            this.InitializeComponent();
            this.DataContext = ExpediaKernel.Instance().Get<ISignInViewModel>();
            Navigator.Instance().SetSignInViewModel(this.DataContext as SignInViewModel);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var context = DataContext as SignInViewModel;
            context.IsBusy = false;
        }
    }
}
