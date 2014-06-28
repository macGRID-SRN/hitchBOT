namespace hitchbotAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveImage : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Images", "HitchBOT_ID", "dbo.hitchBOTs");
            DropForeignKey("dbo.Images", "Location_ID", "dbo.Locations");
            DropIndex("dbo.Images", new[] { "HitchBOT_ID" });
            DropIndex("dbo.Images", new[] { "Location_ID" });
            DropTable("dbo.Images");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.ID);
            
            CreateIndex("dbo.Images", "Location_ID");
            CreateIndex("dbo.Images", "HitchBOT_ID");
            AddForeignKey("dbo.Images", "Location_ID", "dbo.Locations", "ID");
            AddForeignKey("dbo.Images", "HitchBOT_ID", "dbo.hitchBOTs", "ID");
        }
    }
}
