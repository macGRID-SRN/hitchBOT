namespace hitchbotAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHitsAndExceptions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExceptionLogs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        Exception = c.String(),
                        Arguments = c.String(),
                        Method = c.String(),
                        TimeOccured = c.DateTime(nullable: false),
                        Data = c.String(),
                        Action = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.GoogleMapsStatics", "ViewCount", c => c.Int(nullable: false));
            AddColumn("dbo.GoogleMapsStatics", "NearestCity", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GoogleMapsStatics", "NearestCity");
            DropColumn("dbo.GoogleMapsStatics", "ViewCount");
            DropTable("dbo.ExceptionLogs");
        }
    }
}
