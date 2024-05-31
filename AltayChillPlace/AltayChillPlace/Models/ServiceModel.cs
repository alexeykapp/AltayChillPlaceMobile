using AltayChillPlace.ApiResponses;
using AltayChillPlace.Interface;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AltayChillPlace.Models
{
    public class ServiceModel
    {
        private readonly IServicesService _servicesService;
        public ServiceModel(IServicesService servicesService)
        {
            _servicesService = servicesService;
        }
        public async Task<ObservableCollection<AdditionalServiceResponse>> GetAllServices()
        {
            return await _servicesService.GetAllServies();
        }
        public async Task<ObservableCollection<ServiceTypeResponce>> GetServicesType()
        {
            return await _servicesService.GetServicesType();
        }
    }
}
