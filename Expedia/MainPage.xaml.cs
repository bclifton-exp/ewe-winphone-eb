using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Expedia.Client.Interfaces;
using Expedia.Client.Utilities;
using Expedia.Injection;

namespace Expedia
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.DataContext = ExpediaKernel.Instance().Get<IMainPageViewModel>();
            Navigator.Instance().FirstTimeSetup(HotelFrame, FlightFrame, CarFrame);
        }
    }
}
