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
    public class BlogClient
    {
        private ApiClient _apiClient;
        public BlogClient(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        public async Task<ObservableCollection<BlogPostsResponce>> GetBlogPostsAsync()
        {
            var response = await _apiClient.HttpClient.GetAsync("blog/post");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error receiving posts");
            }
            var history = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ObservableCollection<BlogPostsResponce>>(history);
        }
    }
}
