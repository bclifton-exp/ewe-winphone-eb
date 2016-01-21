using System.Linq;
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
        public async Task GetAndSetCurrentPointOfSale()
        {
            var testCountryId = "US";
            await PointOfSaleService.SetCurrentPointOfSale(testCountryId);
            var currentlySetPos = await PointOfSaleService.GetCurrentPointOfSale();

            Assert.IsTrue(currentlySetPos.CountryId == testCountryId);
        }

        [TestMethod]
        public async Task GetCurrentCulture()
        {
            var cultureCode = await PointOfSaleService.GetCurrentCulture();

            Assert.IsNotNull(cultureCode);
        }

        [TestMethod]
        public async Task GetCultures()
        {
            var cultures = await PointOfSaleService.GetCultures();

            Assert.IsNotNull(cultures.First().CultureCode);
        }
    }
}
