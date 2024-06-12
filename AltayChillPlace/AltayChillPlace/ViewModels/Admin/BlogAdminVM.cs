using AltayChillPlace.ApiResponses;
using AltayChillPlace.Interface;
using AltayChillPlace.Services;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AltayChillPlace.ViewModels.Admin
{
    public class BlogAdminVM : BindableBase
    {
        private IBlogServices _blogServices;
        private IMessageService _messageService;
        private IAdminService _adminService;
        private ObservableCollection<BlogPostsResponce> _posts;
        private bool _isRefreshing;

        public BlogAdminVM(IBlogServices blogServices, IMessageService messageService, IAdminService adminService)
        {
            _blogServices = blogServices;
            _messageService = messageService;
            _adminService = adminService;
            DeletePostCommand = new DelegateCommand<BlogPostsResponce>(DeletePost);
            RefreshCommand = new DelegateCommand(OnRefresh);
            Initialization();
        }

        public async void Initialization()
        {
            await LoadingPosts();
        }

        private async Task LoadingPosts()
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

        public async void DeletePost(BlogPostsResponce selected)
        {
            var result = await _messageService.AskUserYesNo("Подтверждение", "Вы уверены, что хотите удалить данную запись?");
            if (result)
            {
                await _adminService.DeletePostBlog(selected.Id);
                await LoadingPosts();
            }
        }

        private async void OnRefresh()
        {
            await LoadingPosts();
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

        public DelegateCommand<BlogPostsResponce> DeletePostCommand { get; set; }
        public DelegateCommand RefreshCommand { get; set; }
    }
}