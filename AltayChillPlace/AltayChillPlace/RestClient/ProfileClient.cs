using AltayChillPlace.ApiResponses;
using AltayChillPlace.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AltayChillPlace.RestClient
{
    public class ProfileClient
    {
        private ApiClient _apiClient;
        public ProfileClient(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        public async Task<PersonResponce> GetPersonInfoByPk(string id)
        {
            var response = await _apiClient.HttpClient.GetAsync("profile/info/"+id);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error receiving profile data");
            }
            var profile = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PersonResponce>(profile);
        }
        public async Task<PersonResponce> UpdateProfileAsync(string id, PersonResponce person)
        {
            var json = JsonConvert.SerializeObject(person);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _apiClient.HttpClient.PutAsync("profile/update/" + id, content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Profile update error");
            }
            string responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PersonResponce>(responseBody);
        }
    }
}
