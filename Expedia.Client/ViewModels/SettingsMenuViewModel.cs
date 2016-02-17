using System.Collections.ObjectModel;
using System.Linq;
using Windows.Devices.Geolocation;
using Expedia.Client.Interfaces;
using Expedia.Entities.AppEnvContext;
using Expedia.Entities.Extensions;
using Expedia.Services;
using Expedia.Services.Interfaces;

namespace Expedia.Client.ViewModels
{
    public class SettingsMenuViewModel : BaseMenuViewModel, ISettingsMenuViewModel
    {

        private GeolocationAccessStatus _useLocationService;
        public GeolocationAccessStatus UseLocationService
        {
            get { return _useLocationService; }
            set
            {
                _useLocationService = value;
                SettingsService.SetUseLocationService(UseLocationService);
                OnPropertyChanged("UseLocationService");
            }
        }

        private PointOfSale _selectedPoS;
        public PointOfSale SelectedPoS
        {
            get { return _selectedPoS; }
            set
            {
                _selectedPoS = value;
                PointOfSaleService.SetCurrentPointOfSale(value.CountryId);
                OnPropertyChanged("SelectedPoS");
            }
        }

        private ObservableCollection<PointOfSale> _pointsOfSale;
        public ObservableCollection<PointOfSale> PointsOfSale
        {
            get { return _pointsOfSale; }
            set
            {
                _pointsOfSale = value;
                OnPropertyChanged("PointsOfSale");
            }
        }



        public SettingsMenuViewModel(ILocationService locationService, ISettingsService settingsService, IPointOfSaleService pointOfSaleService) : base(locationService, settingsService, pointOfSaleService)
        {
            UseLocationService = SettingsService.GetUseLocationService();
            SetPointsOfSale();
        }

        private async void SetPointsOfSale()
        {
            var posItems = await PointOfSaleService.GetPointsOfSale();
            PointsOfSale = posItems.ToObservableCollection();
            var currentPoS = await PointOfSaleService.GetCurrentPointOfSale();
            SelectedPoS = PointsOfSale.First(p => p.CountryId == currentPoS.CountryId);
        }
    }
}
