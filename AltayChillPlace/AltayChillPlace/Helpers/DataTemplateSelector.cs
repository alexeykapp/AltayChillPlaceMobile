using System;
using System.Collections.Generic;
using System.Text;
using AltayChillPlace.ApiResponses;
using Xamarin.Forms;

namespace AltayChillPlace.Helpers
{
    public class MyDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate Template1 { get; set; }
        public DataTemplate Template2 { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is HouseResponse)
                return Template1;
            else if (item is AdditionalServiceResponse)
                return Template2;

            return null;
        }
    }
}
