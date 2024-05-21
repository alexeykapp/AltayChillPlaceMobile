using AltayChillPlace.Interface;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Mime;
using AltayChillPlace.ApiResponses;
using AltayChillPlace.Helpers;
using AltayChillPlace.Models;
using Xamarin.Forms;

namespace AltayChillPlace.ViewModels
{
    public class HousesVM : BindableBase
    {
        private readonly HouseModel _houseModel;
        private object _currentItems;
        public ObservableCollection<HouseResponse> Houses { get; private set; }
        public ObservableCollection<AdditionalServiceResponse> Service { get; private set; }
        private Color _currentServicesColor = Color.Black;
        private Color _currentHouseColor = Color.White;
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public HousesVM(HouseModel houseModel)
        {
            CommandSetting();
            _houseModel = houseModel;
            GetData();
        }
        public DelegateCommand HouseClickCommand { get; private set; }
        public DelegateCommand ServicesClickCommand { get; private set; }

        private async void GetAllHouse()
        {
            Houses = await _houseModel.GetAllHouses();
        }

        public object CurrentItems
        {
            get => _currentItems;
            set => SetProperty(ref _currentItems, value);
        }
        private void ExecuteHouseClick()
        {
            ServicesColor = Color.Black;
            HouseColor = Color.White;

            CurrentItems = Houses;
        }
        private void ExecuteServicesClick()
        {
            HouseColor = Color.Black;
            ServicesColor = Color.White;

            CurrentItems = Service;
        }
        private void CommandSetting()
        {
            HouseClickCommand = new DelegateCommand(ExecuteHouseClick);
            ServicesClickCommand = new DelegateCommand(ExecuteServicesClick);
        }
        public Color HouseColor
        {
            get => _currentHouseColor;
            set => SetProperty(ref _currentHouseColor, value);
        }

        public Color ServicesColor
        {
            get => _currentServicesColor;
            set => SetProperty(ref _currentServicesColor, value);
        }

        private async void GetData()
        {
            Houses = await _houseModel.GetAllHouses();
        }
    }
}
