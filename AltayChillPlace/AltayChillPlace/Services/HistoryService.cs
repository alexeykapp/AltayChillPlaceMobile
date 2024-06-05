using AltayChillPlace.ApiResponses.History;
using AltayChillPlace.Interface;
using AltayChillPlace.RestClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace AltayChillPlace.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly HistoryClient _historyClient;
        public HistoryService(HistoryClient historyClient)
        {
            _historyClient = historyClient;
        }
        public async Task<ObservableCollection<BookingHistory>> GetBookingHistoryAsync(string id)
        {
            try
            {
                var history = await _historyClient.GetBookingHistoryAsync(id);
                return history;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Login failed: {ex.Message}");
            }
            return null;
        }
    }
}
