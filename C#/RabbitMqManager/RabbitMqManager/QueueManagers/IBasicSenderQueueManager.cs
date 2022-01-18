﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueManager.QueueManagers
{
    public interface IBasicSenderQueueManager<T> where T : class
    {
        void Send(T elem);
    }
}
