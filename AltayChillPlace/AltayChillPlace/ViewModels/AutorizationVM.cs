using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Prism.Mvvm;
using System.Threading.Tasks;
using AltayChillPlace.Interface;
using Prism.Commands;

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
                _messageService.ShowPopup("Успешно");
            }
            else
            {
                _messageService.ShowPopup("Ошибка");
            }
        }

        private void InitializingCommands()
        {
            SingInCommand = new DelegateCommand(SingInAsync);
        }
    }
}
