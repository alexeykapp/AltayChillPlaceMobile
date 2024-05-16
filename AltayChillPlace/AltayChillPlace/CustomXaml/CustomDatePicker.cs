using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AltayChillPlace.CustomXaml
{
    public class CustomDatePicker : DatePicker
    {
        public static readonly BindableProperty MinimumDateProperty =
            BindableProperty.Create(nameof(MinimumDate), typeof(DateTime), typeof(CustomDatePicker), DateTime.MinValue, propertyChanged: (bindable, oldValue, newValue) =>
            {
                var datePicker = (CustomDatePicker)bindable;
                datePicker.MinimumDate = (DateTime)newValue;
                // Убедитесь, что выбранная дата не меньше минимальной даты
                if (datePicker.Date < datePicker.MinimumDate)
                {
                    datePicker.Date = datePicker.MinimumDate;
                }
            });

        public DateTime MinimumDate
        {
            get { return (DateTime)GetValue(MinimumDateProperty); }
            set { SetValue(MinimumDateProperty, value); }
        }

        public static readonly BindableProperty MaximumDateProperty =
            BindableProperty.Create(nameof(MaximumDate), typeof(DateTime), typeof(CustomDatePicker), DateTime.MaxValue, propertyChanged: (bindable, oldValue, newValue) =>
            {
                var datePicker = (CustomDatePicker)bindable;
                datePicker.MaximumDate = (DateTime)newValue;
                // Убедитесь, что выбранная дата не больше максимальной даты
                if (datePicker.Date > datePicker.MaximumDate)
                {
                    datePicker.Date = datePicker.MaximumDate;
                }
            });

        public DateTime MaximumDate
        {
            get { return (DateTime)GetValue(MaximumDateProperty); }
            set { SetValue(MaximumDateProperty, value); }
        }
    }
}
