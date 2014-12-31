namespace hitchbotAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWikipediaEntries : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WikipediaEntries",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        WikipediaText = c.String(),
                        RadiusKM = c.Double(),
                        TimeAdded = c.DateTime(nullable: false),
                        HitchBot_ID = c.Int(),
                        TargetLocation_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.hitchBOTs", t => t.HitchBot_ID)
                .ForeignKey("dbo.Locations", t => t.TargetLocation_ID)
                .Index(t => t.HitchBot_ID)
                .Index(t => t.TargetLocation_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WikipediaEntries", "TargetLocation_ID", "dbo.Locations");
            DropForeignKey("dbo.WikipediaEntries", "HitchBot_ID", "dbo.hitchBOTs");
            DropIndex("dbo.WikipediaEntries", new[] { "TargetLocation_ID" });
            DropIndex("dbo.WikipediaEntries", new[] { "HitchBot_ID" });
            DropTable("dbo.WikipediaEntries");
        }
    }
}
