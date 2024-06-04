using AltayChillPlace.HttpClientMiddleware;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace AltayChillPlace.RestClient
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;
        public HttpClient HttpClient => _httpClient;

        public ApiClient(string baseAddress)
        {
            var handler = new HttpClientHandler();
            var refreshTokenHandler = new RefreshTokenHandler(handler, baseAddress);

            _httpClient = new HttpClient(refreshTokenHandler)
            {
                BaseAddress = new Uri(baseAddress),
                Timeout = TimeSpan.FromSeconds(120)
            };

            refreshTokenHandler.SetHttpClient(_httpClient);

            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
