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

        public ApiClient(string baseAdress)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseAdress),
                Timeout = TimeSpan.FromSeconds(120),
                DefaultRequestHeaders = { Accept = { new MediaTypeWithQualityHeaderValue("application/json") } }
            };
        }
    }
}
