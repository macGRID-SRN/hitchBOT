namespace hitchbotAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CleverScriptToHitchBOT : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CleverScriptAPIkeys", "HitchBOT_ID", c => c.Int());
            CreateIndex("dbo.CleverScriptAPIkeys", "HitchBOT_ID");
            AddForeignKey("dbo.CleverScriptAPIkeys", "HitchBOT_ID", "dbo.hitchBOTs", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CleverScriptAPIkeys", "HitchBOT_ID", "dbo.hitchBOTs");
            DropIndex("dbo.CleverScriptAPIkeys", new[] { "HitchBOT_ID" });
            DropColumn("dbo.CleverScriptAPIkeys", "HitchBOT_ID");
        }
    }
}
