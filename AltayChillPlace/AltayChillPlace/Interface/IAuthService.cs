using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AltayChillPlace.Interface
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(string username, string password);
        Task<bool> LogoutAsync();
        Task<string> GetAccessTokenAsync();
    }
}
