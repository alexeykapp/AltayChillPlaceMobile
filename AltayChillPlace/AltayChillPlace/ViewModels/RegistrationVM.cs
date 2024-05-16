using System;
using System.Collections.Generic;
using System.Text;
using AltayChillPlace.Interface;
using Prism.Commands;
using Prism.Mvvm;
using AltayChillPlace.Models;
using Xamarin.Essentials;
using AltayChillPlace.Services;

namespace AltayChillPlace.ViewModels
{
    public class RegistrationVM : BindableBase
    {
        private readonly RegistrationModel _registrationModel;
        private readonly IMessageService _messageService;
        private string _firstName;
        private string _middleName;
        private string _lastName;
        private DateTime _dateOfBirth;
        private string _phoneNumber;
        private string _email;
        private string _password;

        public RegistrationVM(RegistrationModel registrationModel, IMessageService messageService)
        {
            _registrationModel = registrationModel;
            _messageService = messageService;
            RegistrationCommand = new DelegateCommand(Registration);
        }
        public DelegateCommand RegistrationCommand { get; private set; }

        private async void Registration()
        {
            if (!CheckIsEmpty())
            {
                return;
            }
            var resultReg = await _registrationModel.RegistrationAsyncTask(PhoneNumber, Email, Password,
                $"{LastName} {MiddleName} {FirstName}", DateOfBirth.ToString("dd-MM-yyyy"));
            if (!resultReg)
            {
                _messageService.ShowPopup("Ошибка");
            }
            else
            {

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
                _messageService.ShowPopup($"Пустое поле 'Имя'");
                return false;
            }
            else if (string.IsNullOrEmpty(MiddleName))
            {

            }
            else if (string.IsNullOrEmpty(LastName))
            {

            }
            else if (!string.IsNullOrEmpty(Email))
            {

            }
            else if (!string.IsNullOrEmpty(Password))
            {
                
            }
            else if (DateOfBirth == null)
            {

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
