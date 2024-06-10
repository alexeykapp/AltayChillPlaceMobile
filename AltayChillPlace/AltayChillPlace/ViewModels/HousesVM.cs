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
using System.Net.Http.Headers;
using System.Collections.Generic;
using AltayChillPlace.NavigationFile;
using AltayChillPlace.Views;
using System.Runtime.CompilerServices;

namespace AltayChillPlace.ViewModels
{
    public class HousesVM : BindableBase
    {
        private readonly HouseModel _houseModel;
        private readonly ServiceModel _serviceModel;
        private readonly FilteringService _filteringService;
        private ObservableCollection<HouseResponse> _houses;
        private ObservableCollection<HouseResponse> _availableHouses;
        private ObservableCollection<AdditionalServiceResponse> _services;
        private ObservableCollection<TypeHouse> _typeHouses;
        private ObservableCollection<ServiceTypeResponce> _typeServices;
        private object _currentItems;
        private string _searchTextService;
        private bool _isSelectedType;
        private TypeHouse _typeHouseSelected;
        private ServiceTypeResponce _typeServiceSelected;
        private string _textLabel;
 
        private bool _isVisibleHeader;
        private bool _isVisibleHouseList;
        private bool _isVisibleActivityIndicator;
        private bool _isVisibleLabel;
        private bool _isVisibleButtonUpdate;
        private bool _isVisibleHeaderHouse = true;
        private bool _isVisibleHeaderService = false;
        private bool _isRefreshing = false;
 
        private DateTime _arrivalDate = DateTime.Now.AddDays(1);
        private DateTime _departureDate = DateTime.Now.AddDays(2);
        private DateTime _minDepartureDate = DateTime.Now.AddDays(2);
        private DateTime _maxDepartureDate = DateTime.Now.AddMonths(5);
        private DateTime _minArrivalDate = DateTime.Now.AddDays(1);
        private DateTime _maxArrivaDate = DateTime.Now.AddMonths(5);

        private Color _currentHouseColor = Color.White;
        private Color _currentServicesColor = Color.Black;

        private ICommand _itemTappedCommand;

        private CurrentPage _currentPage = CurrentPage.House;
        public HousesVM(HouseModel houseModel, ServiceModel serviceModel)
        {
            _houseModel = houseModel ?? throw new ArgumentNullException(nameof(houseModel));
            _serviceModel = serviceModel ?? throw new ArgumentNullException(nameof(serviceModel));
            _filteringService = new FilteringService();

            InitializationProperties();
            LoadDataAsync();
        }
        private void OnItemSelected(TypeHouse item)
        {
            if (_availableHouses != null)
            {
                CurrentItems = _availableHouses;
            }
            else
            {
                CurrentItems = Houses;
            }
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
                IEnumerable<HouseResponse> houseResponses = CurrentItems as IEnumerable<HouseResponse>;
                var housesFiltering = _filteringService.FilteringHousesByCategory(houseResponses, item.IdTypeHouse);
                if (housesFiltering.Count == 0)
                {
                    IsVisibleLabel = true;
                    TextLabel = "Нет доступных домов";
                }
                else
                {
                    IsVisibleLabel = false;
                }
                CurrentItems = housesFiltering;
            }
        }
        private void OnItemSelectedService(ServiceTypeResponce item)
        {
            if (TypeServiceSelected == item)
            {
                TypeServiceSelected = null;
                item.IsSelected = false;
                CurrentItems = Services;
            }
            else
            {
                if (TypeServiceSelected != null)
                {
                    TypeServiceSelected.IsSelected = false;
                }
                TypeServiceSelected = item;
                item.IsSelected = true;
                if (String.IsNullOrEmpty(SearchTextService))
                {
                    CurrentItems = Services;
                }
                else
                {
                    SearchServives();
                }
                IEnumerable<AdditionalServiceResponse> additionalServices = CurrentItems as IEnumerable<AdditionalServiceResponse>;
                var serviceFiltering = _filteringService.FilteringServicesByCategory(additionalServices, item.IdTypeService);
                if (serviceFiltering.Count == 0)
                {
                    IsVisibleLabel = true;
                    TextLabel = "Нет доступных домов";
                }
                else
                {
                    IsVisibleLabel = false;
                }
                CurrentItems = serviceFiltering;
            }
        }
        private async void ShowHouseInfo(HouseResponse house)
        {
            await NavigationDispatcher.Instance.Navigation.PushAsync(new HouseInfoPage(house));
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
            await Task.Delay(1);
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
            IsRefreshing = true;
            try
            {
                var housesTask = _houseModel.GetAllHouses();
                var servicesTask = _serviceModel.GetAllServices();
                var typeHouseTask = _houseModel.GetTypeHouses();
                var typeServiceTask = _serviceModel.GetServicesType();

                await Task.WhenAll(housesTask, servicesTask, typeHouseTask, typeServiceTask);

                Houses = await housesTask;
                Services = await servicesTask;
                TypeHouses = await typeHouseTask;
                TypeServices = await typeServiceTask;

                SetupCurrentItem();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading data: {ex.Message}");
                IsVisibleLabel = true;
                TextLabel = $"An error occurred: {ex.Message}";
            }
            finally
            {
                IsVisibleActivityIndicator = false;
                IsRefreshing = false;
            }
        }
        private void RefreshingAsync()
        {
            IsRefreshing = true;
            LoadDataAsync();
        }

