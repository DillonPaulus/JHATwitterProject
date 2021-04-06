using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using JHATwitterProject.Objects.Interfaces;
using JHATwitterProject.Objects;
using Tweetinvi.Events.V2;

namespace JHATwitterUnitTestProject
{
    [TestClass]
    public class UnitTestTweenQueue
    {
        [TestMethod]
        public void TestQueue()
        {
            IQueue<TweetV2ReceivedEventArgs> stream = new TweetQueue();
            stream.Push(new TweetV2ReceivedEventArgs(new Tweetinvi.Models.V2.TweetV2Response(), "test"));
            Assert.IsTrue(stream.Pull(out TweetV2ReceivedEventArgs temp));
            Assert.IsNotNull(temp);
        }
    }
}
