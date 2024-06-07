using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AltayChillPlace.ApiResponses.Admin
{
    public class House
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("number")]
        public int Number { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("pricePerDay")]
        public int PricePerDay { get; set; }

        [JsonProperty("maxNumberOfPeople")]
        public int MaxNumberOfPeople { get; set; }

        [JsonProperty("roomSize")]
        public int RoomSize { get; set; }

        [JsonProperty("roomDescription")]
        public string RoomDescription { get; set; }

        [JsonProperty("typeOfNumber")]
        public int TypeOfNumber { get; set; }

        [JsonProperty("additionalCharacteristics")]
        public List<string> AdditionalCharacteristics { get; set; }
    }
}
