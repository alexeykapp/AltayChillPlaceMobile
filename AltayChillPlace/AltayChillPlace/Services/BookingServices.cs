using AltayChillPlace.ApiResponses.Booking;
using AltayChillPlace.Interface;
using AltayChillPlace.RestClient;
using AltayChillPlace.RestClientModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AltayChillPlace.Services
{
    public class BookingServices : IBookingService
    {
        private readonly BookingClient _bookingClient;
        public BookingServices(BookingClient bookingClient)
        {
            _bookingClient = bookingClient;
        }
        public async Task<BookingResponce> CreateNewBooking(int idClient, int idHouse, int numberOfPeople, DateTime arrivalDate, DateTime departureDate)
        {
            RootBoking root = new RootBoking
            {
                booking = new RestClientModel.BookingModel
                {
                    id_client = idClient,
                    id_house = idHouse,
                    numberOfPeople = numberOfPeople,
                    arrivalDate = arrivalDate.ToString("yyyy-MM-dd"),
                    departureDate = departureDate.ToString("yyyy-MM-dd")
                },
                services = new int[1] { 111 }
            };
            try
            {
                var result = await _bookingClient.CreateNewBooking(root);
                return result;
            }
            catch (Exception)
            {
                throw new Exception("Error post create new booking");
            }
        }
    }
}
