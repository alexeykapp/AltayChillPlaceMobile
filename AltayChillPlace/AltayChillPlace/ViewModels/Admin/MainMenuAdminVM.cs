using AltayChillPlace.NavigationFile;
using Prism.Commands;
using Prism.Mvvm;
using AltayChillPlace.Views.Admin;
using AltayChillPlace.Views;
using System;
using System.Collections.Generic;
using System.Text;
using AltayChillPlace.Services;

namespace AltayChillPlace.ViewModels.Admin
{
    public class MainMenuAdminVM : BindableBase
    {
        TokenService tokenService;
        public MainMenuAdminVM(TokenService tokenService)
        {
            OpenReservationPageCommand = new DelegateCommand(OpenReservationPage);
            OpenNewPostCommand = new DelegateCommand(OpenNewPost);
            OpenBlogEditorCommand = new DelegateCommand(OpenBlogEditor);
            LogOutCommand = new DelegateCommand(LogOut);
            this.tokenService = tokenService;
        }
        public DelegateCommand OpenReservationPageCommand {  get; set; }
        public DelegateCommand OpenNewPostCommand { get; set; }
        public DelegateCommand OpenBlogEditorCommand { get; set; }
        public DelegateCommand LogOutCommand { get; set; }
        private void OpenReservationPage()
        {
            NavigationDispatcher.Instance.Navigation.PushAsync(new BookingRequestsAdmin());
        }
        private void OpenNewPost()
        {
            NavigationDispatcher.Instance.Navigation.PushAsync(new NewPost());
        }
        private void OpenBlogEditor()
        {
            NavigationDispatcher.Instance.Navigation.PushAsync(new BlogAdmin());
        }
        private async void LogOut()
        {
            await tokenService.LogOut();
            await NavigationDispatcher.Instance.PushAndRemovePreviousAsync(new Autorization());
        } 
    }
}
