namespace hitchbotAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNameToWikipediaEntry : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WikipediaEntries", "EntryName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WikipediaEntries", "EntryName");
        }
    }
}
