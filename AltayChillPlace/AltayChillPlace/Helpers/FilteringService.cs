using AltayChillPlace.ApiResponses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AltayChillPlace.Helpers
{
    public class FilteringService
    {
        public FilteringService()
        {

        }
        public List<HouseResponse> FilteringHousesByCategory(IEnumerable<HouseResponse> houses, int idCategory)
        {
            List<HouseResponse> housesFilter = new List<HouseResponse>();
            housesFilter = houses.Where(x => x.FkTypeOfNumber == idCategory).ToList();
            return housesFilter;
        }
        public List<AdditionalServiceResponse> FilteringServicesByCategory(IEnumerable<AdditionalServiceResponse> services, int idType)
        {
            List<AdditionalServiceResponse> servicesFilter = new List<AdditionalServiceResponse>();
            servicesFilter = services.Where(x => x.FkServiceType == idType).ToList();
            return servicesFilter;
        }
    }
}
