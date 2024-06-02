using AltayChillPlace.ApiResponses;
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
    public partial class Booking : ContentPage
    {
        public Booking(HouseResponse house)
        {
            InitializeComponent();
            BindingContext = App.AppInitializer.ServiceProvider.GetService<BookingVM>();
            InitIdHouse(house);
        }
        private void InitIdHouse(HouseResponse house)
        {
            if (BindingContext is BookingVM viewModel)
            {
                viewModel.House = house;
            }
        }
    }
}