namespace hitchbot_secure_api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBucketList : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CleverscriptContents", "isBucketList", c => c.Boolean(nullable: false));
            AddColumn("dbo.CleverscriptContents", "TimeVisited", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CleverscriptContents", "TimeVisited");
            DropColumn("dbo.CleverscriptContents", "isBucketList");
        }
    }
}
