using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AltayChillPlace.ApiResponses.Admin
{
    public class AdditionalService
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("serviceId")]
        public int ServiceId { get; set; }

        [JsonProperty("serviceName")]
        public string ServiceName { get; set; }
    }
}
