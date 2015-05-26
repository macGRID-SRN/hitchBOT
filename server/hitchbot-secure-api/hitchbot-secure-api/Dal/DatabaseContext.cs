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
        public DatabaseContext()
            : base("name=Database")
        {

        }

        public DbSet<Journey> Journeys { get; set; }
        public DbSet<HitchBot> HitchBots { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<SpeechLogEvent> SpeechLogEvents { get; set; }
        public DbSet<TabletStatus> TabletStatuses { get; set; }
        public DbSet<ExceptionLog> ExceptionLogs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
