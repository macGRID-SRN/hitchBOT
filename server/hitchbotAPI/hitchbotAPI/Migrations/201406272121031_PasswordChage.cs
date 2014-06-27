namespace hitchbotAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PasswordChage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "HitchBOT_ID", c => c.Int());
            CreateIndex("dbo.Images", "HitchBOT_ID");
            AddForeignKey("dbo.Images", "HitchBOT_ID", "dbo.hitchBOTs", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "HitchBOT_ID", "dbo.hitchBOTs");
            DropIndex("dbo.Images", new[] { "HitchBOT_ID" });
            DropColumn("dbo.Images", "HitchBOT_ID");
        }
    }
}
