using AltayChillPlace.NavigationFile;
using Prism.Commands;
using Prism.Mvvm;
using AltayChillPlace.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using AltayChillPlace.Services;

namespace AltayChillPlace.ViewModels
{
    public class MainMenuVM : BindableBase
    {
        private MessageService _messageService;
        public DelegateCommand OpenHistoryCommand { get; set; }
        public DelegateCommand OpenHousesCommand { get; set; }
        public DelegateCommand OpenAboutCommand { get; set; }
        public DelegateCommand OpenBlogCommand { get; set; }
        public DelegateCommand OpenProfileCommand { get; set; }
        public DelegateCommand OpenMapsCommand { get; set; }
        public MainMenuVM()
        {
            _messageService = new MessageService();
            OpenHistoryCommand = new DelegateCommand(OpenHistory);
            OpenHousesCommand = new DelegateCommand(OpenHousesMenu);
            OpenAboutCommand = new DelegateCommand(OpenAbout);
            OpenBlogCommand = new DelegateCommand(OpenBlog);
            OpenProfileCommand = new DelegateCommand(OpenProfile);
        }
        private async void OpenAbout()
        {
            await NavigationDispatcher.Instance.Navigation.PushAsync(new About());
        }
        private async void OpenHistory()
        {
            await NavigationDispatcher.Instance.Navigation.PushAsync(new HistoryBooking());
        }
        private async void OpenHousesMenu()
        {
            await NavigationDispatcher.Instance.PushAndRemovePreviousAsync(new Houses());
        }
        private async void OpenBlog()
        {
            await NavigationDispatcher.Instance.Navigation.PushAsync(new Blog());
        }
        private async void OpenProfile()
        {
            await NavigationDispatcher.Instance.Navigation.PushAsync(new Profile());

        }
        private async void OpenMaps()
        {
            _messageService.ShowPopup("Предупреждение", "Данная часть приложения находится в разработке");
        }
    }
}
