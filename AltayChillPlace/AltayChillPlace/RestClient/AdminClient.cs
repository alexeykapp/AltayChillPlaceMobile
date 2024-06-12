using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AltayChillPlace.ApiResponses;
using AltayChillPlace.ApiResponses.Admin;
using Newtonsoft.Json;
using Xamarin.Essentials;

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
            return JsonConvert.DeserializeObject<ObservableCollection<ReservationResponse>>(reservation, settings);
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
        public async Task CreateNewApplicationStatusAsync(string idStatus, int idRequest)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("status_id", idStatus),
            });
            var response = await _adminClient.HttpClient.PostAsync($"admin/reservations/createStatus/{idRequest}", content);
            if (response == null || response.IsSuccessStatusCode == false)
            {
                throw new Exception("Error creating a new status");
            }
        }
        public async Task CreateNewPaymentStatusAsync(string idStatus, int idRequest)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("payment_status_id", idStatus),
            });
            var response = await _adminClient.HttpClient.PostAsync($"admin/reservations/createPaymentStatus/{idRequest}", content);
            if (response == null || response.IsSuccessStatusCode == false)
            {
                throw new Exception("Error creating a new status");
            }
        }
        public async Task<NewPostBlogResponse> CreateNewPostBlogAsync(string title, string description, string date, byte[] photo)
        {
            var requestData = new
            {
                title,
                description,
                date,
                photo
            };
            var json = JsonConvert.SerializeObject(requestData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _adminClient.HttpClient.PostAsync("admin/newpost", content);
            if (response == null || !response.IsSuccessStatusCode)
            {
                throw new Exception("Error creating a new post");
            }
            var newPost = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<NewPostBlogResponse>(newPost);
        }
        public async Task DeleteBlogPost(int idPost)
        {
            var response = await _adminClient.HttpClient.DeleteAsync($"admin/delete/post/{idPost}");
            if (response == null || response.IsSuccessStatusCode == false)
            {
                throw new Exception("Error creating a new status");
            }
        }
    }
}
