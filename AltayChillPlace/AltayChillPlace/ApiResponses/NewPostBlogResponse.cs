using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AltayChillPlace.ApiResponses
{
    public class NewPostBlogResponse
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; } 

        [JsonProperty("photo")]
        public byte[] Photo { get; set; }
    }
}

