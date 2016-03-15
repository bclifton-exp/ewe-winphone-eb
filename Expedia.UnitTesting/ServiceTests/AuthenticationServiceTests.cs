using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Expedia.Entities.User;
using Expedia.Injection;
using Expedia.Services.Interfaces;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Expedia.UnitTesting.ServiceTests
{
    [TestClass]
    public class AuthenticationServiceTests
    {
        public IAuthenticationService AuthenticationService { get; set; }
        public ISettingsService SettingsService { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            AuthenticationService = ExpediaKernel.Instance().Get<IAuthenticationService>();
            SettingsService = ExpediaKernel.Instance().Get<ISettingsService>();
        }

        [TestMethod]
        public async Task CanCreateUserAccount()
        {
            var createResult = await AuthenticationService.CreateAccount(new CancellationToken(false), new AccountCreationParams("WpAppTester@expedia.com", "expedia", "Test", "Test", false));
            Assert.IsTrue(createResult.Errors.First().ErrorInfo.Summary.Contains("same email"));
        }

        [TestMethod]
        public async Task CanSignIn()
        {
            var signInResult = await AuthenticationService.SignIn(new CancellationToken(false), "WpAppTester@expedia.com", "expedia");
            Assert.IsTrue(signInResult.IsSuccess);

            var accountName = SettingsService.GetRealName();
            Assert.IsTrue(accountName == "Test Test");
        }
    }
}
