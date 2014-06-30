namespace hitchbotAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageToHitchBOT : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Passwords", "hitchBOT_ID", c => c.Int());
            CreateIndex("dbo.Passwords", "hitchBOT_ID");
            AddForeignKey("dbo.Passwords", "hitchBOT_ID", "dbo.hitchBOTs", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Passwords", "hitchBOT_ID", "dbo.hitchBOTs");
            DropIndex("dbo.Passwords", new[] { "hitchBOT_ID" });
            DropColumn("dbo.Passwords", "hitchBOT_ID");
        }
    }
}
