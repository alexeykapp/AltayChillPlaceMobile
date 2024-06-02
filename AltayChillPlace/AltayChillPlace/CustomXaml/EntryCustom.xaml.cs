using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
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

        public static readonly BindableProperty IsPasswordProperty =
            BindableProperty.Create(nameof(IsPasswordEntry), typeof(bool), typeof(EntryCustom), default(bool));
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(EntryCustom), default(string), BindingMode.TwoWay);
        public static readonly BindableProperty BorderColorProperty =
     BindableProperty.Create(nameof(ColorBorder), typeof(Color), typeof(EntryCustom), Color.FromHex("#EFF5FB"));
        public static readonly BindableProperty TextColorProperty =
   BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(EntryCustom), Color.White);
        public EntryCustom()
        {
            InitializeComponent();
            entry.Text = Text;
            entry.IsPassword = IsPasswordEntry;
            entry.TextChanged += OnTextChanged;
        }
        public Color ColorBorder
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }
        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
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
        public bool IsPasswordEntry
        {
            get => (bool)GetValue(IsPasswordProperty);
            set => SetValue(IsPasswordProperty, value);
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            Text = e.NewTextValue; // Обновляет свойство Text, когда пользователь вводит текст в Entry
        }
        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == nameof(Text) && entry != null)
            {
                entry.Text = Text;
            }

            if (propertyName == nameof(IsPasswordEntry))
            {
                entry.IsPassword = IsPasswordEntry;
            }
        }
    }
}