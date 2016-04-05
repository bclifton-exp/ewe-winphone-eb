using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Expedia.Client.Interfaces;
using Expedia.Client.Utilities;
using Expedia.Client.ViewModels;
using Expedia.Entities.Entities;
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


        private void PivotControl_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var pivotControl = sender as Pivot;
            var selectedPivot = (pivotControl.SelectedItem as PivotItem).Name;

            switch (selectedPivot)
            {
                case "HotelPivot":
                    Navigator.Instance().SetCurrentFrame(LineOfBusiness.HOTELS);
                    return;

                case "FlightPivot":
                    Navigator.Instance().SetCurrentFrame(LineOfBusiness.FLIGHTS);
                    return;

                case "CarPivot":
                    Navigator.Instance().SetCurrentFrame(LineOfBusiness.CARS);
                    return;

                default:
                    Navigator.Instance().SetCurrentFrame(LineOfBusiness.HOTELS);
                    return;
            }
        }
    }
}
