using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using AltayChillPlace.ApiResponses;
using AltayChillPlace.Helpers;
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
            var houses = await _houseDataService.GetAllHouseAsync();
            //ConvertByteToImage(houses);
            return houses;
        }
        public async Task<ObservableCollection<TypeHouse>> GetTypeHouses()
        {
            return await _houseDataService.GetTypeHouseAsync();
        }
        public async Task<ObservableCollection<HouseResponse>> GetAvailableHouse(DateTime arrivalDate, DateTime departureDate)
        {
            return await _houseDataService.GetAvailableHouseAsync(arrivalDate, departureDate);
        }
        private void ConvertByteToImage(ObservableCollection<HouseResponse> houses)
        {
            foreach (var house in houses)
            {
                house.PhotoHouses = ImageConverter.ConvertBase64StringToImage(house.PhotoOfTheRoom);
            }
        }
    }
}
