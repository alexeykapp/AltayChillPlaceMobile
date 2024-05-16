using AltayChillPlace.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace AltayChillPlace.ViewModels
{
    public class HousesVM
    {
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public HousesVM(IDataTransferService dataTransferService)
        {
            ArrivalDate = (DateTime)dataTransferService.GetData("arrival");
            DepartureDate = (DateTime)dataTransferService.GetData("departure");
        }
    }
}
