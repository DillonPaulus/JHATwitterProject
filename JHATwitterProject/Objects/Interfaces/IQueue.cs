namespace JHATwitterProject.Objects.Interfaces
{
    /// <summary>
    /// This interface provides the mechanisms to interact with queues
    /// </summary>
    /// <typeparam name="T">Any type of queue that needs to be created</typeparam>
    public interface IQueue<T>
    {
        /// <summary>
        /// This will allow you to push an item onto a queue
        /// </summary>
        /// <param name="obj">An item to push onto the queue</param>
        void Push(T obj);

        /// <summary>
        /// This will allow you to pull an item off the queue.
        /// </summary>
        /// <param name="obj">An item to pull off, can be null</param>
        /// <returns>True if an item was pulled, false otherwise</returns>
        bool Pull(out T obj);
    }
}
