using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AltayChillPlace.ApiResponses.History
{
    public class BookingHistory
    {
        [JsonProperty("houseName")]
        public string HouseName { get; set; }

        [JsonProperty("arrivalDate")]
        public string ArrivalDate { get; set; }

        [JsonProperty("departureDate")]
        public string DepartureDate { get; set; }

        [JsonProperty("numberOfPersons")]
        public int NumberOfPersons { get; set; }

        [JsonProperty("totalSum")]
        public int TotalSum { get; set; }

        [JsonProperty("bookingStatus")]
        public string BookingStatus { get; set; }

        [JsonProperty("additionalServices")]
        public List<AdditionalService> AdditionalServices { get; set; }
    }
}
