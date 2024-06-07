using AltayChillPlace.ApiResponses;
using AltayChillPlace.ApiResponses.Admin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace AltayChillPlace.Interface
{
    public interface IAdminService
    {
        Task<ObservableCollection<ReservationResponse>> GetAllReservationAsync();
        Task<ObservableCollection<ApplicationStatusResponse>> GetAllApplicationSatus();
    }
}
