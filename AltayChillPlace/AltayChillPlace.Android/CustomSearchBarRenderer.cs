using Android.Content;
using Android.Graphics;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using YourAppNamespace.Droid;

[assembly: ExportRenderer(typeof(SearchBar), typeof(CustomSearchBarRenderer))]
namespace YourAppNamespace.Droid
{
    public class CustomSearchBarRenderer : SearchBarRenderer
    {
        public CustomSearchBarRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                // Применение кастомного стиля
                var searchView = Control as SearchView;
                searchView.SetQueryHint("Search");
                searchView.SetIconifiedByDefault(false);
                searchView.SetBackgroundColor(Android.Graphics.Color.Transparent);

                // Изменение цвета иконки лупы
                int searchIconId = Control.Resources.GetIdentifier("android:id/search_mag_icon", null, null);
                var searchIcon = Control.FindViewById<ImageView>(searchIconId);
                if (searchIcon != null)
                {
                    searchIcon.SetColorFilter(Android.Graphics.Color.ParseColor("#576B57"), PorterDuff.Mode.SrcIn); // HEX-цвет для иконки лупы
                }
            }
        }
    }
}