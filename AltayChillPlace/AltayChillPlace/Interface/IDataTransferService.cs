using System;
using System.Collections.Generic;
using System.Text;

namespace AltayChillPlace.Interface
{
    public interface IDataTransferService
    {
        void SetData(string key, object data);
        object GetData(string key);
    }
}
