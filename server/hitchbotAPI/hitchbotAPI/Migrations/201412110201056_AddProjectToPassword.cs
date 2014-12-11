namespace hitchbotAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProjectToPassword : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "Password_ID", c => c.Int());
            CreateIndex("dbo.Projects", "Password_ID");
            AddForeignKey("dbo.Projects", "Password_ID", "dbo.Passwords", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "Password_ID", "dbo.Passwords");
            DropIndex("dbo.Projects", new[] { "Password_ID" });
            DropColumn("dbo.Projects", "Password_ID");
        }
    }
}
