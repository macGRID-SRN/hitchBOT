namespace hitchbot_secure_api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPastCleverText : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CleverscriptContents", "VisitedCleverText", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CleverscriptContents", "VisitedCleverText");
        }
    }
}
