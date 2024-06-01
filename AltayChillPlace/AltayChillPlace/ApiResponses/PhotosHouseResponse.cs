using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AltayChillPlace.ApiResponses
{
    public class PhotosHouseResponse
    {
        [JsonProperty("photosRoom")]
        public List<PhotoResponse> Photos { get; set; }
    }
}
