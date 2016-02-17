using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Expedia.Client.Interfaces;
using Expedia.Client.ViewModels;
using Expedia.Injection;

namespace Expedia.Client.Views
{
    public sealed partial class SettingsMenuView : Page
    {
        public SettingsMenuView()
        {
            this.DataContext = ExpediaKernel.Instance().Get<ISettingsMenuViewModel>();
            this.InitializeComponent();
        }
    }
}
