using System;
using System.Diagnostics;
using AltayChillPlace.Configuration;
using AltayChillPlace.NavigationFile;
using AltayChillPlace.Services;
using AltayChillPlace.Views;
using AltayChillPlace.Views.Admin;
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
                var isAdmin = await tokenService.CheckIsAdmin();

                if (resultValidate)
                {
                    if (isAdmin)
                    {
                        MainPage = new NavigationPage(new MainMenuAdmin());
                    }
                    else
                    {
                        MainPage = new NavigationPage(new Houses());
                    }
                }
                else
                {
                    MainPage = new NavigationPage(new Autorization());
                }
                // MainPage = resultValidate ? new NavigationPage(new Houses()) : new NavigationPage(new Autorization());

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

