using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AltayChillPlace.CustomXaml
{
    public class SlideData
    {
        public string Title { get; set; }
        public ImageSource BackgroundImage { get; set; }
        public string Description { get; set; }
        public bool IsVisibleButton { get; set; } = false;
        public bool IsVisibleButtonSkip { get; set; } = true;
    }
}
