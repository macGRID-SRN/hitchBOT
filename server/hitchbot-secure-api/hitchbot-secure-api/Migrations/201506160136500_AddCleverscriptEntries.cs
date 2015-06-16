namespace hitchbot_secure_api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCleverscriptEntries : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CleverscriptContents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LocationId = c.Int(),
                        CleverscriptContextId = c.Int(),
                        HitchBotId = c.Int(nullable: false),
                        CleverText = c.String(),
                        EntryName = c.String(),
                        RadiusKm = c.Double(),
                        TimeAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CleverscriptContexts", t => t.CleverscriptContextId)
                .ForeignKey("dbo.HitchBots", t => t.HitchBotId, cascadeDelete: true)
                .ForeignKey("dbo.Locations", t => t.LocationId)
                .Index(t => t.LocationId)
                .Index(t => t.CleverscriptContextId)
                .Index(t => t.HitchBotId);
            
            CreateTable(
                "dbo.CleverscriptContexts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BaseLabel = c.String(),
                        HumanReadableBaseLabel = c.String(),
                        HitchBotId = c.Int(),
                        TimeAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HitchBots", t => t.HitchBotId)
                .Index(t => t.HitchBotId);
            
            AddColumn("dbo.Locations", "CleverscriptContentId", c => c.Int());
            AddColumn("dbo.Locations", "CleverscriptContext_Id", c => c.Int());
            CreateIndex("dbo.Locations", "CleverscriptContext_Id");
            AddForeignKey("dbo.Locations", "CleverscriptContext_Id", "dbo.CleverscriptContexts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CleverscriptContents", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.Locations", "CleverscriptContext_Id", "dbo.CleverscriptContexts");
            DropForeignKey("dbo.CleverscriptContexts", "HitchBotId", "dbo.HitchBots");
            DropForeignKey("dbo.CleverscriptContents", "HitchBotId", "dbo.HitchBots");
            DropForeignKey("dbo.CleverscriptContents", "CleverscriptContextId", "dbo.CleverscriptContexts");
            DropIndex("dbo.Locations", new[] { "CleverscriptContext_Id" });
            DropIndex("dbo.CleverscriptContexts", new[] { "HitchBotId" });
            DropIndex("dbo.CleverscriptContents", new[] { "HitchBotId" });
            DropIndex("dbo.CleverscriptContents", new[] { "CleverscriptContextId" });
            DropIndex("dbo.CleverscriptContents", new[] { "LocationId" });
            DropColumn("dbo.Locations", "CleverscriptContext_Id");
            DropColumn("dbo.Locations", "CleverscriptContentId");
            DropTable("dbo.CleverscriptContexts");
            DropTable("dbo.CleverscriptContents");
        }
    }
}
