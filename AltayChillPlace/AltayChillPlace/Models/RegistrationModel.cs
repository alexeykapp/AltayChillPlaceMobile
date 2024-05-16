using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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
        public async Task<bool> RegistrationAsyncTask(string phone, string email, string password, string fullName, string dateOfBirth)
        {
            var resulReg = await _registrationService.RegistrationAsync(phone, email, password, fullName, dateOfBirth);
            if (!resulReg)
            {
                return false;
            }
            return true;
        }
    }
}
