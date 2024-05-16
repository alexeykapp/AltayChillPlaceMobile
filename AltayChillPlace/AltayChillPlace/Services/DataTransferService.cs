using AltayChillPlace.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace AltayChillPlace.Services
{
    public class DataTransferService : IDataTransferService
    {
        private readonly Dictionary<string, object> _dataStore = new Dictionary<string, object>();

        public void SetData(string key, object data)
        {
            if (_dataStore.ContainsKey(key))
                _dataStore[key] = data;
            else
                _dataStore.Add(key, data);
        }

        public object GetData(string key)
        {
            return _dataStore.TryGetValue(key, out var data) ? data : null;
        }
    }
}
