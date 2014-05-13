namespace hitchbotAPI.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<hitchbotAPI.Models.Database>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            //this is probably really dangerous, it should not be used unless you know what you are doing.
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Models.Database context)
        {
            
        }
    }
}
