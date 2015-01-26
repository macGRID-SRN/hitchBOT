namespace hitchbotAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMapMarker : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MapMarkers", "HasBeenVisited_ID", "dbo.Locations");
            DropIndex("dbo.MapMarkers", new[] { "HasBeenVisited_ID" });
            AddColumn("dbo.MapMarkers", "Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.MapMarkers", "RadiusKM", c => c.Double(nullable: false));
            AddColumn("dbo.MapMarkers", "HasBeenVisited", c => c.Boolean(nullable: false));
            AddColumn("dbo.MapMarkers", "HeaderTextGerman", c => c.String());
            AddColumn("dbo.MapMarkers", "BodyTextGerman", c => c.String());
            DropColumn("dbo.MapMarkers", "HasBeenVisited_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MapMarkers", "HasBeenVisited_ID", c => c.Int());
            DropColumn("dbo.MapMarkers", "BodyTextGerman");
            DropColumn("dbo.MapMarkers", "HeaderTextGerman");
            DropColumn("dbo.MapMarkers", "HasBeenVisited");
            DropColumn("dbo.MapMarkers", "RadiusKM");
            DropColumn("dbo.MapMarkers", "Active");
            CreateIndex("dbo.MapMarkers", "HasBeenVisited_ID");
            AddForeignKey("dbo.MapMarkers", "HasBeenVisited_ID", "dbo.Locations", "ID");
        }
    }
}
