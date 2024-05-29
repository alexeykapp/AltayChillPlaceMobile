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
    public partial class Houses : ContentPage
    {
        public Houses()
        {
            InitializeComponent();
            BindingContext = App.AppInitializer.ServiceProvider.GetService<HousesVM>();
        }
        private void OnItemTapped(object sender, EventArgs e)
        {
            if (BindingContext is HousesVM viewModel && sender is ListView listView)
            {
                viewModel.ItemTappedCommand.Execute(listView.SelectedItem);
                listView.SelectedItem = null;
            }
        }
    }
}