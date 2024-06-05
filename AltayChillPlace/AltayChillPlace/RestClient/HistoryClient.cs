using AltayChillPlace.ApiResponses;
using AltayChillPlace.ApiResponses.History;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace AltayChillPlace.RestClient
{
    public class HistoryClient
    {
        private readonly ApiClient _apiClient;
        public HistoryClient(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        public async Task<ObservableCollection<BookingHistory>> GetBookingHistoryAsync(string id)
        {
            var response = await _apiClient.HttpClient.GetAsync($"booking/history/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error receiving posts");
            }

            var history = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ObservableCollection<BookingHistory>>(history);
        }
    }
}
