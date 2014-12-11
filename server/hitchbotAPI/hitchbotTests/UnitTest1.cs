using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using hitchbotAPI;
using System.Diagnostics;

namespace hitchbotTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

        }

        [TestMethod]
        public void TestDbIsAvailable()
        {
            try
            {
                var db = new hitchbotAPI.Models.Database();
                Debug.WriteLine("The Database is mostly probably working.");
                return;
            }
            catch (Exception e)
            {
                Assert.Fail("Something went wrong when accessing the DB, either the connection string is wrong, the model has changed or it is currently unavailable. " + e.StackTrace);
            }
        }
    }
}
