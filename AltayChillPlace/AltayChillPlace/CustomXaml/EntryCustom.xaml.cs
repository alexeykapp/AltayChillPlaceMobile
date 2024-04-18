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
	public partial class EntryCustom : ContentView
	{
        public static readonly BindableProperty IconSourceProperty =
            BindableProperty.Create(nameof(IconSource), typeof(ImageSource), typeof(EntryCustom), default(ImageSource));

        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(EntryCustom), default(string));
        public EntryCustom ()
		{
			InitializeComponent ();
            BindingContext = this;
		}
        public ImageSource IconSource
        {
            get => (ImageSource)GetValue(IconSourceProperty);
            set => SetValue(IconSourceProperty, value);
        }

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }
    }
}