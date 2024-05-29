using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using AltayChillPlace.ApiResponses;
using AltayChillPlace.Interface;

namespace AltayChillPlace.Models
{
    public class HouseModel
    {
        private readonly IHouseDataService _houseDataService;
        public HouseModel(IHouseDataService houseDataService)
        {
            _houseDataService = houseDataService;
        }
        public async Task<ObservableCollection<HouseResponse>> GetAllHouses()
        {
            return await _houseDataService.GetAllHouseAsync();
        }
        public async Task<ObservableCollection<TypeHouse>> GetTypeHouses()
        {
            return await _houseDataService.GetTypeHouseAsync();
        }
    }
}
