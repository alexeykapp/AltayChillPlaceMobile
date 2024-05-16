using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AltayChillPlace.CustomXaml
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EntryDateMask : ContentView
    {
        public EntryDateMask()
        {
            InitializeComponent();
            datePicker.DateSelected += OnDateSelected;
        }

        public static readonly BindableProperty DateProperty =
            BindableProperty.Create(nameof(Date), typeof(DateTime), typeof(EntryDateMask), default(DateTime), BindingMode.TwoWay);
        public DateTime Date
        {
            get => (DateTime)GetValue(DateProperty);
            set => SetValue(DateProperty, value);
        }
        private void OnDateSelected(object sender, DateChangedEventArgs e)
        {
            if (Date != e.NewDate)
                Date = e.NewDate; // Обновляет свойство Date, когда пользователь выбирает дату
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == nameof(Date) && datePicker != null)
            {
                if (datePicker.Date != Date)
                    datePicker.Date = Date;
            }
        }
    }
}