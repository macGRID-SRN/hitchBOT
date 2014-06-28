namespace hitchbotAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageFix : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        url = c.String(),
                        TimeTaken = c.DateTime(nullable: false),
                        TimeApproved = c.DateTime(),
                        TimeDenied = c.DateTime(),
                        TimeAdded = c.DateTime(nullable: false),
                        HitchBOT_ID = c.Int(),
                        Location_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.hitchBOTs", t => t.HitchBOT_ID)
                .ForeignKey("dbo.Locations", t => t.Location_ID)
                .Index(t => t.HitchBOT_ID)
                .Index(t => t.Location_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "Location_ID", "dbo.Locations");
            DropForeignKey("dbo.Images", "HitchBOT_ID", "dbo.hitchBOTs");
            DropIndex("dbo.Images", new[] { "Location_ID" });
            DropIndex("dbo.Images", new[] { "HitchBOT_ID" });
            DropTable("dbo.Images");
        }
    }
}
