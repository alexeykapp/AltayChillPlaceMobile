using AltayChillPlace.ApiResponses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AltayChillPlace.Static
{
    public static class UserStatic
    {
        public static int Id { get; set; }

        public static string FirstName { get; set; }

        public static string MiddleName { get; set; }

        public static string LastName { get; set; }

        public static string Email { get; set; }

        public static string Phone { get; set; }

        public static string DateOfBerth { get; set; }
        public static void SetDataUser(TokenResponse tokenResponse)
        {
            DateOfBerth = tokenResponse.User.DateOfBerth;
            Phone = tokenResponse.User.Phone;
            Id = tokenResponse.User.Id;
            MiddleName = tokenResponse.User.MiddleName;
            FirstName = tokenResponse.User.FirstName;
            LastName = tokenResponse.User.LastName;
            Email = tokenResponse.User.Email;
        }
    }

}
