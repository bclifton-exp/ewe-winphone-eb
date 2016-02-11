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

        private void Hotels_OnClick(object sender, RoutedEventArgs e) //TODO these will become account settings, PoS, etc.
        {
            //ShellFrame.Navigate(typeof(SearchHotelsView));
        }

        private void Flights_OnClick(object sender, RoutedEventArgs e)
        {
            //ShellFrame.Navigate(typeof(SearchFlightsView));
        }
    }
}
