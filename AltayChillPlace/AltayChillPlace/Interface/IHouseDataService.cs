﻿using AltayChillPlace.ApiResponses;
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
        Task<ObservableCollection<TypeHouse>> GetTypeHouseAsync();
    }
}
