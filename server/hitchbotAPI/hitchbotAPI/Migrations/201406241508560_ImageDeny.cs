namespace hitchbotAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageDeny : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CleverScriptAPIkeys", "HitchBOT_ID", c => c.Int());
            AddColumn("dbo.Images", "TimeApproved", c => c.DateTime(nullable: false));
            AddColumn("dbo.Images", "TimeDenied", c => c.DateTime(nullable: false));
            AddColumn("dbo.TwitterFriends", "TwitterAccount_ID", c => c.Int());
            CreateIndex("dbo.CleverScriptAPIkeys", "HitchBOT_ID");
            CreateIndex("dbo.TwitterFriends", "TwitterAccount_ID");
            AddForeignKey("dbo.CleverScriptAPIkeys", "HitchBOT_ID", "dbo.hitchBOTs", "ID");
            AddForeignKey("dbo.TwitterFriends", "TwitterAccount_ID", "dbo.TwitterAccounts", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TwitterFriends", "TwitterAccount_ID", "dbo.TwitterAccounts");
            DropForeignKey("dbo.CleverScriptAPIkeys", "HitchBOT_ID", "dbo.hitchBOTs");
            DropIndex("dbo.TwitterFriends", new[] { "TwitterAccount_ID" });
            DropIndex("dbo.CleverScriptAPIkeys", new[] { "HitchBOT_ID" });
            DropColumn("dbo.TwitterFriends", "TwitterAccount_ID");
            DropColumn("dbo.Images", "TimeDenied");
            DropColumn("dbo.Images", "TimeApproved");
            DropColumn("dbo.CleverScriptAPIkeys", "HitchBOT_ID");
        }
    }
}
