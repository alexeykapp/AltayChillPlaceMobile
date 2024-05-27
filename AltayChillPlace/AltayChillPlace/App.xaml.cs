using System;
using System.Diagnostics;
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
            //MainPage = new NavigationPage(new Carousel());
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
            try
            {
                TokenService tokenService = new TokenService();
                var resultValidate = await tokenService.IsTokenValidAsync();

                // Инициализация MainPage в зависимости от результата проверки токена
                MainPage = resultValidate ? new NavigationPage(new Houses()) : new NavigationPage(new Autorization());

                // Общие настройки
                NavigationPage.SetHasNavigationBar(MainPage, false);
                NavigationDispatcher.Instance.Initialize(MainPage.Navigation);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }
    }
}