        private void InitializationProperties()
        {
            HouseClickCommand = new DelegateCommand(ExecuteHouseClick);
            ServicesClickCommand = new DelegateCommand(ExecuteServicesClick);
            SelectItemCommand = new Command<TypeHouse>(OnItemSelected);
            SelectItemServiceCommand = new Command<ServiceTypeResponce>(OnItemSelectedService);
            SearchAvailableCommand = new DelegateCommand(SearchAvailableHouse);
            SearchSevicesCommand = new DelegateCommand(SearchServives);
            ShowMainMenuCommand = new DelegateCommand(ShowMainMenu);
            IsRefreshingCommand = new DelegateCommand(RefreshingAsync);
        }
        public DelegateCommand HouseClickCommand { get; private set; }
        public DelegateCommand ServicesClickCommand { get; private set; }
        public DelegateCommand SortingByDataCommand { get; private set; }
        public DelegateCommand SearchAvailableCommand { get; private set; }
        public DelegateCommand SearchSevicesCommand { get; private set; }
        public DelegateCommand ShowMainMenuCommand { get; private set; }
        public DelegateCommand IsRefreshingCommand { get; set; }
        public ICommand ItemTappedHouseCommand { get; private set; }
        public ICommand SelectItemCommand { get; private set; }
        public ICommand SelectItemServiceCommand { get; private set; }
        public ICommand ItemTappedCommand
        {
            get
            {
                _itemTappedCommand = _itemTappedCommand ?? new Command((item) =>
                {
                    var selectedItem = item as HouseResponse;
                    ShowHouseInfo(selectedItem);
                });

                return _itemTappedCommand;
            }
        }
        private void SearchServives()
        {
            if (_searchTextService != null)
            {
                CurrentItems = Services.Where(item => item.NameOfAdditionalService.ToLower().Contains(_searchTextService.ToLower())).ToList();
            }
        }
        private async void SearchAvailableHouse()
        {
            _availableHouses = await _houseModel.GetAvailableHouse(ArrivalDate, DepartureDate);
            if (_availableHouses == null || _availableHouses.Count == 0)
            {
                IsVisibleLabel = true;
                TextLabel = "Нет доступных домов";
            }
            else
            {
                IsVisibleLabel = false;
            }
            if (TypeHouseSelected != null)
            {
                TypeHouseSelected.IsSelected = false;
            }
            if (_availableHouses != null)
            {
                CurrentItems = _availableHouses;
            }

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
        public async void ShowMainMenu()
        {
            await NavigationDispatcher.Instance.Navigation.PushAsync(new MainMenu());
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
        public bool IsVisibleLabel
        {
            get => _isVisibleLabel;
            private set => SetProperty(ref _isVisibleLabel, value);
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
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
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
        public string SearchTextService
        {
            get => _searchTextService;
            set
            {
                SetProperty(ref _searchTextService, value);
                if (value == "")
                {
                    CurrentItems = Services;
                }
            }
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
        public ObservableCollection<ServiceTypeResponce> TypeServices
        {
            get => _typeServices;
            set => SetProperty(ref _typeServices, value);
        }
        public TypeHouse TypeHouseSelected
        {
            get => _typeHouseSelected;
            set => SetProperty(ref _typeHouseSelected, value);
        }
        public ServiceTypeResponce TypeServiceSelected
        {
            get => _typeServiceSelected;
            set => SetProperty(ref _typeServiceSelected, value);
        }
        public string TextLabel
        {
            get => _textLabel;
            set => SetProperty(ref _textLabel, value);
        }
    }
}