using System;
using System.Collections.Generic;
using System.Text;

namespace AltayChillPlace.RestClientModel
{
    public class BookingModel
    {
        public int id_client { get; set; }
        public int id_house { get; set; }
        public int numberOfPeople { get; set; }
        public string arrivalDate { get; set; }
        public string departureDate { get; set; }
    }
}
