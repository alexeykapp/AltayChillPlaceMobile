using AltayChillPlace.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AltayChillPlace.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HouseInfoPage : ContentPage
    {
        public HouseInfoPage(int id)
        {
            InitializeComponent();
            BindingContext = App.AppInitializer.ServiceProvider.GetService<HousesVM>();
            InitIdHouse(id);
        }
        private void InitIdHouse(int id)
        {
            if (BindingContext is HouseInfoPageVM viewModel)
            {
                viewModel.IdHouse = id;
            }
        }
    }
}