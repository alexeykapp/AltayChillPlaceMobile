using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AltayChillPlace.ApiResponses
{
    public class PhotoResponse
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("data")]
        public byte[] Data { get; set; }
    }
}
