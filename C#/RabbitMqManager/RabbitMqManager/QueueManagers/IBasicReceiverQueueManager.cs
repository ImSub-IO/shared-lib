using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueManager.QueueManagers
{
    internal interface IBasicReceiverQueueManager<T> where T : class
    {
        void Consume();
    }
}
