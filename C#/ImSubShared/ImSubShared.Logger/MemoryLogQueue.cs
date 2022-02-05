using ImSubShared.Logger.Configuration;
using ImSubShared.Logger.Exceptions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace ImSubShared.Logger
{
    /// <summary>
    /// Class for a concurrent <see cref="Queue{T}"/> wrapper. register it as Singleton in servies DI
    /// </summary>
    public class MemoryLogQueue : IMemoryLogQueue
    {
        private readonly object _lock = new object();
        private readonly Queue<LogMessage> _queue;
        private readonly MemoryQueueConfiguration _conf;

        /// <summary>
        /// Creates an new instance of <see cref="MemoryLogQueue"/>
        /// </summary>
        /// <param name="conf"></param>
        public MemoryLogQueue(IOptions<MemoryQueueConfiguration> conf)
        {
            if (conf == null)
                throw new ArgumentNullException(nameof(conf));
            _conf = conf.Value;
            if (!_conf.IsValid())
                throw new InvalidMemoryLogQueueConfigurationException();

            _queue = new Queue<LogMessage>();
        }

        /// <summary>
        /// Thread safe enqueueing of an element into the <see cref="MemoryLogQueue"/>
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="FullQueueException"></exception>
        public void EnqueueElement(LogMessage message)
        {
            lock (_lock)
            {
                if (_queue.Count < _conf.QueueSizeLimit)
                    _queue.Enqueue(message);
                else
                    throw new FullQueueException();
            }
        }

        /// <summary>
        /// Thread safe dequeuing of an element into the <see cref="MemoryLogQueue"/>
        /// </summary>
        /// <returns></returns>
        public LogMessage? DequeueElement()
        {
            LogMessage? message = null;
            lock (_lock)
            { 
                if(_queue.Count > 0)
                    message = _queue.Dequeue();
            }
            return message;
        }
    }
}
