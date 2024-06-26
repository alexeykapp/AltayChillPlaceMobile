﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AltayChillPlace.Configuration;
using AltayChillPlace.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AltayChillPlace.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Autorization : ContentPage
    {
        public Autorization()
        {
            InitializeComponent();

            BindingContext = App.AppInitializer.ServiceProvider.GetService<AutorizationVM>();
        }
    }
}