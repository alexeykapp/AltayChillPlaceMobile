using AltayChillPlace.ApiResponses;
using AltayChillPlace.Interface;
using AltayChillPlace.RestClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AltayChillPlace.Services
{
    public class ProfileService : IProfileService
    {
        private ProfileClient _profileClient;
        private TokenService _tokenService;
        public ProfileService(ProfileClient profileClient)
        {
            _profileClient = profileClient;
            _tokenService = new TokenService();
        }

        public async Task<PersonResponce> GetPersonResponce()
        {
            string id  = await _tokenService.GetUserID();
            var respone = await _profileClient.GetPersonInfoByPk(id);
            return respone;
        }
        public async Task<PersonResponce> UpdateProfileAsync(PersonResponce person)
        {
            string id = await _tokenService.GetUserID();
            var respone = await _profileClient.UpdateProfileAsync(id, person);
            return respone;
        }
    }
}
