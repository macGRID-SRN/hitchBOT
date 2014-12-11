using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using hitchbotAPI;
using System.Diagnostics;
using System.Linq;

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
            //so the last way didn't work.. neither does this.. THIS TEST DOES NOT WORK YET.
            Assert.IsTrue(hitchbotAPI.Helpers.TestHelper.TestDatabaseAvailable(), "Database is available.");
            //Assert.Fail("Something went wrong when accessing the DB, either the connection string is wrong, the model has changed or it is currently unavailable.");
        }
    }
}
