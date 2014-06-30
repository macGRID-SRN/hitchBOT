using System;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.Web;

namespace hitchbotAPI.Models
{
    public class Database : DbContext
    {

        public Database()
            : base("name=Database")
        {
            Configuration.AutoDetectChangesEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<SpeechEvent> SpeechEvents { get; set; }
        public DbSet<hitchBOT> hitchBOTs { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ListenEvent> ListenEvents { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<TwitterAccount> TwitterAccounts { get; set; }
        public DbSet<TwitterStatus> TwitterStatuses { get; set; }
        public DbSet<TwitterFriend> TwitterFriends { get; set; }
        public DbSet<TwitterLocationTarget> TwitterLocations { get; set; }
        public DbSet<CleverScriptAPIkey> CleverScriptAPIkeys { get; set; }
        public DbSet<GoogleMapsStatic> StaticMaps { get; set; }
        public DbSet<Password> Passwords { get; set; }

    }
}