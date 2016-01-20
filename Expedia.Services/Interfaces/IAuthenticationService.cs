using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.Advertisement;
using Expedia.Entities.User;

namespace Expedia.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AccountCreationResponse> CreateAccount(CancellationToken ct, AccountCreationParams accountParams);
        Task<bool> SignIn(CancellationToken ct, string email, string password);
        void SignOut();
    }
}
