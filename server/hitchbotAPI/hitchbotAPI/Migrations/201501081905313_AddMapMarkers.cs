namespace hitchbotAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMapMarkers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MapMarkers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        HeaderText = c.String(),
                        BodyText = c.String(),
                        TimeAdded = c.DateTime(nullable: false),
                        HasBeenVisited_ID = c.Int(),
                        Project_ID = c.Int(),
                        TargetLocation_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Locations", t => t.HasBeenVisited_ID)
                .ForeignKey("dbo.Projects", t => t.Project_ID)
                .ForeignKey("dbo.Locations", t => t.TargetLocation_ID)
                .Index(t => t.HasBeenVisited_ID)
                .Index(t => t.Project_ID)
                .Index(t => t.TargetLocation_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MapMarkers", "TargetLocation_ID", "dbo.Locations");
            DropForeignKey("dbo.MapMarkers", "Project_ID", "dbo.Projects");
            DropForeignKey("dbo.MapMarkers", "HasBeenVisited_ID", "dbo.Locations");
            DropIndex("dbo.MapMarkers", new[] { "TargetLocation_ID" });
            DropIndex("dbo.MapMarkers", new[] { "Project_ID" });
            DropIndex("dbo.MapMarkers", new[] { "HasBeenVisited_ID" });
            DropTable("dbo.MapMarkers");
        }
    }
}
