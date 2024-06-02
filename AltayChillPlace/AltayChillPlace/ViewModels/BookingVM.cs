using AltayChillPlace.ApiResponses;
using Prism.Mvvm;
using System;

namespace AltayChillPlace.ViewModels
{
    public class BookingVM : BindableBase
    {
        private string _firstName;
        private string _middleName;
        private string _lastName;
        private string _phoneNumber;
        private DateTime _arrivalDate;
        private DateTime _departureDate;
        private HouseResponse house;
        private int _finalPrice;
        public BookingVM()
        {

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
    }
}
