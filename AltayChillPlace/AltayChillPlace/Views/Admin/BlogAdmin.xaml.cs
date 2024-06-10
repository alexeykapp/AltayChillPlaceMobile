using AltayChillPlace.ViewModels.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AltayChillPlace.Views.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BlogAdmin : ContentPage
    {
        public BlogAdmin()
        {
            InitializeComponent();
            BindingContext = App.AppInitializer.ServiceProvider.GetService<BlogAdminVM>();
        }
    }
}