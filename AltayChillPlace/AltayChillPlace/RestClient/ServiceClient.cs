using AltayChillPlace.ApiResponses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace AltayChillPlace.RestClient
{
    class ServiceClient
    {
        private readonly ApiClient _apiClient;

        public ServiceClient(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        public async Task<ObservableCollection<AdditionalServiceResponse>> GetAllServices()
        {
            var response = await _apiClient.HttpClient.GetAsync("/service/all");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("GET request failed");
            }
            var services = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ObservableCollection<AdditionalServiceResponse>>(services);
        }
    }
}
