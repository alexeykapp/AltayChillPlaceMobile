using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using AltayChillPlace.ApiResponses;
using Newtonsoft.Json;

namespace AltayChillPlace.RestClient
{
    public class HousesDataClient
    {
        private readonly ApiClient _apiClient;

        public HousesDataClient(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<ObservableCollection<HouseResponse>> GetDataAsync(string url)
        {
            var response = await _apiClient.HttpClient.GetAsync("houses/all");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("GET request failed");
            }

            var houses = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ObservableCollection<HouseResponse>>(houses);
        }
    }
}
