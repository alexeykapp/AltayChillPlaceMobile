using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AltayChillPlace.ApiResponses;
using AltayChillPlace.Interface;
using AltayChillPlace.Services;

namespace AltayChillPlace.Models
{
    public class RegistrationModel
    {
        private readonly IRegistrationService _registrationService;
        public RegistrationModel(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }
        public async Task<TokenResponse> RegistrationAsyncTask(string phone, string email, string password, string firstName, string middleName, string lastName, string dateOfBirth)
        {
            var resulReg = await _registrationService.RegistrationAsync(phone, email, password, firstName, middleName, lastName, dateOfBirth);
            return resulReg;
        }
    }
}
