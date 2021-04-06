using System;
using System.Data;

namespace JHATwitterProject.Objects.Interfaces
{
    /// <summary>
    /// This interface will allow a client to process a <see cref="IQueue{T}"/> and receive a <see cref="DataTable"/> with the results
    /// </summary>
    /// <scope>public</scope>
    /// <type>interface</type>
    public interface IDetail
    {
        /// <summary>
        /// The fired event if the queue stops
        /// </summary>
        event EventHandler QueueStopped;

        /// <summary>
        /// This will start the queue processing
        /// </summary>
        void ProcessQueue();

        /// <summary>
        /// This will get a <see cref="DataTable"/> on the stream analysis
        /// </summary>
        /// <returns><see cref="DataTable"/></returns>
        DataTable GetViewTable();
    }
}
