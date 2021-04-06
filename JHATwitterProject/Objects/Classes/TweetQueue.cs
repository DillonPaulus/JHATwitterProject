using System.Collections.Concurrent;
using Tweetinvi.Events.V2;

namespace JHATwitterProject.Objects
{
    /// <summary>
    /// This class will set up hold a queue for processing twitter tweets
    /// </summary>
    /// <scope>public</scope>
    /// <type>class</type>
    public class TweetQueue : Interfaces.IQueue<TweetV2ReceivedEventArgs>
    {
        //the queue to hold
        private ConcurrentQueue<TweetV2ReceivedEventArgs> Queue { get; } = new ConcurrentQueue<TweetV2ReceivedEventArgs>();

        /// <summary>
        /// <see cref="Interfaces.IQueue{T}.Push(T)"/>
        /// </summary>
        public void Push(TweetV2ReceivedEventArgs tweet)
        {
            Queue.Enqueue(tweet);
        }

        /// <summary>
        /// <see cref="Interfaces.IQueue{T}.Pull(out T)"/>
        /// </summary>
        public bool Pull(out TweetV2ReceivedEventArgs Response)
        {
            return Queue.TryDequeue(out Response);
        }
    }
}
