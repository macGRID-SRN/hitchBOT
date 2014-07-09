namespace hitchbotAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMapHitCounterAndCity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GoogleMapsStatics", "ViewCount", c => c.Int(nullable: false));
            AddColumn("dbo.GoogleMapsStatics", "NearestCity", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GoogleMapsStatics", "NearestCity");
            DropColumn("dbo.GoogleMapsStatics", "ViewCount");
        }
    }
}
