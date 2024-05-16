using System;
using System.Collections.Generic;
using System.Text;

namespace AltayChillPlace.ApiResponses
{
    public class AuthenticationResponse
    {
        public int FkClient { get; set; }
        public String RefreshToken { get; set; }
    }
}
