using AltayChillPlace.ApiResponses.Booking;
using AltayChillPlace.RestClientModel;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AltayChillPlace.RestClient
{
    public class BookingClient
    {
        private readonly ApiClient _apiClient;
        public BookingClient(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        public async Task<BookingResponce> CreateNewBooking(RootBoking root)
        {
            string json = JsonConvert.SerializeObject(root);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _apiClient.HttpClient.PostAsync("user/booking/create", content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("GET request failed");
            }

            var houses = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<BookingResponce>(houses);
        }
    }
}
