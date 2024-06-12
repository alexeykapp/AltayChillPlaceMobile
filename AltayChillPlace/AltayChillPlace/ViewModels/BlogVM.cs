using AltayChillPlace.ApiResponses;
using AltayChillPlace.Interface;
using AltayChillPlace.Services;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AltayChillPlace.ViewModels
{
    public class BlogVM : BindableBase
    {
        private readonly IBlogServices _blogServices;
        private readonly MessageService _messageService;
        private ObservableCollection<BlogPostsResponce> _posts;
        private bool _isRefreshing;

        public BlogVM(IBlogServices blogServices)
        {
            _blogServices = blogServices;
            _messageService = new MessageService();
            RefreshCommand = new DelegateCommand(async () => await RefreshDataAsync());
            LoadingPosts();
        }

        private async void LoadingPosts()
        {
            IsRefreshing = true;
            try
            {
                Posts = await _blogServices.GetBlogPostsResponces();
            }
            catch (Exception ex)
            {
                _messageService.ShowPopup("Ошибка", "Произошла ошибка\nПовторите прозже");
            }
            finally
            {
                IsRefreshing = false;
            }
        }

        private async Task RefreshDataAsync()
        {
            IsRefreshing = true;
            try
            {
                Posts = await _blogServices.GetBlogPostsResponces();
            }
            catch (Exception ex)
            {
                _messageService.ShowPopup("Ошибка", "Произошла ошибка\nПовторите прозже");
            }
            finally
            {
                IsRefreshing = false;
            }
        }

        public ObservableCollection<BlogPostsResponce> Posts
        {
            get => _posts;
            set => SetProperty(ref _posts, value);
        }

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        public DelegateCommand RefreshCommand { get; }
    }
}