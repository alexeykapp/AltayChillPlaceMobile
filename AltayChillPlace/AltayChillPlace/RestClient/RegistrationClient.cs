using AltayChillPlace.ApiResponses;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AltayChillPlace.RestClient
{
    public class RegistrationClient
    {
        private readonly ApiClient _apiClient;
        public RegistrationClient(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<TokenResponse> RegistrationAsync(FormUrlEncodedContent content)
        {
            var response = await _apiClient.HttpClient.PostAsync("user/registration", content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Registration failed");
            }
            var tokenResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TokenResponse>(tokenResponse);
        }
    }
}

