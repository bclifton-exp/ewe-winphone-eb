using System;
using System.Collections.Generic;
using System.Text;
using Windows.Devices.Geolocation;
using Expedia.Client.Interfaces;
using Expedia.Services.Interfaces;

namespace Expedia.Client.ViewModels
{
    public class MainPageViewModel : IMainPageViewModel
    {
        private ISettingsService _settingsService { get; set; }
        private ILocationService _locationService { get; set; }

        public MainPageViewModel(ISettingsService settingsService, ILocationService locationService)
        {
            _settingsService = settingsService;
            _locationService = locationService;
        }

    }
}
