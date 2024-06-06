using AltayChillPlace.ApiResponses;
using AltayChillPlace.Interface;
using AltayChillPlace.NavigationFile;
using AltayChillPlace.Services;
using AltayChillPlace.Views;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Diagnostics;

namespace AltayChillPlace.ViewModels
{
    public class ProfileVM : BindableBase
    {
        private IProfileService _profileService;
        private PersonResponce _personResponce;
        private MessageService _messageService;
        private bool _isEnabledSaveButton = false;
        public ProfileVM(IProfileService profileService)
        {
            _profileService = profileService;
            _messageService = new MessageService();
            SaveСhangesCommand = new DelegateCommand(SaveChanges);
            LogOutCommand = new DelegateCommand(LogOut);
            LoadingProfileInfo();
        }
        public DelegateCommand SaveСhangesCommand { get; set; }
        public DelegateCommand LogOutCommand { get; set; }
        private async void SaveChanges()
        {
            try
            {
                var resultUpdate = await _profileService.UpdateProfileAsync(PersonResponce);
                if (resultUpdate != null)
                {
                    PersonResponce = resultUpdate;
                    _messageService.ShowPopup("Успешно", "Данные успешно обновленны");
                }
                else
                {
                    _messageService.ShowPopup("Ошибка", "Повторите позже");
                }

            }
            catch (Exception ex)
            {
                _messageService.ShowPopup("Ошибка", "Повторите позже");
                Debug.WriteLine(ex);
            }
        }
        private async void LoadingProfileInfo()
        {
            try
            {
                var response = await _profileService.GetPersonResponce();
                PersonResponce = response;
            }
            catch (Exception ex)
            {
                _messageService.ShowPopup("Ошибка", "Повторите попытку позже");
            }
        }
        private async void LogOut()
        {
            await NavigationDispatcher.Instance.PushAndRemovePreviousAsync(new Autorization());
        }
        public bool IsEnabledSaveButton
        {
            get => _isEnabledSaveButton;
            set => SetProperty(ref _isEnabledSaveButton, value);
        }
        public PersonResponce PersonResponce
        {
            get => _personResponce;
            set
            {
                if (_personResponce != null)
                {
                    _isEnabledSaveButton = false;
                }
                SetProperty(ref _personResponce, value);
            }
        }
    }
}
