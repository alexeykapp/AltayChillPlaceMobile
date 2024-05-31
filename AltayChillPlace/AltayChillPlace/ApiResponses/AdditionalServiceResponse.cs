using Newtonsoft.Json;

namespace AltayChillPlace.ApiResponses
{
    public class AdditionalServiceResponse
    {
        [JsonProperty("id_additional_service")]
        public int IdAdditionalService { get; set; }
        [JsonProperty("name_of_additional_service")]
        public string NameOfAdditionalService { get; set; }
        [JsonProperty("additional_service_price")]
        public int AdditionalServicePrice { get; set; }
        [JsonProperty("description_of_additional_service")]
        public string DescriptionOfAdditionalService { get; set; }
        [JsonProperty("fk_service_type")]
        public int FkServiceType { get; set; }
    }
}
