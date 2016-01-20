using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.Advertisement;

namespace Expedia.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<bool> SignIn(CancellationToken ct, string email, string password);
        void SignOut();
    }
}
