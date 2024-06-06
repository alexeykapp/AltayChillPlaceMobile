using AltayChillPlace.ApiResponses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AltayChillPlace.Interface
{
    public interface IProfileService
    {
        Task<PersonResponce> GetPersonResponce();
        Task<PersonResponce> UpdateProfileAsync(PersonResponce person);
    }
}
