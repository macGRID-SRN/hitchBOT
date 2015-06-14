namespace hitchbot_secure_api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserAccount : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LoginAccounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Description = c.String(),
                        PasswordHash = c.String(),
                        Salt = c.String(),
                        HitchBotId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HitchBots", t => t.HitchBotId, cascadeDelete: true)
                .Index(t => t.HitchBotId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LoginAccounts", "HitchBotId", "dbo.HitchBots");
            DropIndex("dbo.LoginAccounts", new[] { "HitchBotId" });
            DropTable("dbo.LoginAccounts");
        }
    }
}
