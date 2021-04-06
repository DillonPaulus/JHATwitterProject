using System;
using System.Data;
using Tweetinvi.Events.V2;

namespace JHATwitterProject.Objects
{
    /// <summary>
    /// This class will handle creating and processing all needed items for a twitter stream
    /// </summary>
    /// <scope>public</scope>
    /// <type>class</type>
    /// <inherits><see cref="Interfaces.IBusinessLayer"/></inherits>
    public class TweetBusinessLayer : Interfaces.IBusinessLayer
    {

        //These three objects are used to read from twitter and to fill and process a queue
        internal Interfaces.IStream Stream { get; set; }
        internal Interfaces.IDetail Tweets { get; set; }
        internal Interfaces.IQueue<TweetV2ReceivedEventArgs> Queue { get; set; }

        /// <summary>
        /// This constructor will set the private variables
        /// </summary>
        public TweetBusinessLayer()
        {
            Queue = new TweetQueue();
            Stream = new TweetStream(Queue);
            Tweets = new TweetDetail(Queue);
        }

        /// <summary>
        /// <see cref="Interfaces.IDetail"/>
        /// </summary>
        public DataTable GetView()
        {
            return Tweets.GetViewTable();
        }

        /// <summary>
        /// This will start the processing job and ensure that the threads are managed
        /// </summary>
        public void Run()
        {
            Tweets.QueueStopped += TriggerQueue;
            Stream.StreamDisconnected += TriggerStream;
            TriggerStream(null, null);
            TriggerQueue(null, null);
        }

        //This will restart the stream in the event of error
        private void TriggerStream(object sender, EventArgs e)
        {
            Stream.ConsumeStream();
        }

        //This will restart the queue in the event of error
        private void TriggerQueue(object sender, EventArgs e)
        {
            Tweets.ProcessQueue();
        }
    }
}
