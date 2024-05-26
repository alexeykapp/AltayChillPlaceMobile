using AltayChillPlace.ApiResponses;
using AltayChillPlace.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AltayChillPlace.ViewModels
{
    public class HousesVM : BindableBase
    {
        // Private fields
        private readonly HouseModel _houseModel;
        private readonly ServiceModel _serviceModel;
        private ObservableCollection<HouseResponse> _houses;
        private ObservableCollection<AdditionalServiceResponse> _services;
        private object _currentItems;
        private bool _isVisibleHeader;
        private bool _isVisibleHouseList;
        private bool _isVisibleActivityIndicator;
        private bool _isVisibleError;
        private DateTime _arrivalDate;
        private DateTime _departureDate;
        private DateTime _minDepartureDate = DateTime.Now.AddDays(2);
        private DateTime _maxDepartureDate = DateTime.Now.AddMonths(5);
        private DateTime _minArrivalDate = DateTime.Now.AddDays(1);
        private DateTime _maxArrivaDate = DateTime.Now.AddMonths(5);
        private Color _currentHouseColor = Color.White;
        private Color _currentServicesColor = Color.Black;
        private event Action ClickHouse;

        // Constructor
        public HousesVM(HouseModel houseModel, ServiceModel serviceModel)
        {
            _houseModel = houseModel ?? throw new ArgumentNullException(nameof(houseModel));
            _serviceModel = serviceModel ?? throw new ArgumentNullException(nameof(serviceModel));

            // Initialize commands
            HouseClickCommand = new DelegateCommand(ExecuteHouseClick);
            ServicesClickCommand = new DelegateCommand(ExecuteServicesClick);

            // Load data asynchronously
            LoadDataAsync();
        }

        public ObservableCollection<HouseResponse> Houses
        {
            get => _houses;
            private set => SetProperty(ref _houses, value, onChanged: HandleHousesChanged);
        }

        public ObservableCollection<AdditionalServiceResponse> Services
        {
            get => _services;
            private set => SetProperty(ref _services, value, onChanged: HandleServicesChanged);
        }

        public object CurrentItems
        {
            get => _currentItems;
            private set => SetProperty(ref _currentItems, value);
        }

        public bool IsVisibleHeader
        {
            get => _isVisibleHeader;
            private set => SetProperty(ref _isVisibleHeader, value);
        }

        public bool IsVisibleHouseList
        {
            get => _isVisibleHouseList;
            private set => SetProperty(ref _isVisibleHouseList, value);
        }

        public bool IsVisibleActivityIndicator
        {
            get => _isVisibleActivityIndicator;
            private set => SetProperty(ref _isVisibleActivityIndicator, value);
        }

        public bool IsVisibleError
        {
            get => _isVisibleError;
            private set => SetProperty(ref _isVisibleError, value);
        }

        public Color HouseColor
        {
            get => _currentHouseColor;
            private set => SetProperty(ref _currentHouseColor, value);
        }

        public Color ServicesColor
        {
            get => _currentServicesColor;
            private set => SetProperty(ref _currentServicesColor, value);
        }

        public DelegateCommand HouseClickCommand { get; private set; }
        public DelegateCommand ServicesClickCommand { get; private set; }

        private void ExecuteHouseClick()
        {
            if (Houses.Count != 0 || Houses != null)
            {
                IsVisibleHeader = true;
            }
            HouseColor = Color.White;
            ServicesColor = Color.Black;
            CurrentItems = Houses;
        }

        private void ExecuteServicesClick()
        {
            IsVisibleHeader = false;
            HouseColor = Color.Black;
            ServicesColor = Color.White;
            CurrentItems = Services;
            IsVisibleHeader = false;
        }

        private async void LoadDataAsync()
        {
            IsVisibleActivityIndicator = true;
            IsVisibleError = false;

            try
            {
                Houses = await _houseModel.GetAllHouses();
                Services = await _serviceModel.GetAllServices();

                CurrentItems = Houses;
            }
            catch (Exception)
            {
                IsVisibleError = true;
            }
            finally
            {
                IsVisibleActivityIndicator = false;
            }
        }

        private void HandleHousesChanged()
        {
            IsVisibleHouseList = Houses != null && Houses.Count > 0;
            IsVisibleError = Houses == null || Houses.Count == 0;
        }

        private void HandleServicesChanged()
        {
            IsVisibleHouseList = Services != null && Services.Count > 0;
            IsVisibleError = Services == null || Services.Count == 0;
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
    }
}