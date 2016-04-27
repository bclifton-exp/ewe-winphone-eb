using Windows.UI.Xaml.Controls;
using Expedia.Client.Interfaces;
using Expedia.Client.Utilities;
using Expedia.Client.ViewModels;
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
    }
}
