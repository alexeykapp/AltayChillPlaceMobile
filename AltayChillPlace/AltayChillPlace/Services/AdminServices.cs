using AltayChillPlace.ApiResponses;
using AltayChillPlace.ApiResponses.Admin;
using AltayChillPlace.Interface;
using AltayChillPlace.RestClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace AltayChillPlace.Services
{
    public class AdminServices : IAdminService
    {
        private AdminClient _adminClient;
        public AdminServices(AdminClient adminClient)
        {
            _adminClient = adminClient;
        }
        public async Task<ObservableCollection<ReservationResponse>> GetAllReservationAsync()
        {
            var result = await _adminClient.GetAllReservationAsync();
            return result;
        }
        public async Task<ObservableCollection<ApplicationStatusResponse>> GetAllApplicationSatus()
        {
            var result = await _adminClient.GetAllApplicationSatus();
            return result;
        }
        public async Task CreateNewApplicationStatusAsync(int idStatus, int idRequest)
        {
            await _adminClient.CreateNewApplicationStatusAsync(Convert.ToString(idStatus), idRequest);
        }
        public async Task CreateNewPaymenеStatusAsync(int idStatus, int idRequest) 
        {
            await _adminClient.CreateNewPaymentStatusAsync(Convert.ToString(idStatus), idRequest);
        }
        public async Task<NewPostBlogResponse> CreateNewPostBlogAsync(string title, string description, string date, byte[] photo)
        {
            var newPost = await _adminClient.CreateNewPostBlogAsync(title, description, date, photo);
            return newPost;
        }
        public async Task DeletePostBlog(int idPost)
        {
            await _adminClient.DeleteBlogPost(idPost);
        }
    }
}
