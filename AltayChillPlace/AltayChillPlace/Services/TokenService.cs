using AltayChillPlace.ApiResponses;
using AltayChillPlace.Models.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace AltayChillPlace.Services
{
    public class TokenService
    {
        public async Task SaveTokensAsync(TokenResponse tokenResponse)
        {
            try
            {
                await SecureStorage.SetAsync("AccessToken", tokenResponse.AccessToken);
                await SecureStorage.SetAsync("RefreshToken", tokenResponse.RefreshToken);
                await SecureStorage.SetAsync("IdUser", tokenResponse.User.Id.ToString());
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error save token: {ex.Message}");
            }

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

        public async Task<bool> IsTokenValidAsync()
        {
            try
            {
                string token = await SecureStorage.GetAsync("RefreshToken");

                if (string.IsNullOrEmpty(token))
                {
                    return false;
                }

                if (string.IsNullOrEmpty(token))
                {
                    return false;
                }

                var parts = token.Split('.');
                if (parts.Length != 3) 
                {
                    return false;
                }

                var payload = ConvertFromBase64UrlToBase64String(parts[1]);

                var payloadJson = Encoding.UTF8.GetString(Convert.FromBase64String(payload));
                var payloadData = JsonConvert.DeserializeObject<JwtPayload>(payloadJson);

                var expDate = DateTimeOffset.FromUnixTimeSeconds(payloadData.ExpiresAt);
                return expDate.UtcDateTime > DateTime.UtcNow;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return false;
        }
        private string ConvertFromBase64UrlToBase64String(string base64Url)
        {
            base64Url = base64Url.Replace('-', '+').Replace('_', '/');

            switch (base64Url.Length % 4)
            {
                case 2: base64Url += "=="; break;
                case 3: base64Url += "="; break;
            }

            return base64Url;
        }
        public async Task ReplacementRefreshToken(string refreshToken)
        {

        }
    }
}
