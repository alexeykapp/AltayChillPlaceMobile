using AltayChillPlace.ApiResponses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft;

namespace AltayChillPlace.RestClient
{
    public class AuthClient
    {
        private readonly ApiClient _apiClient;

        public AuthClient(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<TokenResponse> AuthenticateAsync(string login, string password)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("email", login),
                new KeyValuePair<string, string>("password", password)
            });
            Debug.WriteLine($"Login: {login}, Password: {password}");
            var response = await _apiClient.HttpClient.PostAsync("user/login", content);
            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine($"Error - {response.RequestMessage}");
                throw new Exception("Authentication failed");
            }

            var tokenResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TokenResponse>(tokenResponse);
        }
    }
}
