using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static System.Net.Mime.MediaTypeNames;

namespace AltayChillPlace.CustomXaml
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EntryPhoneMask : ContentView
    {
        public static readonly BindableProperty IconSourceProperty =
            BindableProperty.Create(nameof(IconSource), typeof(ImageSource), typeof(EntryPhoneMask), default(ImageSource));


        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(EntryPhoneMask), default(string), BindingMode.TwoWay);
        public EntryPhoneMask()
        {
            InitializeComponent(); 
            //entry.Text = Text;
            entry.TextChanged += OnTextChanged;
        }
        public ImageSource IconSource
        {
            get => (ImageSource)GetValue(IconSourceProperty);
            set => SetValue(IconSourceProperty, value);
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (Text != e.NewTextValue)
                Text = e.NewTextValue; // Обновляет свойство Text, когда пользователь вводит текст в Entry
        }
        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == nameof(Text) && entry != null)
            {
                if (propertyName == nameof(Text) && entry != null)
                {
                    if (entry.Text != Text)
                        entry.Text = Text;
                }
            }
        }
    }
}