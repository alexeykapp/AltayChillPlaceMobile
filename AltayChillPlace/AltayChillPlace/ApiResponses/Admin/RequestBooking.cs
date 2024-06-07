using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AltayChillPlace.ApiResponses.Admin
{
    public class RequestBooking
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("dateOfApplication")]
        public string DateOfApplication { get; set; }

        [JsonProperty("arrivalDate")]
        public string ArrivalDate { get; set; }

        [JsonProperty("dateOfDeparture")]
        public string DepartureDate { get; set; }

        [JsonProperty("numberOfPersons")]
        public int NumberOfPersons { get; set; }
        [JsonProperty("totalPrice")]
        public string TotalPrice { get; set; }
    }
}
