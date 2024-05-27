using AltayChillPlace.ApiResponses;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AltayChillPlace.Helpers
{
    public class HeaderTemplateSelector : DataTemplateSelector
    {
        public DataTemplate HouseHeader { get; set; }
        public DataTemplate ServiceHeader { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is HouseResponse)
                return HouseHeader;

            return ServiceHeader;
        }
    }
}
