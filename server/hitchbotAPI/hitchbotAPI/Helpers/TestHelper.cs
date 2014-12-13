using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace hitchbotAPI.Helpers
{
    /// <summary>
    /// So apparently trying to test the db from another project is a terrible idea.. so here is the test.
    /// </summary>
    public static class TestHelper
    {
        /// <summary>
        /// THIS TEST DOES NOT WORK YET.
        /// </summary>
        /// <returns></returns>
        public static bool TestDatabaseAvailable()
        {
            try
            {
                using (var db = new Models.Database())
                {
                    db.GetValidationErrors();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
