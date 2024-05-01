using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AltayChillPlace.Interface;
using Xamarin.Forms;

namespace AltayChillPlace.Services
{
    public class MessageService : IMessageService
    {
        public async void ShowPopup(string message)
        {
            Page currentPage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault() ?? Application.Current.MainPage;
            await currentPage.DisplayAlert("Notification", message, "OK");
        }
    }
}
