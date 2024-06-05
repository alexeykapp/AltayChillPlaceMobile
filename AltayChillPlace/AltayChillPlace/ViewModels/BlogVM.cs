using AltayChillPlace.ApiResponses;
using AltayChillPlace.Interface;
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
        private ObservableCollection<BlogPostsResponce> _posts;
        public BlogVM(IBlogServices blogServices)
        {
            _blogServices = blogServices;
        }
        public ObservableCollection<BlogPostsResponce> Posts
        {
            get => _posts;
            set => SetProperty(ref _posts, value);
        }
    }
}
