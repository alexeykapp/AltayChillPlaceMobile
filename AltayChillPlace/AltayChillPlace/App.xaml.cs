using System;
using AltayChillPlace.Configuration;
using Xamarin.Forms;
using AltayChillPlace.Views;
using Xamarin.Forms.Xaml;

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
            MainPage = new Autorization();
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
    }
}
