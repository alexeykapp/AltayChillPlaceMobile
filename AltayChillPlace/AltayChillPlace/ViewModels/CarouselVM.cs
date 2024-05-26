using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace AltayChillPlace.ViewModels
{
    public class CarouselVM
    {
        public ObservableCollection<ImageSource> ImageCollection { get; set; }

        public CarouselVM()
        {
            ImageCollection = new ObservableCollection<ImageSource>();

            ImageCollection.Add(ImageSource.FromFile("Carouse1"));
            ImageCollection.Add(ImageSource.FromFile("Carouse2"));
        }
    }
}
