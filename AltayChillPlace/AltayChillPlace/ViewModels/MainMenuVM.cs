using AltayChillPlace.NavigationFile;
using Prism.Commands;
using Prism.Mvvm;
using AltayChillPlace.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace AltayChillPlace.ViewModels
{
    public class MainMenuVM : BindableBase
    {
        public DelegateCommand OpenHistoryCommand { get; set; }
        public DelegateCommand OpenHousesCommand { get; set; }
        public DelegateCommand OpenAboutCommand { get; set; }
        public DelegateCommand OpenBlogCommand { get; set; }
        public MainMenuVM()
        {
            OpenHistoryCommand = new DelegateCommand(OpenHistory);
            OpenHousesCommand = new DelegateCommand(OpenHousesMenu);
            OpenAboutCommand = new DelegateCommand(OpenAbout);
            OpenBlogCommand = new DelegateCommand(OpenBlog);
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
    }
}
