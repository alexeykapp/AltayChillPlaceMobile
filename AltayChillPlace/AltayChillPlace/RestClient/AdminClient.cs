using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using AltayChillPlace.ApiResponses;
using AltayChillPlace.ApiResponses.Admin;
using Newtonsoft.Json;

namespace AltayChillPlace.RestClient
{
    public class AdminClient
    {
        private ApiClient _adminClient;
        public AdminClient(ApiClient adminClient)
        {
            _adminClient = adminClient;
        }
        public async Task<ObservableCollection<ReservationResponse>> GetAllReservationAsync()
        {
            var response = await _adminClient.HttpClient.GetAsync("admin/reservations");
            if (response == null)
            {
                throw new Exception("Error in receiving booking requests");
            }
            var reservation = await response.Content.ReadAsStringAsync();
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            return JsonConvert.DeserializeObject<ObservableCollection<ReservationResponse>>(reservation,settings);
        }
        public async Task<ObservableCollection<ApplicationStatusResponse>> GetAllApplicationSatus()
        {
            var response = await _adminClient.HttpClient.GetAsync("admin/reservations/statuses");
            if (response == null)
            {
                throw new Exception("Error in obtaining booking statuses");
            }
            var statuses = await response.Content.ReadAsStringAsync();
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            return JsonConvert.DeserializeObject<ObservableCollection<ApplicationStatusResponse>>(statuses, settings);
        }
    }
}
