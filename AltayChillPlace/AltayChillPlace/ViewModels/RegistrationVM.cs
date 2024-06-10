using System;
using System.Collections.Generic;
using System.Text;
using AltayChillPlace.Interface;
using Prism.Commands;
using Prism.Mvvm;
using AltayChillPlace.Models;
using Xamarin.Essentials;
using AltayChillPlace.Services;
using AltayChillPlace.NavigationFile;
using AltayChillPlace.Views;

namespace AltayChillPlace.ViewModels
{
    public class RegistrationVM : BindableBase
    {
        private TokenService tokenService;
        private readonly RegistrationModel _registrationModel;
        private readonly IMessageService _messageService;
        private string _firstName;
        private string _middleName;
        private string _lastName;
        private DateTime _dateOfBirth;
        private string _phoneNumber;
        private string _email;
        private string _password;

        public RegistrationVM(RegistrationModel registrationModel, IMessageService messageService, TokenService tokenService)
        {
            _registrationModel = registrationModel;
            _messageService = messageService;
            this.tokenService = tokenService;
            RegistrationCommand = new DelegateCommand(Registration);
        }
        public DelegateCommand RegistrationCommand { get; private set; }

        private async void Registration()
        {
            if (!CheckIsEmpty())
            {
                return;
            }
            var resultReg = await _registrationModel.RegistrationAsyncTask(PhoneNumber, Email, Password, FirstName, MiddleName, LastName, DateOfBirth.ToString("dd-MM-yyyy"));
            if (resultReg == null)
            {
                _messageService.ShowPopup("Ошибка", "Повторите попытку");
            }
            else
            {
                _messageService.ShowPopup("Успешно","Спасибо, что присоединились к нам!");
                await NavigationDispatcher.Instance.PushAndRemovePreviousAsync(new Carousel());
            }
        }
        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        public string MiddleName
        {
            get => _middleName;
            set => SetProperty(ref _middleName, value);
        }

        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }

        public DateTime DateOfBirth
        {
            get => _dateOfBirth;
            set => SetProperty(ref _dateOfBirth, value);
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set => SetProperty(ref _phoneNumber, value);
        }

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private bool CheckIsEmpty()
        {
            if (string.IsNullOrEmpty(FirstName))
            {
                _messageService.ShowPopup("Ошибка", $"Пустое поле 'Имя'");
                return false;
            }
            else if (string.IsNullOrEmpty(LastName))
            {
                _messageService.ShowPopup("Ошибка", $"Пустое поле 'Фамилия'");
                return false;
            }
            else if (string.IsNullOrEmpty(Email))
            {
                _messageService.ShowPopup("Ошибка", $"Пустое поле 'Email'");
                return false;
            }
            else if (string.IsNullOrEmpty(Password))
            {
                _messageService.ShowPopup("Ошибка", $"Пустое поле 'Пароль'");
                return false;
            }
            else if (string.IsNullOrEmpty(PhoneNumber))
            {
                _messageService.ShowPopup("Ошибка", $"Пустое поле 'Телефон'");
                return false;
            }
            else if (DateOfBirth == null)
            {
                _messageService.ShowPopup("Ошибка", $"Пустое поле 'Дата рождения'");
                return false;
            }
            return true;
        }

        private void ClearField()
        {
            FirstName = "";
            MiddleName = "";
            LastName = "";
            Password = "";
            Email = "";
            PhoneNumber = "";
            DateOfBirth = DateTime.UtcNow;
        }
    }
}
