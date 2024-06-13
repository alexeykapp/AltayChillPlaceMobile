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
using System.Linq;
using System.Collections.Generic;
using AltayChillPlace.NavigationFile;
using AltayChillPlace.Views;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Extensions;

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
        private bool _isErrorVisible;

        private Page currentPage;

        private Func<Task> func = () => Task.CompletedTask;

        private DateTime _arrivalDate = DateTime.Now.AddDays(1);
        private DateTime _departureDate = DateTime.Now.AddDays(2);
        private DateTime _minDepartureDate = DateTime.Now.AddDays(2);
        private DateTime _maxDepartureDate = DateTime.Now.AddMonths(5);
        private DateTime _minArrivalDate = DateTime.Now.AddDays(1);
        private DateTime _maxArrivalDate = DateTime.Now.AddMonths(5);

        private Color _currentHouseColor = Color.White;
        private Color _currentServicesColor = Color.Black;

        private ICommand _itemTappedCommand;

        private CurrentPage _currentPage = CurrentPage.House;

        public HousesVM(HouseModel houseModel, ServiceModel serviceModel)
        {
            _houseModel = houseModel ?? throw new ArgumentNullException(nameof(houseModel));
            _serviceModel = serviceModel ?? throw new ArgumentNullException(nameof(serviceModel));
            _filteringService = new FilteringService();
            currentPage = NavigationDispatcher.GetCurrentPage();

            InitializeCommands();
            LoadDataAsync();
        }

        private void InitializeCommands()
        {
            HouseClickCommand = new DelegateCommand(ExecuteHouseClick);
            ServicesClickCommand = new DelegateCommand(ExecuteServicesClick);
            SelectItemCommand = new Command<TypeHouse>(OnItemSelected);
            SelectItemServiceCommand = new Command<ServiceTypeResponce>(OnItemSelectedService);
            SearchAvailableCommand = new DelegateCommand(SearchAvailableHouse);
            SearchServicesCommand = new DelegateCommand(SearchServices);
            ShowMainMenuCommand = new DelegateCommand(ShowMainMenu);
            IsRefreshingCommand = new DelegateCommand(RefreshingAsync);
        }

        private async void LoadDataAsync()
        {
            IsVisibleActivityIndicator = true;
            IsRefreshing = true;
            IsErrorVisible = false;
            try
            {
                var housesTask = _houseModel.GetAllHouses();
                var servicesTask = _serviceModel.GetAllServices();
                var typeHouseTask = _houseModel.GetTypeHouses();
                var typeServiceTask = _serviceModel.GetServicesType();

                await Task.WhenAll(housesTask, typeHouseTask);
                Houses = await housesTask;
                TypeHouses = await typeHouseTask;
                await Task.WhenAll(servicesTask, typeServiceTask);
                Services = await servicesTask;
                TypeServices = await typeServiceTask;

                SetupCurrentItem();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading data: {ex.Message}");
                IsErrorVisible = true;
                TextLabel = $"An error occurred: {ex.Message}";
            }
            finally
            {
                IsVisibleActivityIndicator = false;
                IsRefreshing = false;
            }
        }

        private void SetupCurrentItem()
        {
            CurrentItems = _currentPage == CurrentPage.House ? Houses : Services;
        }

        private void ExecuteHouseClick()
        {
            _currentPage = CurrentPage.House;
            UpdateVisibilityForCurrentPage();
        }

        private void ExecuteServicesClick()
        {
            _currentPage = CurrentPage.Service;
            UpdateVisibilityForCurrentPage();
        }

        private void UpdateVisibilityForCurrentPage()
        {
            IsVisibleHeaderHouse = _currentPage == CurrentPage.House;
            IsVisibleHeaderService = _currentPage == CurrentPage.Service;
            HouseColor = _currentPage == CurrentPage.House ? Color.White : Color.Black;
            ServicesColor = _currentPage == CurrentPage.Service ? Color.White : Color.Black;
            CurrentItems = _currentPage == CurrentPage.House ? Houses : Services;
        }

        private void OnItemSelected(TypeHouse item)
        {
            if (TypeHouseSelected == item)
            {
                TypeHouseSelected = null;
                item.IsSelected = false;
                CurrentItems = _availableHouses ?? Houses;
            }
            else
            {
                if (TypeHouseSelected != null)
                {
                    TypeHouseSelected.IsSelected = false;
                }
                TypeHouseSelected = item;
                item.IsSelected = true;
                UpdateCurrentItemsForSelectedType(item);
            }
        }

        private void UpdateCurrentItemsForSelectedType(TypeHouse item)
        {
            var houseResponses = CurrentItems as IEnumerable<HouseResponse>;
            var housesFiltering = _filteringService.FilteringHousesByCategory(houseResponses, item.IdTypeHouse);
            UpdateVisibilityForNoHouses(housesFiltering);
            CurrentItems = housesFiltering;
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
                UpdateCurrentItemsForSelectedService(item);
            }
        }

        private void UpdateCurrentItemsForSelectedService(ServiceTypeResponce item)
        {
            if (string.IsNullOrEmpty(SearchTextService))
            {
                CurrentItems = Services;
            }
            else
            {
                SearchServices();
            }
            var additionalServices = CurrentItems as IEnumerable<AdditionalServiceResponse>;
            var serviceFiltering = _filteringService.FilteringServicesByCategory(additionalServices, item.IdTypeService);
            IsVisibleLabel = serviceFiltering.Count == 0;
            TextLabel = serviceFiltering.Count == 0 ? "Нет доступных домов" : string.Empty;
            CurrentItems = serviceFiltering;
        }

        private async void ShowHouseInfo(HouseResponse house)
        {
            await NavigationDispatcher.Instance.Navigation.PushAsync(new HouseInfoPage(house));
        }

        private void RefreshingAsync()
        {
            IsRefreshing = true;
            LoadDataAsync();
        }

        private async void ShowMainMenu()
        {
            await NavigationDispatcher.Instance.Navigation.PushAsync(new MainMenu());
        }

        private void SearchServices()
        {
            if (!string.IsNullOrEmpty(SearchTextService))
            {
                CurrentItems = Services.Where(item => item.NameOfAdditionalService.ToLower().Contains(SearchTextService.ToLower())).ToList();
            }
        }

        private async void SearchAvailableHouse()
        {
            IsRefreshing = true;
            currentPage = NavigationDispatcher.GetCurrentPage();
            try
            {
                // Загружаем доступные домики
                var availableHouses = await _houseModel.GetAvailableHouse(ArrivalDate, DepartureDate);

                // Обновляем коллекцию
                _availableHouses = new ObservableCollection<HouseResponse>(availableHouses);

                // Обновляем видимость и метки
                UpdateVisibilityForNoHouses(_availableHouses);

                // Снимаем выделение с выбранного типа домика, если есть
                if (TypeHouseSelected != null)
                {
                    TypeHouseSelected.IsSelected = false;
                }

                // Обновляем текущие элементы
                Device.BeginInvokeOnMainThread(() =>
                {
                    CurrentItems = _availableHouses;
                });

                IsErrorVisible = false;

                await currentPage.DisplaySnackBarAsync($"Найдено домиков: {_availableHouses.Count}", "", func, duration: TimeSpan.FromSeconds(3));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error searching available houses: {ex.Message}");
                IsErrorVisible = true;
                TextLabel = $"An error occurred: {ex.Message}";
            }
            finally
            {
                IsRefreshing = false;
            }
        }
        private void UpdateVisibilityForNoHouses(IEnumerable<HouseResponse> houses)
        {
            bool noHouses = houses == null || !houses.Any();
            IsVisibleLabel = noHouses;
            //TextLabel = noHouses ? "Нет доступных домов" : string.Empty;
        }
        public DelegateCommand HouseClickCommand { get; private set; }
        public DelegateCommand ServicesClickCommand { get; private set; }
        public DelegateCommand SearchAvailableCommand { get; private set; }
        public DelegateCommand SearchServicesCommand { get; private set; }
        public DelegateCommand ShowMainMenuCommand { get; private set; }
        public DelegateCommand IsRefreshingCommand { get; set; }
        public ICommand SelectItemCommand { get; private set; }
        public ICommand SelectItemServiceCommand { get; private set; }
        public ICommand ItemTappedCommand => _itemTappedCommand ??= new Command<HouseResponse>(ShowHouseInfo);

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
            get => _maxArrivalDate;
            set => SetProperty(ref _maxArrivalDate, value);
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
            set {
                SetProperty(ref _currentItems, value);
                if (value == null) {
                    IsVisibleLabel = true;
                }
                else
                {
                    IsVisibleLabel = false;
                }
            }
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
                if (string.IsNullOrEmpty(value))
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

        public bool IsErrorVisible
        {
            get => _isErrorVisible;
            set => SetProperty(ref _isErrorVisible, value);
        }
    }
}
