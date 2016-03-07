using System;
using System.Net.NetworkInformation;
using System.Reactive.Concurrency;
using System.Threading;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;
using Expedia.Business.Entities;
using Expedia.Client;
using Expedia.Entites;
using Expedia.Entities.Extensions;
using Expedia.Entities.User;
using Expedia.Services.Base;
using Expedia.Services.Interfaces;
using Expedia.Services.Query;
using Facebook;
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

        public async Task<bool> VerifySignIn(CancellationToken ct)
        {
            var response = await VerifyAuthentication(ct);

            return await ProcessSignInResponse(response, ct);
        }

        public async Task<SignInResponse> VerifyAuthentication(CancellationToken ct)
        {
            var urlBase = string.Format(Constants.Urls.BaseUrlFormat, _settingsService.GetCurrentDomain());
            var request = new ApiRequest(urlBase);
            request.AppendPath(Constants.Urls.UserApiRoot);
            request.AppendPath("sign-in");
            request.PayloadParam("profileOnly", "true");

            var result = await ExecutePost(request.GetFullUri(), request.GetPayloadContent(), ct);

            return result != null ? JsonConvert.DeserializeObject<SignInResponse>(result) : null;
        }

        public async Task<FacebookAccount> SignInWithFacebook(CancellationToken ct, string accessToken)
        {
            var fbClient = new FacebookClient(accessToken);
            dynamic result = await fbClient.GetTaskAsync("me", new { fields = "name,id,email" }, ct);

            var linkingStatus = await GetFacebookLinking(ct, result.id, accessToken);

            var facebookAccount = new FacebookAccount
            {
                AccessToken = accessToken,
                Email = result.email,
                Name = result.name,
                UserId = result.id,
                Linking = linkingStatus
            };

            return facebookAccount;
        }

        public async Task<bool> CompleteSignInWithFacebook(CancellationToken ct, FacebookAccount account, string expediaEmail = null, string expediaPassword = null)
        {
            bool canVerify = false;

            switch (account.Linking)
            {
                case FacebookLinking.NotLinked:
                    var response = await CreateWithFacebook(ct, account.UserId, account.AccessToken, account.Email);
                    canVerify = response.Status.Equals("success", StringComparison.OrdinalIgnoreCase);
                    break;

                case FacebookLinking.Existing:
                    response = await LinkWithFacebook(ct, account.UserId, account.AccessToken, expediaEmail, expediaPassword);
                    canVerify = response.Status.Equals("success", StringComparison.OrdinalIgnoreCase);
                    break;

                case FacebookLinking.Linked:
                    canVerify = true;
                    break;
            }

            if (!canVerify)
            {
                throw new AuthenticationException();
            }

            return await VerifySignIn(ct);
        }

        private async Task<LinkAccountResponse> CreateWithFacebook(CancellationToken ct, string facebookUserId, string facebookAccessToken, string facebookEmail)
        {
            var urlBase = string.Format(Constants.Urls.BaseUrlFormat, _settingsService.GetCurrentDomain());
            var request = new ApiRequest(urlBase);
            request.AppendPath(Constants.Urls.AuthenticationApiRoot);
            request.AppendSearchQuery("linkNewAccount");
            request.AppendParam("accessToken", facebookAccessToken);
            request.AppendParam("email",facebookEmail);
            request.AppendParam("provider", "facebook");
            request.AppendParam("userId", facebookUserId);

            var response = await ExecuteGet(request.GetFullUri(), ct);
            return JsonConvert.DeserializeObject<LinkAccountResponse>(response);
        }

        public async Task<LinkAccountResponse> LinkWithFacebook(CancellationToken ct, string facebookUserId, string facebookAccessToken, string expediaEmail, string expediaPassword)
        {
            var urlBase = string.Format(Constants.Urls.BaseUrlFormat, _settingsService.GetCurrentDomain());
            var request = new ApiRequest(urlBase);
            request.AppendPath(Constants.Urls.AuthenticationApiRoot);
            request.AppendSearchQuery("linkExistingAccount");
            request.AppendParam("provider", "Facebook");
            request.AppendParam("userId", facebookUserId);
            request.AppendParam("accessToken", facebookAccessToken);
            request.AppendParam("email", expediaEmail);
            request.AppendParam("password", expediaPassword);

            var response = await ExecuteGet(request.GetFullUri(), ct);
            return JsonConvert.DeserializeObject<LinkAccountResponse>(response);
        }

        private async Task<FacebookLinking> GetFacebookLinking(CancellationToken ct, string facebookUserId, string facebookAccessToken)
        {
            var urlBase = string.Format(Constants.Urls.BaseUrlFormat, _settingsService.GetCurrentDomain());
            var request = new ApiRequest(urlBase);
            request.AppendPath(Constants.Urls.AuthenticationApiRoot);
            request.AppendSearchQuery("autologin");
            request.AppendParam("accessToken",facebookAccessToken);
            request.AppendParam("provider", "facebook");
            request.AppendParam("userId",facebookUserId);

            var response = await ExecuteGet(request.GetFullUri(), ct);
            var result = JsonConvert.DeserializeObject<AutoLoginResponse>(response);

            if (result.Status.Equals(Constants.LinkingStatus.FacebookNotFound, StringComparison.OrdinalIgnoreCase)
                || result.Status.Equals(Constants.LinkingStatus.FacebookNotLinked, StringComparison.OrdinalIgnoreCase))
            {
                return FacebookLinking.NotLinked;
            }
            else if (result.Status.Equals(Constants.LinkingStatus.FacebookExisting, StringComparison.OrdinalIgnoreCase))
            {
                return FacebookLinking.Existing;
            }
            else if (result.Status.Equals(Constants.LinkingStatus.FacebookLinked, StringComparison.OrdinalIgnoreCase))
            {
                return FacebookLinking.Linked;
            }
            else if (result.Status.Equals(Constants.LinkingStatus.FacebookError, StringComparison.OrdinalIgnoreCase))
            {
                return FacebookLinking.Error;
            }
            else if (result.Status.Equals(Constants.LinkingStatus.FacebookLinkedWithOther, StringComparison.OrdinalIgnoreCase))
            {
                return FacebookLinking.ErrorLinkedWithOther;
            }

            return FacebookLinking.Unknown;
        }

    }
}
