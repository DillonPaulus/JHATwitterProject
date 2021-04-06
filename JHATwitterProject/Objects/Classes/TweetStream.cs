using System;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Events.V2;
using Tweetinvi.Models;
using Tweetinvi.Streaming.V2;

namespace JHATwitterProject.Objects
{
    /// <summary>
    /// This class will handle consuming the Tweet stream. It will keep the stream open and will push all tweets to a global queue
    /// </summary>
    /// <scope>public</scope>
    /// <tyep>class</tyep>
    public class TweetStream : Interfaces.IStream
    {
        /// <summary>
        /// This event handler will fire anytime the stream is disconnected
        /// </summary>
        /// <scope>public</scope>
        /// <type>event</type>
        /// <returns><see cref="EventHandler"/></returns>
        public event EventHandler StreamDisconnected;

        /// <summary>
        /// This is the queue that the Tweets will be pushed into.
        /// </summary>
        /// <scope>public static</scope>
        /// <type><see cref="ConcurrentQueue"/> and <see cref="TweetV2ReceivedEventArgs"/></type>

        //This will hold a variable outside of the scope that the stream is running on. It will be responsible for firing the event due to disconnection
        private bool StreamAlive { get { return Alive; } set { Alive = value; if (!StreamAlive) StreamDisconnected(this, EventArgs.Empty); } }
        private bool Alive { get; set; }

        private Interfaces.IQueue<TweetV2ReceivedEventArgs> Queue { get; set; }

        public TweetStream(Interfaces.IQueue<TweetV2ReceivedEventArgs> queue)
        {
            Queue = queue;
        }

        /// <summary>
        /// This method will start the stream and begin to fill the queue
        /// </summary>
        public void ConsumeStream()
        {
            new Thread(async () => await GetTweets().ConfigureAwait(false)).Start();
        }

        //This method will set the user token information and will begin to sip the stream of tweets
        private async Task GetTweets()
        {
            StreamAlive = true;
            try
            {
                TwitterClient appClient = new TwitterClient(new ConsumerOnlyCredentials(ConfigurationManager.AppSettings["TwitterPublicToken"], ConfigurationManager.AppSettings["TwitterSecretToken"])
                {
                    BearerToken = ConfigurationManager.AppSettings["TwitterBearToken"]
                });
                ISampleStreamV2 sampleStream = appClient.StreamsV2.CreateSampleStream();
                sampleStream.TweetReceived += (sender, args) =>
                {
                    Queue.Push(args);
                };

                await sampleStream.StartAsync();
            }
            catch
            {
                StreamAlive = false;
            }
        }
    }
}
