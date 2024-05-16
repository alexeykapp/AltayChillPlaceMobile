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
            string token = await SecureStorage.GetAsync("token_key");

            if (string.IsNullOrEmpty(token))
            {
                return false;
            }

            // Разобьем токен на составляющие части
            var parts = token.Split('.');
            if (parts.Length != 3) // JWT должен состоять из 3 частей
            {
                return false;
            }

            var payload = ConvertFromBase64UrlToBase64String(parts[1]);
            // Декодируем полезную нагрузку
            var payloadJson = Encoding.UTF8.GetString(Convert.FromBase64String(payload));
            var payloadData = JsonConvert.DeserializeObject<JwtPayload>(payloadJson);

            var expDate = DateTimeOffset.FromUnixTimeSeconds(payloadData.ExpiresAt);
            return expDate.UtcDateTime > DateTime.UtcNow;
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
