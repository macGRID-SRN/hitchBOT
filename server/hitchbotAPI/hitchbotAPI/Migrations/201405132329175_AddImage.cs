namespace hitchbotAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImage : DbMigration
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
                        TimeAdded = c.DateTime(nullable: false),
                        location_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Locations", t => t.location_ID)
                .Index(t => t.location_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "location_ID", "dbo.Locations");
            DropIndex("dbo.Images", new[] { "location_ID" });
            DropTable("dbo.Images");
        }
    }
}
