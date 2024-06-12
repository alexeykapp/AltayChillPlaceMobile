using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http.Headers;
using Xamarin.Essentials;
using AltayChillPlace.ApiResponses;
using Newtonsoft.Json;
using AltayChillPlace.Services;
using AltayChillPlace.NavigationFile;
using AltayChillPlace.Views;

namespace AltayChillPlace.HttpClientMiddleware
{
    public class RefreshTokenHandler : DelegatingHandler
    {
        private string _baseAddress;
        private HttpClient _httpClient;
        private TokenService _tokenService;
        private bool isRefreshing = false;
        private int _countIsRefreshing = 0;

        public RefreshTokenHandler(HttpMessageHandler innerHandler, string baseAddress)
        : base(innerHandler)
        {
            this._baseAddress = baseAddress;
            this._tokenService = new TokenService();
        }

        public void SetHttpClient(HttpClient client)
        {
            this._httpClient = client;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var accessToken = await SecureStorage.GetAsync("AccessToken");
            if (!string.IsNullOrEmpty(accessToken))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }

            var response = await base.SendAsync(request, cancellationToken);
            if (response.StatusCode == HttpStatusCode.Unauthorized && !isRefreshing)
            {
                isRefreshing = true;
                var newAccessToken = await ObtainNewToken();

                if (newAccessToken == null)
                {
                    isRefreshing = false;
                    await _tokenService.LogOut();
                    return response;
                }

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", newAccessToken);

                isRefreshing = false;
                _countIsRefreshing = 0;
                response = await base.SendAsync(request, cancellationToken);
            }

            return response;
        }

        private async Task<string> ObtainNewToken()
        {
            var refreshToken = await SecureStorage.GetAsync("RefreshToken");
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{_baseAddress}user/refresh");
            requestMessage.Headers.Add("Cookie", $"refreshToken={refreshToken}");

            var tokenResponse = await _httpClient.SendAsync(requestMessage);

            if (tokenResponse.StatusCode == HttpStatusCode.Unauthorized)
            {
                await NavigationDispatcher.Instance.PushAndRemovePreviousAsync(new Autorization());
                return null;
            }

            tokenResponse.EnsureSuccessStatusCode();

            var tokenResponseString = await tokenResponse.Content.ReadAsStringAsync();
            var newTokenResult = JsonConvert.DeserializeObject<TokenResponse>(tokenResponseString);
            await _tokenService.SaveTokensAsync(newTokenResult);

            return newTokenResult.AccessToken;
        }
    }
}
