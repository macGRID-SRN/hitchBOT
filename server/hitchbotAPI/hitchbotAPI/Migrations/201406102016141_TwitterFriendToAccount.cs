namespace hitchbotAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TwitterFriendToAccount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TwitterFriends", "TwitterAccount_ID", c => c.Int());
            CreateIndex("dbo.TwitterFriends", "TwitterAccount_ID");
            AddForeignKey("dbo.TwitterFriends", "TwitterAccount_ID", "dbo.TwitterAccounts", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TwitterFriends", "TwitterAccount_ID", "dbo.TwitterAccounts");
            DropIndex("dbo.TwitterFriends", new[] { "TwitterAccount_ID" });
            DropColumn("dbo.TwitterFriends", "TwitterAccount_ID");
        }
    }
}
