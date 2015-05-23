using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hitchbot_secure_api.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace hitchbot_secure_api.Dal
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("Database")
        {
            
        }

        //Dbsets go here.

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
