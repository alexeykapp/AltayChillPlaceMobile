using AltayChillPlace.ApiResponses;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace AltayChillPlace.Helpers
{
    public class SelectableTypeHouse : BindableBase
    {
        private bool _isSelected;
        public TypeHouse TypeHouse { get; set; }

        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        public string NameTypeHouse => TypeHouse?.NameTypeHouse;
    }
}
