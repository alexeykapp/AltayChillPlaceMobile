using AltayChillPlace.ApiResponses;
using AltayChillPlace.Interface;
using AltayChillPlace.Services;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;

namespace AltayChillPlace.ViewModels.Admin
{
    public class BlogAdminVM : BindableBase
    {
        private IBlogServices _blogServices;
        private IMessageService _messageService;
        private ObservableCollection<BlogPostsResponce> _posts;
        public BlogAdminVM(IBlogServices blogServices, IMessageService messageService)
        {
            _blogServices = blogServices;
            _messageService = messageService;
            LoadingPosts();
        }
        private async void LoadingPosts()
        {
            try
            {
                Posts = await _blogServices.GetBlogPostsResponces();
            }
            catch (Exception ex)
            {
                _messageService.ShowPopup("Ошибка", "Произошла ошибка\nПовторите прозже");
            }
        }
        public ObservableCollection<BlogPostsResponce> Posts
        {
            get => _posts;
            set => SetProperty(ref _posts, value);
        }
    }
}
