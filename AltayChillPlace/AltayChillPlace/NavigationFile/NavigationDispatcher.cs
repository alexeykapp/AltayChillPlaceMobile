using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AltayChillPlace.NavigationFile
{
    public class NavigationDispatcher
    {
        private static NavigationDispatcher _instance;

        private INavigation _navigation;

        public static NavigationDispatcher Instance =>
            _instance ?? (_instance = new NavigationDispatcher());

        public INavigation Navigation =>
            _navigation ?? throw new Exception("NavigationDispatcher is not initialized");

        public void Initialize(INavigation navigation)
        {
            _navigation = navigation;
        }
        public void OpenAsRoot(Page page)
        {
            Application.Current.MainPage = new NavigationPage(page);
        }
        public async Task PushAndRemovePreviousAsync(Page page)
        {
            await Navigation.PushAsync(page);

            if (Navigation.NavigationStack.Count > 1)
            {
                for (var i = 0; i < Navigation.NavigationStack.Count - 1; i++)
                {
                    Navigation.RemovePage(Navigation.NavigationStack[i]);
                }
            }
        }
    }
}
