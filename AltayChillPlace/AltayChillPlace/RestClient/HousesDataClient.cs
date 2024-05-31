﻿using System;
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
            var response = await _apiClient.HttpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("GET request failed");
            }

            var houses = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ObservableCollection<HouseResponse>>(houses);
        }
        public async Task<ObservableCollection<TypeHouse>> GetAllTypeAsync()
        {
            var response = await _apiClient.HttpClient.GetAsync("typeHouse/all");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("GET request failed");
            }
            var typesHouse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ObservableCollection<TypeHouse>>(typesHouse);
        }
        public async Task<ObservableCollection<HouseResponse>> GetAvailableHouseAsync(string arrivalDate, string departureDate)
        {
            var response = await _apiClient.HttpClient.GetAsync($"houses/availableHouses?arrival={arrivalDate}&departure={departureDate}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("GET request failed");
            }
            var typesHouse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ObservableCollection<HouseResponse>>(typesHouse);
        }
    }
}
