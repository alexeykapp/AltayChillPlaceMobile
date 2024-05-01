using AltayChillPlace.ApiResponses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace AltayChillPlace.Services
{
    public class TokenService
    {
        public async Task SaveTokensAsync(TokenResponse tokenResponse)
        {
            await SecureStorage.SetAsync("AccessToken", tokenResponse.AccessToken);
            await SecureStorage.SetAsync("RefreshToken", tokenResponse.RefreshToken);
        }
        public async Task<string> GetAccessTokenAsync()
        {
            return await SecureStorage.GetAsync("AccessToken");
        }
        public async Task ClearTokensAsync()
        {
            SecureStorage.Remove("AccessToken");
            SecureStorage.Remove("RefreshToken");
        }

        public async Task ReplacementRefreshToken(string refreshToken)
        {

        }
    }
}
