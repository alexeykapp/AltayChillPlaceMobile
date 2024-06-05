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
	public partial class HistoryBooking : ContentPage
	{
		public HistoryBooking ()
		{
			InitializeComponent ();
            BindingContext = App.AppInitializer.ServiceProvider.GetService<HistoryBookingVM>();
        }
	}
}