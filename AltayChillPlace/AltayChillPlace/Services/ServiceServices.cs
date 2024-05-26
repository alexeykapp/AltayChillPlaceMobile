using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AltayChillPlace.ApiResponses;
using AltayChillPlace.Interface;
using AltayChillPlace.RestClient;

namespace AltayChillPlace.Services
{
    class ServiceServices : IServicesService
    {
        private readonly ServiceClient _serviceClient;
        public ServiceServices(ServiceClient serviceClient)
        {
            _serviceClient = serviceClient;
        }

        public async Task<ObservableCollection<AdditionalServiceResponse>> GetAllServies()
        {
            var services = await _serviceClient.GetAllServices();
            return services;
        }
    }
}
