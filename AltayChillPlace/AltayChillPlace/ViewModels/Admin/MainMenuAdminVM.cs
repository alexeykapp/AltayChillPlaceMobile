using AltayChillPlace.NavigationFile;
using Prism.Commands;
using Prism.Mvvm;
using AltayChillPlace.Views.Admin;
using System;
using System.Collections.Generic;
using System.Text;

namespace AltayChillPlace.ViewModels.Admin
{
    public class MainMenuAdminVM : BindableBase
    {
        public MainMenuAdminVM()
        {
            OpenReservationPageCommand = new DelegateCommand(OpenReservationPage);
        }
        public DelegateCommand OpenReservationPageCommand {  get; set; }
        private void OpenReservationPage()
        {
            NavigationDispatcher.Instance.Navigation.PushAsync(new BookingRequestsAdmin());
        }

    }
}
