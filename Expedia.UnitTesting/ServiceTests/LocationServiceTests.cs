using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Expedia.Injection;
using Expedia.Services.Interfaces;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Expedia.UnitTesting.ServiceTests
{
    [TestClass]
    public class LocationServiceTests
    {
        public ILocationService LocationService { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            LocationService = ExpediaKernel.Instance().Get<ILocationService>();
        }

        [TestMethod]
        public async void GetCurrentCoords()
        {
            var coords = await LocationService.GetSetCurrentLocation();
            Assert.IsTrue(coords.Coordinate != null);
        }
    }
}
