using AltayChillPlace.ApiResponses.Booking;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AltayChillPlace.Interface
{
    public interface IBookingService
    {
        Task<BookingResponce> CreateNewBooking(int idClient, int idHouse, int numberOfPeople, DateTime arrivalDate, DateTime departureDate);
    }
}
