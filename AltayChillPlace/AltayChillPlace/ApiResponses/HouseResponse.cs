using Newtonsoft.Json;

namespace AltayChillPlace.ApiResponses
{
    public class HouseResponse
    {
        [JsonProperty("id_house")]
        public int IdHouse { get; set; }
        [JsonProperty("house_number")]
        public int HouseNumber { get; set; }
        [JsonProperty("house_name")]
        public string HouseName { get; set; }
        [JsonProperty("price_per_day")]
        public int PricePerDay { get; set; }
        [JsonProperty("max_number_of_people")]
        public int MaxNumberOfPeople { get; set; }
        [JsonProperty("room_size")]
        public int RoomSize { get; set; }
        [JsonProperty("room_description")]
        public string RoomDescription { get; set; }
        [JsonProperty("photo_of_the_room")]
        public PhotoResponse? PhotoOfTheRoom { get; set; }
        [JsonProperty("fk_type_of_number")]
        public int? FkTypeOfNumber { get; set; }
    }
}
