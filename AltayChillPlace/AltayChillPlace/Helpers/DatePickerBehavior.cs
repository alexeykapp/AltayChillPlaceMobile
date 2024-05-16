using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AltayChillPlace.Helpers
{
    public class DatePickerBehavior : Behavior<DatePicker>
    {
        public static readonly BindableProperty CommandProperty
            = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(DatePickerBehavior), null);

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        protected override void OnAttachedTo(DatePicker datePicker)
        {
            datePicker.DateSelected += DatePicker_DateSelected;
            datePicker.PropertyChanged += DatePicker_PropertyChanged;
            base.OnAttachedTo(datePicker);
        }

        protected override void OnDetachingFrom(DatePicker datePicker)
        {
            datePicker.DateSelected -= DatePicker_DateSelected;
            base.OnDetachingFrom(datePicker);
        }

        private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            var datePicker = (DatePicker)sender;
            datePicker.Format = "dd/MM/yyyy";

            if (Command != null && Command.CanExecute(null))
            {
                Command.Execute(null);
            }
        }

        private void DatePicker_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var datePicker = (DatePicker)sender;

            // Обработка событий изменения MinimumDate и MaximumDate 
            if (e.PropertyName == DatePicker.MinimumDateProperty.PropertyName ||
                e.PropertyName == DatePicker.MaximumDateProperty.PropertyName)
            {
                // Если введенное значение Date меньше MinimumDate или больше MaximumDate,
                // установите значение Date равным новым минимальному или максимальному значению соответственно.
                if (datePicker.Date < datePicker.MinimumDate)
                {
                    datePicker.Date = datePicker.MinimumDate;
                }
                else if (datePicker.Date > datePicker.MaximumDate)
                {
                    datePicker.Date = datePicker.MaximumDate;
                }
            }
        }
    }
}
