using AltayChillPlace.ApiResponses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace AltayChillPlace.Interface
{
    public interface IHouseDataService
    {
        Task<ObservableCollection<HouseResponse>> GetAllHouseAsync();
        Task<ObservableCollection<HouseResponse>> GetAvailableHouseAsync(DateTime arrivalDate, DateTime departureDate);
        Task<ObservableCollection<TypeHouse>> GetTypeHouseAsync();
        Task<HouseResponse> GetHouseByIdAsync(int idHouse);
        Task<PhotosHouseResponse> GetPhotoHouseByIdAsync(int idHouse);
    }
}
