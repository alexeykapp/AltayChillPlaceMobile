using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace AltayChillPlace.ApiResponses
{
    public class ServiceTypeResponce : BindableBase
    {
        private bool _isSelected;
        [JsonProperty("id_service_type")]
        public int IdTypeService {  get; set; }
        [JsonProperty("name_of_service_type")]
        public string ServiceTypeName { get; set; }

        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

    }
}
