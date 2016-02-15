using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Expedia.Client.Interfaces;
using Expedia.Client.ViewModels;
using Expedia.Entities.Hotels;
using Expedia.Injection;

namespace Expedia.Client.Views
{
    public sealed partial class HotelResultsView : Page
    {
        public HotelResultsView()
        {
            this.DataContext = ExpediaKernel.Instance().Get<IHotelResultsViewModel>();
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var hotelParams = e.Parameter as SearchHotelsLocalParameters;
            var context = DataContext as HotelResultsViewModel;
            context.GetHotelResults(hotelParams);
        }
    }
}
