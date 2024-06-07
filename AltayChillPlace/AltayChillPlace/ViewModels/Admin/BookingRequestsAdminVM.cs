using AltayChillPlace.ApiResponses;
using AltayChillPlace.ApiResponses.Admin;
using AltayChillPlace.Interface;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AltayChillPlace.ViewModels.Admin
{
    public class BookingRequestsAdminVM : BindableBase
    {
        private IAdminService _adminService;
        private IMessageService _messageService;
        private bool _isPickerVisible;
        private ApplicationStatusResponse _selectedStatus;
        private ObservableCollection<ReservationResponse> _reservationResponses;
        private ObservableCollection<ApplicationStatusResponse> _applicationStatusResponses;

        public BookingRequestsAdminVM(IAdminService adminService, IMessageService messageService)
        {
            _adminService = adminService;
            _messageService = messageService;
            EditStatusCommand = new DelegateCommand(EditStatus);
            InitializeAsync();
        }
        public DelegateCommand EditStatusCommand { get; set; }
        public ObservableCollection<ReservationResponse> ReservationResponses
        {
            get => _reservationResponses;
            set => SetProperty(ref _reservationResponses, value);
        }
        public ObservableCollection<ApplicationStatusResponse> ApplicationStatuses { get; set; }

        private async void InitializeAsync()
        {
            await LoadingDataRequests();
            await LoadingStatus();
        }
        private async void EditStatus()
        {
            IsPickerVisible = true;
        }
        private async Task LoadingDataRequests()
        {
            try
            {
                var applications = await _adminService.GetAllReservationAsync();
                if (applications != null)
                {
                    ReservationResponses = applications;
                }
            }
            catch (Exception ex)
            {
                _messageService.ShowPopup("Ошибка", "Повторите позже");
                Debug.WriteLine(ex);
            }
        }
        private async Task LoadingStatus()
        {
            try
            {
                var statuses = await _adminService.GetAllApplicationSatus();
                if (statuses != null)
                {
                    ApplicationStatuses = statuses;
                }
            }
            catch
            {
                _messageService.ShowPopup("Ошибка", "");
            }

        }
        public ApplicationStatusResponse SelectedStatus
        {
            get => _selectedStatus;
            set
            {
                if (SetProperty(ref _selectedStatus, value))
                {
                    Debug.WriteLine($"Selected status: {_selectedStatus}");
                }
            }
        }
        public bool IsPickerVisible
        {
            get => _isPickerVisible;
            set => SetProperty(ref _isPickerVisible, value);
        }
    }
}
