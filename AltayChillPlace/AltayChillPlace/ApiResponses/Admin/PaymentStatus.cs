using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AltayChillPlace.ApiResponses.Admin
{
    public class PaymentStatus
    {
        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("dateOfPayment")]
        public DateTime? DateOfPayment { get; set; }

        [JsonProperty("paymentTime")]
        public TimeSpan? PaymentTime { get; set; }

        [JsonProperty("paymentState")]
        public int? PaymentState { get; set; }

        [JsonProperty("paymentStatusName")]
        public string PaymentStatusName { get; set; }
    }
}
