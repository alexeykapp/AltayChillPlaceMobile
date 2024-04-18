using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AltayChillPlace.Droid;
using Android.Content;

[assembly: ExportRenderer(typeof(Entry), typeof(CustomEntryRenderer))]
namespace AltayChillPlace.Droid
{
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                // Убираем подчеркивание
                Control.Background = null;

                // Дополнительно можно установить минимальную высоту, если нужно
                Control.SetMinHeight(0);
            }
        }
    }
}
