using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AltayChillPlace.ApiResponses
{
    public class ApplicationStatusResponse
    {
        [JsonProperty("id_booking_request_status_name")]
        public int IdStatus { get; set; }
        [JsonProperty("name_booking_request_status_name")]
        public string Status { get; set; }
    }
}
