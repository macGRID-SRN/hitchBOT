namespace hitchbotAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GoogleMapsStatic : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Images", new[] { "location_ID" });
            CreateTable(
                "dbo.GoogleMapsStatics",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        URL = c.String(),
                        TimeGenerated = c.DateTime(nullable: false),
                        TimeAdded = c.DateTime(nullable: false),
                        HitchBot_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.hitchBOTs", t => t.HitchBot_ID)
                .Index(t => t.HitchBot_ID);
            
            CreateIndex("dbo.Images", "Location_ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GoogleMapsStatics", "HitchBot_ID", "dbo.hitchBOTs");
            DropIndex("dbo.GoogleMapsStatics", new[] { "HitchBot_ID" });
            DropIndex("dbo.Images", new[] { "Location_ID" });
            DropTable("dbo.GoogleMapsStatics");
            CreateIndex("dbo.Images", "location_ID");
        }
    }
}
