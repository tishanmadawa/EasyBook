using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WatiN.Core;
namespace HotelBook
{
    [TestClass]
    public class unitTesting
    {
        Browser browserInstance;

        [TestInitialize]
        public void WithAnInstanceOfTheBrowserSearchingGoogle()
        {
            browserInstance = new FireFox(@"http://www.google.com");
            TextField criteria =
              browserInstance.TextField(tf => tf.Name == "q");
            criteria.Value = "Come Jam With Us";
            Button search =
              browserInstance.Button(btn => btn.IdOrName == "btnG");
            search.Click();
        }

        [TestCleanup]
        public void ShutdownBrowserWhenDone()
        {
            browserInstance.Close();
            browserInstance = null;
        }
    }
}