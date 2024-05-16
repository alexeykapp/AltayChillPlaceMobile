using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AltayChillPlace.CustomXaml
{
    public class ScaledImage : Image
    {
        public static readonly BindableProperty ScrollViewProperty = BindableProperty.Create(nameof(ScrollView), typeof(ScrollView), typeof(ScaledImage), null, propertyChanged: OnScrollViewChanged);

        public ScrollView ScrollView
        {
            get => (ScrollView)GetValue(ScrollViewProperty);
            set => SetValue(ScrollViewProperty, value);
        }

        private static void OnScrollViewChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var image = (ScaledImage)bindable;
            var scrollView = (ScrollView)newValue;

            scrollView.SizeChanged += (sender, e) => image.ScaleTo(scrollView.Width / image.Width, (uint)(scrollView.Height / image.Height));
        }
    }
}
