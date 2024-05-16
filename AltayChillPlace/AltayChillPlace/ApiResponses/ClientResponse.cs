using System;
using System.Collections.Generic;
using System.Text;

namespace AltayChillPlace.ApiResponses
{
    public class Client
    {
        public int IdClient { get; set; }
        public string FullNameClient { get; set; }
        public DateTime DateOfBirthClient { get; set; }
        public string PhoneNumberClient { get; set; }
        public string MailClient { get; set; }
        public string PasswordClient { get; set; }
        public byte[] PhotoAvatar { get; set; }
    }
}
