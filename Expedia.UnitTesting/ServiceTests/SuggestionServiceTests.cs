using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Expedia.Entities.Suggestions;
using Expedia.Injection;
using Expedia.Services.Interfaces;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Expedia.UnitTesting.ServiceTests
{
    [TestClass]
    public class SuggestionServiceTests
    {
        public ISuggestionService SuggestionService { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            SuggestionService = ExpediaKernel.Instance().Get<ISuggestionService>();
        }

        [TestMethod]
        public async Task GetSuggestions()
        {
            var suggestionResults = await SuggestionService.Suggest(new CancellationToken(false), "Detroit", SuggestionLob.HOTELS );

            Assert.IsTrue(suggestionResults.IsSuccess);
            Assert.IsTrue(suggestionResults.Suggestions.First().Display != null);
        }

        [TestMethod]
        public async Task GetNearbySuggestions()
        {
            var nearbySuggestionResults = await SuggestionService.Suggest(new CancellationToken(false), 40.440625, -79.995886, SuggestionLob.HOTELS);

            Assert.IsTrue(nearbySuggestionResults.IsSuccess);
            Assert.IsTrue(nearbySuggestionResults.Suggestions.First().Display != null);
        }
    }
}
