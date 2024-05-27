using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AltayChillPlace.CustomXaml
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HeaderContentView : ContentPage
	{
		public HeaderContentView ()
		{
			InitializeComponent ();
		}
	}
}