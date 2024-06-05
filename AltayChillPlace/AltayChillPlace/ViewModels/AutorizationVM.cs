using AltayChillPlace.Interface;
using AltayChillPlace.Views;
using Prism.Commands;
using Prism.Mvvm;
using System.Threading.Tasks;
using Xamarin.Forms;
using System;
using AltayChillPlace.NavigationFile;

namespace AltayChillPlace.ViewModels
{
    public class AutorizationVM : BindableBase
    {
        private readonly IAuthService _authService;
        private readonly IMessageService _messageService;
        private string _login;
        private string _password;

        public string Login
        {
            get => _login;
            set
            {
                if (value != _login)
                {
                    SetProperty(ref _login, value);
                }
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                if (value != _password)
                {
                    SetProperty(ref _password, value);
                }
            }
        }
        public DelegateCommand SingInCommand { get; private set; }
        public DelegateCommand RegistrationCommand { get; private set; }
        public AutorizationVM(IAuthService authService, IMessageService messageService)
        {
            _authService = authService;
            _messageService = messageService;
            InitializingCommands();
        }

        private async void SingInAsync()
        {
            await LoginAsync(Login, Password);
        }
        private async Task LoginAsync(string username, string password)
        {
            var success = await _authService.LoginAsync(username, password);
            if (success)
            {
                //_messageService.ShowPopup("Успешно");
                await NavigationDispatcher.Instance.PushAndRemovePreviousAsync(new Lenta());
            }
            else
            {
                _messageService.ShowPopup("Ошибка", "Повторите попытку");
            }
        }

        private async void OpenRegistrationPage()
        {
            await NavigationDispatcher.Instance.Navigation.PushAsync(new Registration());
        }

        private void InitializingCommands()
        {
            SingInCommand = new DelegateCommand(SingInAsync);
            RegistrationCommand = new DelegateCommand(OpenRegistrationPage);
        }
    }
}
