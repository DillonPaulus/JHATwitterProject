using JHATwitterProject.Objects;
using JHATwitterProject.Objects.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;

namespace JHATwitterUnitTestProject
{
    [TestClass]
    public class UnitTestTweetDetail
    {
        [TestMethod]
        public void TestDetails ()
        {
            IDetail detail = new TweetDetail(null);
            DataTable dt = detail.GetViewTable();
            Assert.IsTrue(dt.Rows.Count > 0);
            Assert.IsNotNull(detail);
            detail.QueueStopped += (_, s) => { };
            detail.ProcessQueue();

        }
    }
}
