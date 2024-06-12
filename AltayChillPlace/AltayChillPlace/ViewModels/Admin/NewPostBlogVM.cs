using AltayChillPlace.Helpers;
using AltayChillPlace.Interface;
using AltayChillPlace.NavigationFile;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Essentials;
using Xamarin.CommunityToolkit.Extensions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AltayChillPlace.ViewModels.Admin
{
    public class NewPostBlogVM : BindableBase
    {
        private readonly IAdminService _adminService;
        private byte[] selectedPhotos;
        private string title;
        private string description;
        private bool isVisibleMainPhoto = true;
        private bool isVisibleSelectedPhoto = false;
        private bool isBusy;
        private Page currentPage;
        private Func<Task> func = () => Task.CompletedTask;

        public NewPostBlogVM(IAdminService adminService)
        {
            _adminService = adminService;
            currentPage = NavigationDispatcher.GetCurrentPage();
            OpenSelectedProtoCommand = new DelegateCommand(OpenSelectedPhoto);
            PublicationCommand = new DelegateCommand(Publication);
        }

        public DelegateCommand OpenSelectedProtoCommand { get; set; }
        public DelegateCommand PublicationCommand { get; set; }

        private async void OpenSelectedPhoto()
        {
            var byteArr = await ImageConverter.PickPhotoAsync();
            if (byteArr != null)
            {
                IsVisibleMainPhoto = false;
                SelectedPhotos = byteArr;
                IsVisibleSelectedPhoto = true;
            }
        }

        private async void Publication()
        {
            IsBusy = true;
            try
            {
                bool check = IsEmptyEntry();
                if (check != false)
                {
                    var newPost = await _adminService.CreateNewPostBlogAsync(Title, Description, DateTime.Now.ToString("d"), SelectedPhotos);
                    Success();
                    await currentPage.DisplaySnackBarAsync("Пост успешно опубликован!", "", func, duration: TimeSpan.FromSeconds(3));
                }
                else
                {
                    await currentPage.DisplaySnackBarAsync("Проверьте все поля", "", func, duration: TimeSpan.FromSeconds(3));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error-" + ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private bool IsEmptyEntry()
        {
            if (string.IsNullOrEmpty(Description) || string.IsNullOrEmpty(Title) || SelectedPhotos == null)
            {
                return false;
            }
            return true;
        }

        public byte[] SelectedPhotos
        {
            get => selectedPhotos;
            set => SetProperty(ref selectedPhotos, value);
        }

        public bool IsVisibleMainPhoto
        {
            get => isVisibleMainPhoto;
            set => SetProperty(ref isVisibleMainPhoto, value);
        }

        public bool IsVisibleSelectedPhoto
        {
            get => isVisibleSelectedPhoto;
            set => SetProperty(ref isVisibleSelectedPhoto, value);
        }

        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        private void Success()
        {
            IsVisibleSelectedPhoto = false;
            IsVisibleMainPhoto = true;
            SelectedPhotos = null;
            Title = string.Empty;
            Description = string.Empty;
        }
    }
}