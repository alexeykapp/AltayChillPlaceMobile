using AltayChillPlace.ApiResponses;
using AltayChillPlace.Interface;
using AltayChillPlace.Services;
using Prism.Modularity;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AltayChillPlace.ViewModels
{
    public class BlogVM : BindableBase
    {
        private IBlogServices _blogServices;
        private MessageService _messageService;
        private ObservableCollection<BlogPostsResponce> _posts;
        public BlogVM(IBlogServices blogServices)
        {
            _blogServices = blogServices;
            _messageService = new MessageService();
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
