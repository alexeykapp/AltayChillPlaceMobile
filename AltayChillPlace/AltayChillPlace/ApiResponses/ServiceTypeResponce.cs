using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace AltayChillPlace.ApiResponses
{
    public class ServiceTypeResponce : BindableBase
    {
        private bool _isSelected;
        public int IdTypeService {  get; set; }
        public string ServiceName { get; set; }

        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

    }
}
