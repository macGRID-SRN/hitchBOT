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
        public DbSet<ContextPacket> ContextPackets { get; set; }
        public DbSet<VariableValuePair> VariableValuePairs { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<LoginAccount> LoginAccounts { get; set; }
        public DbSet<CleverscriptContext> CleverscriptContexts { get; set; }
        public DbSet<CleverscriptContent> CleverscriptContents { get; set; }
        public DbSet<PolgonVertex> PolgonVertices { get; set; }
        public DbSet<TabletSerial> TabletSerials { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
