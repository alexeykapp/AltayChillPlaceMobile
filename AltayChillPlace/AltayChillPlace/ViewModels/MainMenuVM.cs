using AltayChillPlace.NavigationFile;
using AltayChillPlace.Services;
using AltayChillPlace.Views;
using Prism.Commands;
using Prism.Mvvm;
using Xamarin.Essentials;

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
            OpenMapsCommand = new DelegateCommand(OpenMaps);
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
            var url = @"https://yandex.ru/maps/org/territoriya_otdykha_altay/146834553289/?ll=86.219533%2C51.298231&z=9";
            await Browser.OpenAsync(url, BrowserLaunchMode.SystemPreferred);
        }
    }
}
