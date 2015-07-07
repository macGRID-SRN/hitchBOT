namespace hitchbot_secure_api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSpotMessageType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Locations", "SpotGpsMessageType", c => c.Int());
            AddColumn("dbo.Locations", "HideFromProduction", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Locations", "HideFromProduction");
            DropColumn("dbo.Locations", "SpotGpsMessageType");
        }
    }
}
