namespace hitchbotAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddConversationLink : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Conversations", new[] { "hitchBOT_ID" });
            CreateIndex("dbo.Conversations", "HitchBOT_ID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Conversations", new[] { "HitchBOT_ID" });
            CreateIndex("dbo.Conversations", "hitchBOT_ID");
        }
    }
}
