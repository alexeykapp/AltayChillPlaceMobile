using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using AltayChillPlace.ApiResponses.History;
using AltayChillPlace.Interface;
using AltayChillPlace.Services;
using Prism.Mvvm;
namespace AltayChillPlace.ViewModels
{
    public class HistoryBookingVM : BindableBase
    {
        private ObservableCollection<BookingHistory> _bookingHistories;
        private readonly TokenService _tokenService;
        private readonly IHistoryService _historyService;
        private readonly MessageService _messageService;
        public HistoryBookingVM(IHistoryService historyService)
        {
            _tokenService = new TokenService();
            _historyService = historyService;
            _messageService = new MessageService();
            LoadingBookingHistory();
        }
        private async void LoadingBookingHistory()
        {
            try
            {
                string idUser = await _tokenService.GetUserID();
                var history = await _historyService.GetBookingHistoryAsync(idUser);
                if (history != null)
                {
                    BookingHistories = history;
                }
            }
            catch (Exception ex) 
            {
                _messageService.ShowPopup("Ошибка", "Повторите попытку");
                Debug.WriteLine(ex.Message);
            }

        }
        public ObservableCollection<BookingHistory> BookingHistories
        {
            get => _bookingHistories;
            set
            {
                SetProperty(ref _bookingHistories, value);
            }
        }
    }
}
