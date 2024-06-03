using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AltayChillPlace.ApiResponses.Booking
{
    public class Booking
    {
        [JsonProperty("id_reservation_request")]
        public int IdReservationRequest { get; set; }

        [JsonProperty("arrival_date")]
        public string ArrivalDate { get; set; }

        [JsonProperty("date_of_departure")]
        public string DepartureDate { get; set; }

        [JsonProperty("number_of_persons")]
        public int NumberOfPersons { get; set; }

        [JsonProperty("fk_client")]
        public int ClientId { get; set; }

        [JsonProperty("fk_house")]
        public int HouseId { get; set; }

        [JsonProperty("date_of_application")]
        public string ApplicationDate { get; set; }
    }
}
