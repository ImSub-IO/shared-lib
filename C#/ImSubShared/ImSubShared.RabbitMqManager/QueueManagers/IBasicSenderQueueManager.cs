using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImSubShared.RabbitMqManagers.QueueManager.QueueManagers
{
    public interface IBasicSenderQueueManager<T> where T : class
    {
        void Close(bool onlyChannel);
        void Send(T elem);
    }
}
