using AltayChillPlace.RestClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using AltayChillPlace.Interface;

namespace AltayChillPlace.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly RegistrationClient _registrationClient;
        private readonly TokenService _tokenService;
        public RegistrationService(RegistrationClient registrationClient, TokenService tokenService)
        {
            _registrationClient = registrationClient;
            _tokenService = tokenService;
        }

        public async Task<bool> RegistrationAsync(string phone, string email, string password, string firstName, string middleName, string lastName, string dateOfBirth)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("phone", phone),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("email", email),
                new KeyValuePair<string, string>("dateOfBirth", dateOfBirth),
                new KeyValuePair<string, string>("last_name", lastName ),
                new KeyValuePair<string, string>("middle_name", middleName),
                new KeyValuePair<string, string>("first_name", firstName),
                new KeyValuePair<string, string>("device", "1")
            });
            try
            {
               var tokenResponse = await _registrationClient.RegistrationAsync(content);
               if (tokenResponse != null || !string.IsNullOrEmpty(tokenResponse.AccessToken))
               {
                   await _tokenService.SaveTokensAsync(tokenResponse);
                   return true;
               }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Registration failed: {ex.Message}");
            }
            return false;
        }
    }
}
