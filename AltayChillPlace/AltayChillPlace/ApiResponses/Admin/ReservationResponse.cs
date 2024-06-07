using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AltayChillPlace.ApiResponses.Admin
{
    public class ReservationResponse
    {
        [JsonProperty("requestBooking")]
        public RequestBooking RequestBooking { get; set; }

        [JsonProperty("client")]
        public Client Client { get; set; }

        [JsonProperty("house")]
        public House House { get; set; }

        [JsonProperty("paymentStatus")]
        public PaymentStatus PaymentStatus { get; set; }

        [JsonProperty("applicationStatus")]
        public ApplicationStatus ApplicationStatus { get; set; }

        [JsonProperty("additionalServices")]
        public List<AdditionalService> AdditionalServices { get; set; }
    }
}
