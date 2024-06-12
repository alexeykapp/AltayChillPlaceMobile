using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AltayChillPlace.Interface
{
    public interface IMessageService
    {
        void ShowPopup(string title,string message);
        Task<bool> AskUserYesNo(string title, string message);
    }
}
