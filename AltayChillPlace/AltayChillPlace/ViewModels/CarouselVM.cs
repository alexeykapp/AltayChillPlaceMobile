using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using AltayChillPlace.CustomXaml;

namespace AltayChillPlace.ViewModels
{
    public class CarouselVM
    {
        public ObservableCollection<SlideData> ImageCollection { get; set; }

        public CarouselVM()
        {
            ImageCollection = new ObservableCollection<SlideData>();

            ImageCollection.Add(new SlideData
            {
                Title = "Добро пожаловать",
                Description = "Добро пожаловать в мир загадочных гор Алтая, где дикие вершины сливаются с невероятным духом природы. Погрузитесь в атмосферу гор  и позвольте Altay Chill Place воплотить ваши мечты.",
                BackgroundImage = ImageSource.FromFile("Carousel1")
            });
            ImageCollection.Add(new SlideData
            {
                Title= "Серенада гор",
                Description = "Разорвите оковы городской суеты, прогрузившись в объятия естественной красоты и гармонии природы.Здесь вас ждет душевное спокойствие и великолепие гор, укутанных в шелест ветра.",
                BackgroundImage = ImageSource.FromFile("Carousel2")
            });
            ImageCollection.Add(new SlideData
            {
                Title= "Ждем Вас с нетерпением",
                Description = "Выделите время для уютного убежища в окружении природы - забронируйте наши прекрасные домики прямо сейчас и обеспечьте себе незабываемый отдых.",
                BackgroundImage = ImageSource.FromFile("Carousel3"),
                IsVisibleButton = true,
                IsVisibleButtonSkip = false
            });
        }
    }
}
