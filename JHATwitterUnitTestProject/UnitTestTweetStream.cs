using JHATwitterProject.Objects;
using JHATwitterProject.Objects.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace JHATwitterUnitTestProject
{
    [TestClass]
    public class UnitTestTweetStream
    {
        [TestMethod]
        public void TestStream()
        {
            IStream stream = new TweetStream(null);
            Assert.IsNotNull(stream);
            stream.StreamDisconnected += (_, s) => { };
            stream.ConsumeStream();
        }
    }
}
