using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace AltayChillPlace.ViewModels
{
    public class HouseInfoPageVM : BindableBase
    {
        private int _idHouse;
        public int IdHouse
        {
            get => _idHouse;
            set => SetProperty(ref _idHouse, value);
        }
    }
}
