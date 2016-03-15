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
            var results = await HotelService.SearchHotels(new CancellationToken(false),new SearchHotelsLocalParameters{ CheckInDate = DateTime.Today, CheckOutDate = DateTime.Today.AddDays(1), LocationName = "SFO", RoomsCount = 1 });

            Assert.IsTrue(results.Hotels.First().HotelId != null);
        }
    }
}
