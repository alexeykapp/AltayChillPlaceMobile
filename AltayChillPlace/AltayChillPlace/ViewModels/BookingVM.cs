﻿using AltayChillPlace.ApiResponses;
using AltayChillPlace.Interface;
using AltayChillPlace.Services;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Diagnostics.SymbolStore;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Xamarin.Essentials;
using System.Threading.Tasks;

namespace AltayChillPlace.ViewModels
{
    public class BookingVM : BindableBase
    {
        private readonly IBookingService _bookingServices;
        private readonly IProfileService _profileService;
        private int _idUser;
        private string _firstName;
        private string _middleName;
        private string _lastName;
        private string _phoneNumber;
        private int _numberOfPeople = 1;

        private DateTime _arrivalDate = DateTime.Now.AddDays(1);
        private DateTime _departureDate = DateTime.Now.AddDays(2);
        private DateTime _minDepartureDate = DateTime.Now.AddDays(2);
        private DateTime _maxDepartureDate = DateTime.Now.AddMonths(5);
        private DateTime _minArrivalDate = DateTime.Now.AddDays(1);
        private DateTime _maxArrivaDate = DateTime.Now.AddMonths(5);
        private HouseResponse house;
        private int _finalPrice;
        private MessageService _messageService;
        private bool _isRefreshing;

        public BookingVM(IBookingService bookingService, IProfileService profileService)
        {
            _profileService = profileService;
            LoadingUserInfo();
            _bookingServices = bookingService;
            _messageService = new MessageService();
            BookingCommand = new DelegateCommand(RequestBooking);
            RefreshCommand = new DelegateCommand(async () => await RefreshData());
        }

        public DelegateCommand BookingCommand { get; set; }
        public DelegateCommand RefreshCommand { get; set; }

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        private async Task RefreshData()
        {
            IsRefreshing = true;

            await LoadingUserInfo();

            IsRefreshing = false;
        }

        private async void RequestBooking()
        {
            var resultCheck = CheckIsEmpty();
            if (resultCheck)
            {
                var resultCreate = await _bookingServices.CreateNewBooking(_idUser, house.IdHouse, _numberOfPeople, _arrivalDate, _departureDate);
                if (resultCreate == null)
                {
                    _messageService.ShowPopup("Ошибка", "Повторите попытку");
                }
                else
                {
                    _messageService.ShowPopup("Успешно!", "Ожидайте звонка администратора");
                    EntryEmpty();
                }
            }
            else
            {
                _messageService.ShowPopup("Ошибка", "Проверьте все поля ввода");
            }
        }

        private async Task LoadingUserInfo()
        {
            _idUser = int.Parse(await SecureStorage.GetAsync("IdUser"));
            var profile = await _profileService.GetPersonResponce();
            FirstName = profile.FirstName;
            LastName = profile.LastName;
            MiddleName = profile.MiddleName;
            PhoneNumber = profile.Phone;

            NumberOfPeople = 2;
        }

        private void SettingValue()
        {
            FinalPrice = House.PricePerDay;
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

        public string PhoneNumber
        {
            get => _phoneNumber;
            set => SetProperty(ref _phoneNumber, value);
        }

        public DateTime ArrivalDate
        {
            get => _arrivalDate;
            set => SetProperty(ref _arrivalDate, value);
        }

        public DateTime DepartureDate
        {
            get => _departureDate;
            set => SetProperty(ref _departureDate, value);
        }

        public DateTime MinDepartureDate
        {
            get => _minDepartureDate;
            set => SetProperty(ref _minDepartureDate, value);
        }

        public DateTime MaxDepartureDate
        {
            get => _maxDepartureDate;
            set => SetProperty(ref _maxDepartureDate, value);
        }

        public DateTime MinArrivalDate
        {
            get => _minArrivalDate;
            set => SetProperty(ref _minArrivalDate, value);
        }

        public DateTime MaxArrivalDate
        {
            get => _maxArrivaDate;
            set => SetProperty(ref _maxArrivaDate, value);
        }

        public int NumberOfPeople
        {
            get => _numberOfPeople;
            set
            {
                SetProperty(ref _numberOfPeople, value);
                CheckValuePeople(value);
            }
        }

        public HouseResponse House
        {
            get => house;
            set
            {
                SetProperty(ref house, value);
                SettingValue();
            }
        }

        public int FinalPrice
        {
            get => _finalPrice;
            set => SetProperty(ref _finalPrice, value);
        }

        private bool CheckIsEmpty()
        {
            if (string.IsNullOrEmpty(FirstName))
            {
                _messageService.ShowPopup("Ошибка", $"Пустое поле 'Имя'");
                return false;
            }
            else if (string.IsNullOrEmpty(MiddleName))
            {
                _messageService.ShowPopup("Ошибка", $"Пустое поле 'Отчество'");
                return false;
            }
            else if (string.IsNullOrEmpty(LastName))
            {
                _messageService.ShowPopup("Ошибка", $"Пустое поле 'Фамилия'");
                return false;
            }
            else if (string.IsNullOrEmpty(PhoneNumber))
            {
                _messageService.ShowPopup("Ошибка", $"Пустое поле 'Телефон'");
                return false;
            }
            return true;
        }

        private void EntryEmpty()
        {
            FirstName = string.Empty;
            MiddleName = string.Empty;
            LastName = string.Empty;
            PhoneNumber = string.Empty;
            NumberOfPeople = 2;
        }

        private void CheckValuePeople(int value)
        {
            if (value < 1)
            {
                NumberOfPeople = 1;
                _messageService.ShowPopup("Ошибка", "Введите другое число человек");
            }
        }
        private bool isCheckAddPlace;
        public bool IsCheckAddPlace
        {
            get => isCheckAddPlace;
            set
            {
                if (value == true)
                {
                    FinalPrice += 1000;
                }
                else if (isCheckAddPlace == true && value == false)
                {
                    FinalPrice -= 1000;
                }
                SetProperty(ref isCheckAddPlace, value);
            }
        }
        private bool isCheckDinner;
        public bool IsCheckDinner
        {
            get => isCheckDinner;
            set
            {
                if(value == true)
                {
                    FinalPrice += 600;
                }
                else if (isCheckDinner == true && value == false)
                {
                    FinalPrice -= 600;
                }
                SetProperty(ref isCheckDinner, value);
            }
        }
    }
}