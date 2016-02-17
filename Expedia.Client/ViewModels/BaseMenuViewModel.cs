using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Expedia.Services.Interfaces;

namespace Expedia.Client.ViewModels
{
    public abstract class BaseMenuViewModel : INotifyPropertyChanged
    {
        public ILocationService LocationService;
        public ISettingsService SettingsService;
        public IPointOfSaleService PointOfSaleService;

        protected BaseMenuViewModel(ILocationService locationService, ISettingsService settingsService, IPointOfSaleService pointOfSaleService)
        {
            LocationService = locationService;
            SettingsService = settingsService;
            PointOfSaleService = pointOfSaleService;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
