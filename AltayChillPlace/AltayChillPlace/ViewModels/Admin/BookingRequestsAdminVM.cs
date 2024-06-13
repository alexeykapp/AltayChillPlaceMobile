using AltayChillPlace.ApiResponses;
using AltayChillPlace.ApiResponses.Admin;
using AltayChillPlace.Interface;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace AltayChillPlace.ViewModels.Admin
{
    public class BookingRequestsAdminVM : BindableBase
    {
        private ApplicationStatus _selectedStatus2;
        private IAdminService _adminService;
        private IMessageService _messageService;
        private bool _popupAppStatusIsVisible;
        private bool _popupPayStatusIsVisible;
        private ApplicationStatusResponse _selectedStatus;
        private ObservableCollection<ReservationResponse> _reservationResponses;
        private ObservableCollection<ApplicationStatusResponse> _applicationStatusResponses;
        private ObservableCollection<ReservationResponse> _filteredReservationResponses;

        private bool isConfirmed;
        private bool isCancelled;
        private bool isUnderReview;
        //Payment
        private bool _isAwaitingPrepayment;
        private bool _isPrepaid;
        private bool _isPaid;
        private bool _isRefunded;

        public BookingRequestsAdminVM(IAdminService adminService, IMessageService messageService)
        {
            _adminService = adminService;
            _messageService = messageService;
            CancelPopupAppCommand = new DelegateCommand(CancelPopupApp);
            CancelPopupPayCommand = new DelegateCommand(CancelPopupPay);
            EditStatusCommand = new DelegateCommand(EditAppStatus);
            EditPayStatusCommand = new DelegateCommand(EditPayStatus);
            OpenPopupAppStatusCommand = new DelegateCommand<ReservationResponse>(OpenPopupAppStatus);
            OpenPopupPayStatusCommand = new DelegateCommand<ReservationResponse>(OpenPopupPayStatus);
            CallClientCommand = new DelegateCommand<ReservationResponse>(CallClient);
            InitializeAsync();
        }

        public DelegateCommand EditStatusCommand { get; set; }
        public DelegateCommand<ReservationResponse> OpenPopupAppStatusCommand { get; set; }
        public DelegateCommand<ReservationResponse> OpenPopupPayStatusCommand { get; set; }
        public DelegateCommand EditPayStatusCommand { get; set; }
        public DelegateCommand<ReservationResponse> CallClientCommand { get; set; }
        public DelegateCommand CancelPopupAppCommand { get; set; }
        public DelegateCommand CancelPopupPayCommand { get; set; }

        public ObservableCollection<ReservationResponse> ReservationResponses
        {
            get => _reservationResponses;
            set
            {
                if (SetProperty(ref _reservationResponses, value))
                {
                    FilterReservations();
                }
            }
        }

        public ObservableCollection<ReservationResponse> FilteredReservationResponses
        {
            get => _filteredReservationResponses;
            set => SetProperty(ref _filteredReservationResponses, value);
        }

        public ObservableCollection<ApplicationStatusResponse> ApplicationStatuses
        {
            get => _applicationStatusResponses;
            set => SetProperty(ref _applicationStatusResponses, value);
        }

        public ApplicationStatus SelectedStatus2
        {
            get => _selectedStatus2;
            set
            {
                if (SetProperty(ref _selectedStatus2, value))
                {
                    FilterReservations();
                }
            }
        }

        private async void InitializeAsync()
        {
            await LoadingStatus();
            await LoadingDataRequests();
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
            catch (Exception ex)
            {
                _messageService.ShowPopup("Ошибка", "Не удалось загрузить статусы");
                Debug.WriteLine(ex);
            }
        }

        private void FilterReservations()
        {
            if (SelectedStatus2 == null)
            {
                FilteredReservationResponses = new ObservableCollection<ReservationResponse>(ReservationResponses);
            }
            else
            {
                FilteredReservationResponses = new ObservableCollection<ReservationResponse>(
                    ReservationResponses.Where(r => r.ApplicationStatus.StatusName == SelectedStatus2.StatusName));
            }
        }

        private void CallClient(ReservationResponse reservation)
        {
            var phoneNumber = reservation.Client.PhoneNumber;
            try
            {
                PhoneDialer.Open(phoneNumber);
            }
            catch (FeatureNotSupportedException ex)
            {
                _messageService.ShowPopup("Ошибка", "Телефонный набор не поддерживается на этом устройстве");
            }
            catch (Exception ex)
            {
                _messageService.ShowPopup("Ошибка", "Не удалось выполнить звонок");
            }
        }

        private ReservationResponse _currentSelected;
        private async void EditAppStatus()
        {
            int id = WhatIsIdStatus();
            try
            {
                if (id != 0)
                {
                    await _adminService.CreateNewApplicationStatusAsync(id, _currentSelected.RequestBooking.Id);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error-" + ex);
            }
            PopupAppStatusIsVisible = false;
            InitializeAsync();
        }

        private async void EditPayStatus()
        {
            int id = WhatIsPayStatus();
            try
            {
                if (id != 0)
                {
                    await _adminService.CreateNewPaymenеStatusAsync(id, _currentSelected.RequestBooking.Id);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error-" + ex);
            }
            PopPayStatusIsVisible = false;
            InitializeAsync();
        }

        private void OpenPopupAppStatus(ReservationResponse selectedReservation)
        {
            PopupAppStatusIsVisible = true;
            _currentSelected = selectedReservation;
        }

        private void OpenPopupPayStatus(ReservationResponse selectedReservation)
        {
            PopPayStatusIsVisible = true;
            _currentSelected = selectedReservation;
        }

        private int WhatIsIdStatus()
        {
            if (IsConfirmed)
            {
                return 3;
            }
            else if (IsCancelled)
            {
                return 4;
            }
            else if (IsUnderReview)
            {
                return 2;
            }
            return 0;
        }

        private int WhatIsPayStatus()
        {
            if (IsAwaitingPrepayment)
            {
                return 6;
            }
            else if (IsPrepaid)
            {
                return 2;
            }
            else if (IsPaid)
            {
                return 3;
            }
            else if (IsRefunded)
            {
                return 5;
            }
            return 0;
        }

        private void CancelPopupApp()
        {
            PopupAppStatusIsVisible = false;
        }

        private void CancelPopupPay()
        {
            PopPayStatusIsVisible = false;
        }

        public bool PopupAppStatusIsVisible
        {
            get => _popupAppStatusIsVisible;
            set => SetProperty(ref _popupAppStatusIsVisible, value);
        }

        public bool PopPayStatusIsVisible
        {
            get => _popupPayStatusIsVisible;
            set => SetProperty(ref _popupPayStatusIsVisible, value);
        }

        public bool IsConfirmed
        {
            get => isConfirmed;
            set
            {
                if (SetProperty(ref isConfirmed, value))
                {
                    FilterReservations();
                }
            }
        }

        public bool IsCancelled
        {
            get => isCancelled;
            set
            {
                if (SetProperty(ref isCancelled, value))
                {
                    FilterReservations();
                }
            }
        }

        public bool IsUnderReview
        {
            get => isUnderReview;
            set
            {
                if (SetProperty(ref isUnderReview, value))
                {
                    FilterReservations();
                }
            }
        }
        public bool IsAwaitingPrepayment
        {
            get => _isAwaitingPrepayment;
            set => SetProperty(ref _isAwaitingPrepayment, value);
        }

        public bool IsPrepaid
        {
            get => _isPrepaid;
            set => SetProperty(ref _isPrepaid, value);
        }

        public bool IsPaid
        {
            get => _isPaid;
            set => SetProperty(ref _isPaid, value);
        }

        public bool IsRefunded
        {
            get => _isRefunded;
            set => SetProperty(ref _isRefunded, value);
        }
    }
}