using AltayChillPlace.Configuration;
using AltayChillPlace.NavigationFile;
using AltayChillPlace.Services;
using AltayChillPlace.Views;
using Xamarin.Forms;

namespace AltayChillPlace
{
    public partial class App : Application
    {
        public static AppInitializer AppInitializer { get; private set; }
        public App()
        {
            InitializeComponent();

            AppInitializer = new AppInitializer();
            AppInitializer.Initialize();
            NavigateMainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private async void NavigateMainPage()
        {
            TokenService tokenService = new TokenService();
            var resultValidate = tokenService.IsTokenValidAsync().Result;
            if (resultValidate)
            {
                MainPage = new NavigationPage(new Houses());
            }
            MainPage = new NavigationPage(new Autorization());

            MainPage = new NavigationPage(new Autorization());
            NavigationPage.SetHasNavigationBar(MainPage, false);
            NavigationDispatcher.Instance.Initialize(MainPage.Navigation);
        }
    }

}
