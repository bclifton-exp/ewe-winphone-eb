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
    public class PointOfSaleServiceTests
    {
        public IPointOfSaleService PointOfSaleService { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            PointOfSaleService = ExpediaKernel.Instance().Get<IPointOfSaleService>();
        }


        [TestMethod]
        public async Task SetCurrentPointOfSale()
        {
            var testCountryId = "GB";
            await PointOfSaleService.SetCurrentPointOfSale(testCountryId);
            var currentlySetPos = await PointOfSaleService.GetCurrentPointOfSale();

            Assert.IsTrue(currentlySetPos.CountryId == testCountryId);
        }
    }
}
