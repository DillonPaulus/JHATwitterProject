using JHATwitterProject.Objects;
using JHATwitterProject.Objects.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;

namespace JHATwitterUnitTestProject
{
    [TestClass]
    public class UnitTestTweetBusinessLayer
    {
        [TestMethod]
        public void TestBusinessLayer()
        {
            IBusinessLayer businessLayer = new TweetBusinessLayer();
            businessLayer.Run();
            DataTable dt = businessLayer.GetView();
            Assert.IsTrue(dt.Rows.Count > 0);
        }
    }
}
