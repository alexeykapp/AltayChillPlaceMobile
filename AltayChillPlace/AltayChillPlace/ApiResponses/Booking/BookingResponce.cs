using Newtonsoft.Json;
using System.Collections.Generic;

namespace AltayChillPlace.ApiResponses.Booking
{
    public class BookingResponce
    {
        [JsonProperty("booking")]
        public Booking Booking { get; set; }

        [JsonProperty("bookingServices")]
        public List<BookingService> BookingServices { get; set; }
    }
}
