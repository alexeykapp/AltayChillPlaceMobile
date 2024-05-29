using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace AltayChillPlace.ApiResponses
{
    public class TypeHouse : BindableBase
    {
        private bool _isSelected;
        [JsonProperty("id_type_of_number")]
        public int IdTypeHouse { get; set; }
        [JsonProperty("room_type_name")]
        public string NameTypeHouse { get; set; }
        public bool IsSelected
        {
            get => _isSelected; 
            set => SetProperty(ref _isSelected, value);
        }

    }
}
