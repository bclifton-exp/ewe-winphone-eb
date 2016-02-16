using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Expedia.Client.Interfaces;
using Expedia.Client.Utilities;
using Expedia.Client.ViewModels;
using Expedia.Injection;

namespace Expedia
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.DataContext = ExpediaKernel.Instance().Get<IMainPageViewModel>();
            var context = DataContext as MainPageViewModel;
            Navigator.Instance().FirstTimeSetup(MenuFrame, HotelFrame, FlightFrame, CarFrame, context);
        }
    }
}
