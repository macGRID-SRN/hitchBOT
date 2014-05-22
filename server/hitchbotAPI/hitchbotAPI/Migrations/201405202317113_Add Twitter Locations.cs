namespace hitchbotAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTwitterLocations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TwitterLocationTargets",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TweetText = c.String(nullable: false),
                        RadiusKM = c.Double(nullable: false),
                        TimeAdded = c.DateTime(nullable: false),
                        HitchBot_ID = c.Int(nullable: false),
                        Status_ID = c.Int(),
                        TargetLocation_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.hitchBOTs", t => t.HitchBot_ID)
                .ForeignKey("dbo.TwitterStatus", t => t.Status_ID)
                .ForeignKey("dbo.Locations", t => t.TargetLocation_ID)
                .Index(t => t.HitchBot_ID)
                .Index(t => t.Status_ID)
                .Index(t => t.TargetLocation_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TwitterLocationTargets", "TargetLocation_ID", "dbo.Locations");
            DropForeignKey("dbo.TwitterLocationTargets", "Status_ID", "dbo.TwitterStatus");
            DropForeignKey("dbo.TwitterLocationTargets", "HitchBot_ID", "dbo.hitchBOTs");
            DropIndex("dbo.TwitterLocationTargets", new[] { "TargetLocation_ID" });
            DropIndex("dbo.TwitterLocationTargets", new[] { "Status_ID" });
            DropIndex("dbo.TwitterLocationTargets", new[] { "HitchBot_ID" });
            DropTable("dbo.TwitterLocationTargets");
        }
    }
}
