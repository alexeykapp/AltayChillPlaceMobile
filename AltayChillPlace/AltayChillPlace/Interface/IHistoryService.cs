using AltayChillPlace.ApiResponses.History;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace AltayChillPlace.Interface
{
    public interface IHistoryService
    {
        Task<ObservableCollection<BookingHistory>> GetBookingHistoryAsync(string id);
    }
}
