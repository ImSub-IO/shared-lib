
namespace ImSubShared.Logger
{
    public interface IMemoryLogQueue
    {
        /// <summary>
        /// Thread safe enqueueing of an element into the <see cref="MemoryLogQueue"/>
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="FullQueueException"></exception>
        void EnqueueElement(LogMessage message);

        /// <summary>
        /// Thread safe dequeuing of an element into the <see cref="MemoryLogQueue"/>
        /// </summary>
        /// <returns></returns>
        LogMessage? DequeueElement();
    }
}
