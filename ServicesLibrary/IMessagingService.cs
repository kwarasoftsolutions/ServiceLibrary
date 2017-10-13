using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLibrary
{
    public interface IMessagingService
    {

        Task<bool> SendTextMessage(string from, string to, string subject, string message);
        Task<bool> SendMailMessage(string messengerName, string from, string to, string subject, string htmlMessage, string textMessage);
    }
}
