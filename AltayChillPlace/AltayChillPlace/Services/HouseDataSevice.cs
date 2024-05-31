using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using AltayChillPlace.ApiResponses;
using AltayChillPlace.Interface;
using AltayChillPlace.RestClient;

namespace AltayChillPlace.Services
{
    public class HouseDataSevice : IHouseDataService
    {
        private readonly HousesDataClient _housesDataClient;
        public HouseDataSevice(HousesDataClient housesDataClient)
        {
            _housesDataClient = housesDataClient;
        }

        public async Task<ObservableCollection<HouseResponse>> GetAllHouseAsync()
        {
            try
            {
                ObservableCollection<HouseResponse> listHouses = await _housesDataClient.GetDataAsync("houses/all");
                return listHouses;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw new Exception("Error in the request to get a list of houses: " + ex.Message);
            }
        }
        public async Task<ObservableCollection<TypeHouse>> GetTypeHouseAsync()
        {
            try
            {
                ObservableCollection<TypeHouse> listTypes = await _housesDataClient.GetAllTypeAsync();
                return listTypes;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw new Exception("Error in the request to get a list of types: " + ex.Message);
            }
        }
        public async Task<ObservableCollection<HouseResponse>> GetAvailableHouseAsync(DateTime arrivalDate, DateTime departureDate)
        {
            try
            {
                ObservableCollection<HouseResponse> listTypes = await _housesDataClient.GetAvailableHouseAsync(arrivalDate.ToString(), departureDate.ToString());
                return listTypes;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw new Exception("Error in the request to get a list of types: " + ex.Message);
            }
        }
    }
}
