﻿using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http.Headers;
using Xamarin.Essentials;
using AltayChillPlace.ApiResponses;
using Newtonsoft.Json;
using AltayChillPlace.Services;

namespace AltayChillPlace.HttpClientMiddleware
{
    public class RefreshTokenHandler : DelegatingHandler
    {
        private string _baseAddress;
        private HttpClient _httpClient;
        private TokenService _tokenService;
        private bool isRefreshing = false;

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
            // Получение текущих токенов из SecureStorage
            var accessToken = await SecureStorage.GetAsync("AccessToken");
            var refreshToken = await SecureStorage.GetAsync("RefreshToken");

            if (!string.IsNullOrEmpty(accessToken))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }

            //if (!string.IsNullOrEmpty(refreshToken))
            //{
            //    // Добавление refreshToken в заголовок куки
            //    if (request.Headers.Contains("Cookie"))
            //    {
            //        request.Headers.Remove("Cookie");
            //    }
            //    request.Headers.Add("Cookie", $"refreshToken={refreshToken}");
            //}

            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized && !isRefreshing)
            {
                isRefreshing = true;

                var newAccessToken = await ObtainNewToken();

                // Если не удалось получить новый токен, выбрасываем исключение
                if (newAccessToken == null)
                {
                    isRefreshing = false;
                    return response;
                }

                // Изменение токена в HttpRequest и повторный запрос
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", newAccessToken);

                isRefreshing = false;
                response = await base.SendAsync(request, cancellationToken);
            }

            return response;
        }

        private async Task<string> ObtainNewToken()
        {
            //var refreshToken = await SecureStorage.GetAsync("RefreshToken");
            var refreshToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6NzYsImVtYWlsIjoiYWxsc0Bkc2QucnUiLCJpc0FkbWluIjpmYWxzZSwiaWF0IjoxNzE3NDc0OTY0LCJleHAiOjE3MjAwNjY5NjR9.fwo2bgghHv10RresvKZOjrhhtYy4dUtvrQwM00qJFG4";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{_baseAddress}user/refresh");
            requestMessage.Headers.Add("Cookie", $"refreshToken={refreshToken}");

            var tokenResponse = await _httpClient.SendAsync(requestMessage);

            // Если обновление токена не удалось, возвращаем null
            if (tokenResponse.StatusCode == HttpStatusCode.Unauthorized)
            {
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
