using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AltayChillPlace.ApiResponses.Booking
{
    public class BookingService
    {
        [JsonProperty("id_composition_of_application_additional_services")]
        public int IdCompositionOfApplicationAdditionalServices { get; set; }

        [JsonProperty("fk_additional_service")]
        public int AdditionalServiceId { get; set; }

        [JsonProperty("fk_reservation_request")]
        public int ReservationRequestId { get; set; }
    }
}
