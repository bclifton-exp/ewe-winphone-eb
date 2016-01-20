using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Expedia.Services;
using Expedia.Services.Interfaces;
using Ninject.Modules;

namespace Expedia.Injection
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IHotelService>().To<HotelService>();
            Bind<ISettingsService>().To<SettingsService>();
            Bind<ISuggestionService>().To<SuggestionService>();
            Bind<IPointOfSaleService>().To<PointOfSaleService>();
            Bind<IAuthenticationService>().To<AuthenticationService>();
        }
    }
}
