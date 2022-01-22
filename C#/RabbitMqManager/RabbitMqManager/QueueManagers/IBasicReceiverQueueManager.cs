using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueManager.QueueManagers
{
    public interface IBasicReceiverQueueManager<T> where T : class
    {
        void CheckState();
        void Consume();
        void Close(bool onlyChannel);
    }
}
