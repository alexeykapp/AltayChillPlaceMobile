using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AltayChillPlace.Interface
{
    public interface IRegistrationService
    {
        Task<bool> RegistrationAsync(string phone, string email, string password, string firstName, string middleName, string lastName, string dateOfBirth);
    }
}
