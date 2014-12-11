using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using hitchbotAPI;

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
                Assert.Inconclusive("The Database is mostly probably working.");
            }
            catch
            {
                Assert.Fail("Something went wrong when accessing the DB, either the connection string is wrong, the model has changed or it is currently unavailable.");
            }
        }
    }
}
