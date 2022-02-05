using ImSubShared.Logger.Configuration;
using ImSubShared.Logger.Exceptions;
using System;
using System.Collections.Generic;

namespace ImSubShared.Logger
{
    /// <summary>
    /// Class for a concurrent <see cref="Queue{T}"/> wrapper
    /// </summary>
    internal class MemoryLogQueue
    {
        private static MemoryLogQueue _instance;

        private readonly object _lock = new object();
        private readonly Queue<LogMessage> _queue;
        private readonly MemoryQueueConfiguration _conf;

        /// <summary>
        /// Creates an new instance of <see cref="MemoryLogQueue"/>
        /// </summary>
        /// <param name="conf"></param>
        private MemoryLogQueue(MemoryQueueConfiguration conf)
        {
            _conf = conf ?? throw new ArgumentNullException(nameof(conf));
            
            if (!_conf.IsValid())
                throw new InvalidMemoryLogQueueConfigurationException();

            _queue = new Queue<LogMessage>();
        }

        /// <summary>
        /// Creates a new singleton instance of <see cref="MemoryLogQueue"/>
        /// </summary>
        /// <param name="conf"></param>
        /// <returns></returns>
        public static MemoryLogQueue GetInstance(MemoryQueueConfiguration conf)
        {
            if (_instance != null)
                _instance = new MemoryLogQueue(conf);

            return _instance;
        }

        /// <summary>
        /// Thread safe enqueueing of an element into the <see cref="MemoryLogQueue"/>
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="FullQueueException"></exception>
        public void EnqueueElement(LogMessage message)
        {
            bool enqueued = false;
            lock (_lock)
            {
                if (_queue.Count < _conf.QueueSizeLimit)
                {
                    _queue.Enqueue(message);
                    enqueued = true;
                }   
            }

            if (!enqueued)
                throw new FullQueueException();
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
