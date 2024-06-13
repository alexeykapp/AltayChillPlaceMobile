using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Input;
using AltayChillPlace.Interface;
using AltayChillPlace.NavigationFile;
using AltayChillPlace.Services;
using AltayChillPlace.Views;
using Prism.Commands;
using Prism.Mvvm;
using Xamarin.Forms;

namespace AltayChillPlace.ViewModels
{
    public class LentaVM : BindableBase
    {
        private IDataTransferService _dataTransferService;
        private DateTime _arrivalDate;
        private DateTime _departureDate;
        private DateTime _minDepartureDate;
        private DateTime _maxDepartureDate;
        private DateTime _minArrivalDate;
        private DateTime _maxArrivaDate;

        public LentaVM(IDataTransferService dataTransferService)
        {
            InitDate();
            _dataTransferService = dataTransferService;
            OpenHousesMenuCommand = new DelegateCommand(OpenHousesMenu);
            OpenBlogCommand = new DelegateCommand(OpenBlog);
            DateChangedEventArgs = new Command<DateChangedEventArgs>(ExecuteMethod);
        }
        public void ExecuteMethod(DateChangedEventArgs obj)
        {
            MinDepartureDate = obj.NewDate.AddDays(2);
        }
        public DelegateCommand OpenHousesMenuCommand { get; set; }
        public DelegateCommand OpenBlogCommand{ get; set; }
        public DelegateCommand OpenInfoRoom{ get; set; }

        public ICommand DateChangedEventArgs { get; private set; }
        private async void OpenHousesMenu()
        {
            _dataTransferService.SetData("arrival", ArrivalDate);
            _dataTransferService.SetData("departure", DepartureDate);
            await NavigationDispatcher.Instance.PushAndRemovePreviousAsync(new Houses());
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

        private void InitDate()
        {
            MinArrivalDate = DateTime.Now.AddDays(1);
            MinDepartureDate = MinArrivalDate.AddDays(1);
            MaxArrivalDate = DateTime.Now.AddMonths(6);
            MaxDepartureDate = DateTime.Now.AddMonths(6);
        }
        private async void OpenBlog()
        {
            await NavigationDispatcher.Instance.PushAndRemovePreviousAsync(new MainMenu());
        } 

    }
}
