using AltayChillPlace.ApiResponses;
using AltayChillPlace.Models;
using AltayChillPlace.Helpers;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Diagnostics;
using System.Windows.Input;
using System.Linq;

namespace AltayChillPlace.ViewModels
{
    public class HousesVM : BindableBase
    {
        // Private fields
        private readonly HouseModel _houseModel;
        private readonly ServiceModel _serviceModel;
        private ObservableCollection<HouseResponse> _houses;
        private ObservableCollection<AdditionalServiceResponse> _services;
        private ObservableCollection<TypeHouse> _typeHouses;
        private object _currentItems;
        private string _searchTextService;
        private bool _isSelectedType;
        private TypeHouse _typeHouseSelected;
        // isVisible
        private bool _isVisibleHeader;
        private bool _isVisibleHouseList;
        private bool _isVisibleActivityIndicator;
        private bool _isVisibleError;
        private bool _isVisibleButtonUpdate;
        private bool _isVisibleHeaderHouse = true;
        private bool _isVisibleHeaderService = false;
        // Date
        private DateTime _arrivalDate = DateTime.Now.AddDays(1);
        private DateTime _departureDate = DateTime.Now.AddDays(2);
        private DateTime _minDepartureDate = DateTime.Now.AddDays(2);
        private DateTime _maxDepartureDate = DateTime.Now.AddMonths(5);
        private DateTime _minArrivalDate = DateTime.Now.AddDays(1);
        private DateTime _maxArrivaDate = DateTime.Now.AddMonths(5);
        // Color
        private Color _currentHouseColor = Color.White;
        private Color _currentServicesColor = Color.Black;

        private CurrentPage _currentPage = CurrentPage.House;
        public HousesVM(HouseModel houseModel, ServiceModel serviceModel)
        {
            _houseModel = houseModel ?? throw new ArgumentNullException(nameof(houseModel));
            _serviceModel = serviceModel ?? throw new ArgumentNullException(nameof(serviceModel));

            // Initialize commands
            HouseClickCommand = new DelegateCommand(ExecuteHouseClick);
            ServicesClickCommand = new DelegateCommand(ExecuteServicesClick);
            ItemTappedHouseCommand = new Command<ItemTappedEventArgs>(OnItemTappedHouse);
            SelectItemCommand = new Command<TypeHouse>(OnItemSelected);

            // Load data asynchronously
            LoadDataAsync();
        }
        private void OnItemSelected(TypeHouse item)
        {
            if (TypeHouseSelected == item)
            {
                TypeHouseSelected = null;
                item.IsSelected = false;
            }
            else
            {
                if (TypeHouseSelected != null)
                {
                    TypeHouseSelected.IsSelected = false;
                }
                TypeHouseSelected = item;
                item.IsSelected = true;
            }
        }
        private void OnItemTappedHouse(ItemTappedEventArgs e)
        {
            var selectedItem = e.Item as HouseResponse;
        }

        private void ExecuteHouseClick()
        {
            _currentPage = CurrentPage.House;

            IsVisibleHeaderService = false;
            IsVisibleHeaderHouse = true;
            HouseColor = Color.White;
            ServicesColor = Color.Black;
            CurrentItems = Houses;
        }

        private void ExecuteServicesClick()
        {
            _currentPage = CurrentPage.Service;

            IsVisibleHeaderHouse = false;
            IsVisibleHeaderService = true;
            HouseColor = Color.Black;
            ServicesColor = Color.White;
            CurrentItems = Services;
            IsVisibleHeader = false;
        }

        public async void UpdateProperties()
        {
            await Task.Delay(100);
            MinDepartureDate = DateTime.Now.AddDays(2);
            MaxDepartureDate = DateTime.Now.AddMonths(5);
            MinArrivalDate = DateTime.Now.AddDays(1);
            MaxArrivalDate = DateTime.Now.AddMonths(5);

            ArrivalDate = MinArrivalDate;
            DepartureDate = MinDepartureDate;
        }
        private void SetupCurrentItem()
        {
            if (_currentPage == CurrentPage.House)
            {
                CurrentItems = Houses;
            }
            else
            {
                CurrentItems = Services;
            }
        }
        private async void LoadDataAsync()
        {
            IsVisibleActivityIndicator = true;
            try
            {
                var houses = _houseModel.GetAllHouses();
                var services = _serviceModel.GetAllServices();
                var typeHouse = _houseModel.GetTypeHouses();
                await Task.WhenAll(houses, services, typeHouse);
                Houses = houses.Result;
                Services = services.Result;
                TypeHouses = typeHouse.Result;
                SetupCurrentItem();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                IsVisibleError = true;
            }
            finally
            {
                IsVisibleActivityIndicator = false;
            }
        }

        public DelegateCommand HouseClickCommand { get; private set; }
        public DelegateCommand ServicesClickCommand { get; private set; }
        public DelegateCommand SortingByDataCommand { get; private set; }
        public ICommand ItemTappedHouseCommand { get; private set; }
        public ICommand SelectItemCommand { get; private set; }
        public ICommand ItemTappedCommand
        {
            get
            {
                return new Command((item) =>
                {
                    HouseResponse selectedItem = item as HouseResponse;

                });
            }
        }
        private void SearchServives()
        {
            CurrentItems = Services.Where(item => item.NameOfAdditionalService.ToLower().Contains(_searchTextService.ToLower())).ToList();
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


        public bool IsSelectedType
        {
            get => _isSelectedType;
            set => SetProperty(ref _isSelectedType, value);
        }
        public bool IsVisibleHeaderHouse
        {
            get => _isVisibleHeaderHouse;
            set => SetProperty(ref _isVisibleHeaderHouse, value);
        }
        public bool IsVisibleHeaderService
        {
            get => _isVisibleHeaderService;
            set => SetProperty(ref _isVisibleHeaderService, value);
        }
        public bool IsVisibleError
        {
            get => _isVisibleError;
            private set => SetProperty(ref _isVisibleError, value);
        }
        public bool IsVisibleButtonUpdate
        {
            get => _isVisibleButtonUpdate;
            set => SetProperty(ref _isVisibleButtonUpdate, value);
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

        public ObservableCollection<HouseResponse> Houses
        {
            get => _houses;
            private set => SetProperty(ref _houses, value);
        }

        public ObservableCollection<AdditionalServiceResponse> Services
        {
            get => _services;
            private set => SetProperty(ref _services, value);
        }
        public ObservableCollection<TypeHouse> TypeHouses
        {
            get => _typeHouses;
            set => SetProperty(ref _typeHouses, value);
        }
        public TypeHouse TypeHouseSelected
        {
            get => _typeHouseSelected;
            set => SetProperty(ref _typeHouseSelected, value);
        }
    }
}