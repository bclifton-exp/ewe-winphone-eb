using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Expedia.Entities.Hotels;
using Expedia.Injection;
using Expedia.Services.Interfaces;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Expedia.UnitTesting.ServiceTests
{
    [TestClass]
    public class HotelServiceTests
    {
        public IHotelService HotelService { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            HotelService = ExpediaKernel.Instance().Get<IHotelService>();
        }

        [TestMethod]
        public async Task GetHotels()
        {
            var results = await HotelService.GetHotels(new HotelSearchQueryParameters{ CheckInDate = DateTime.Today, CheckOutDate = DateTime.Today.AddDays(1), City = "SFO", Room = new[] { 1 } }, new CancellationToken(false));

            Assert.IsTrue(results.HotelList.First().HotelId != null);
        }
    }
}
