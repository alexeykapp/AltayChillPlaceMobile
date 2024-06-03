using AltayChillPlace.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace AltayChillPlace.RestClientModel
{
    public class RootBoking
    {
        public BookingModel booking { get; set; }
        public int[] services { get; set; }
    }
}
