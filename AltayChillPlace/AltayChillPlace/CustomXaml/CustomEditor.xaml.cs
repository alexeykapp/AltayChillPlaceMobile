using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AltayChillPlace.CustomXaml
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CustomEditor : ContentView
	{
		public CustomEditor ()
		{
			InitializeComponent ();
            editor.TextChanged += Editor_TextChanged;
        }
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(CustomEditor), default(string),
                BindingMode.TwoWay, propertyChanged: OnTextChanged);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomEditor)bindable;
            control.editor.Text = (string)newValue;
        }

        private void Editor_TextChanged(object sender, TextChangedEventArgs e)
        {
            Text = e.NewTextValue;
        }

        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(CustomEditor), default(string));

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            editor.BindingContext = BindingContext;
        }
    }
}