using System.Threading;
using System.Threading.Tasks;
using Expedia.Client;
using Expedia.Entites;
using Expedia.Entities.User;
using Expedia.Services.Base;
using Expedia.Services.Interfaces;
using Expedia.Services.Query;
using Newtonsoft.Json;

namespace Expedia.Services
{
    public class AuthenticationService : BaseService, IAuthenticationService
    {
        private ISettingsService _settingsService;

        public AuthenticationService(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        public async Task<bool> SignIn(CancellationToken ct, string email, string password)
        {
            var response = await Authenticate(ct, email, password);

            return await ProcessSignInResponse(response, ct);
        }

        public void SignOut()
        {
            _settingsService.ClearUserInfo();
        }

        public async Task<AccountCreationResponse> CreateAccount(CancellationToken ct, AccountCreationParams accountParams)
        {
            var urlBase = string.Format(Constants.Urls.BaseUrlFormat, _settingsService.GetCurrentDomain());
            var request = new ApiRequest(urlBase);
            request.AppendPath(Constants.Urls.UserApiRoot);
            request.AppendPath("create");
            request.PayloadParam("email", accountParams.Email);
            request.PayloadParam("password", accountParams.Password);
            request.PayloadParam("firstName", accountParams.FirstName);
            request.PayloadParam("lastName", accountParams.LastName);
            request.PayloadParam("middleName", accountParams.MiddleName);
            request.PayloadParam("expediaEmailOptIn", accountParams.ExpediaEmailOptIn.ToString());

            var result = await ExecutePost(request.GetFullUri(), request.GetPayloadContent(), ct);

            var response = result != null ? JsonConvert.DeserializeObject<AccountCreationResponse>(result) : null;

            var realName = response.FirstName.ToTitleCase() + " " + response.LastName.ToTitleCase();
            _settingsService.SetUserInfo(long.Parse(response.TUID), realName);

            return response;
        }

        private async Task<SignInResponse> Authenticate(CancellationToken ct, string email, string password)
        {
            var urlBase = string.Format(Constants.Urls.BaseUrlFormat, _settingsService.GetCurrentDomain());
            var request = new ApiRequest(urlBase);
            request.AppendPath(Constants.Urls.UserApiRoot);
            request.AppendPath("sign-in");
            request.PayloadParam("email", email);
            request.PayloadParam("password", password);
            request.PayloadParam("staySignedIn", "true");

            var result = await ExecutePost(request.GetFullUri(),request.GetPayloadContent(),ct);

            return result != null ? JsonConvert.DeserializeObject<SignInResponse>(result) : null;
        }

        private async Task<bool> ProcessSignInResponse(SignInResponse response, CancellationToken ct)
        {
            if (!response.IsSuccess)
            {
                SignOut();
            }

            var realName = response.FirstName.ToTitleCase() + " " + response.LastName.ToTitleCase();
            _settingsService.SetUserInfo(response.TUid,realName);

            return response.IsSuccess;
        }

        //     public Task SignInWithFacebook(CancellationToken ct) //TODO webview-ish implementation
   //     {
   //         var callback = new Uri(Constants.Facebook.RedirectUrl, UriKind.RelativeOrAbsolute);

        //         var uri = new Uri(
        //             Constants.Facebook.ConnectUrl.InvariantCultureFormat(
        //                 Constants.Facebook.ExpediaClientAppId,
        //                 Uri.EscapeDataString(callback.ToString()),
        //                 Constants.Facebook.ResponseType,
        //                 Constants.Facebook.Scope),
        //                 UriKind.Absolute);


        //         //Force error since we won't get any from authenticationBroker
        //         if (!NetworkInterface.GetIsNetworkAvailable())
        //         {
        //             //Error message flyout is generic and includes connection verification
        //             throw new Exception();
        //         }

        //return _dispatcherScheduler.Run(async ict => WebAuthenticationBroker.AuthenticateAndContinue(uri, callback), ct);

        //     }

    }
}
