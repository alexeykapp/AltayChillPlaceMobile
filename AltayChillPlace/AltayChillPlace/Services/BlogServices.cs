using AltayChillPlace.ApiResponses;
using AltayChillPlace.Interface;
using AltayChillPlace.RestClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace AltayChillPlace.Services
{
    public class BlogServices : IBlogServices
    {
        private readonly BlogClient _blogClient;
        public BlogServices(BlogClient blogClient)
        {
            _blogClient = blogClient;
        }
        public async Task<ObservableCollection<BlogPostsResponce>> GetBlogPostsResponces()
        {
            var posts = await _blogClient.GetBlogPostsAsync();
            return posts;
        }
    }
}
