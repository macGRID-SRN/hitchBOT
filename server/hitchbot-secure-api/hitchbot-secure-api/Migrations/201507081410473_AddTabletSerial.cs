namespace hitchbot_secure_api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTabletSerial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TabletSerials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TabletSerialNumber = c.String(),
                        HitchBotId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HitchBots", t => t.HitchBotId, cascadeDelete: true)
                .Index(t => t.HitchBotId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TabletSerials", "HitchBotId", "dbo.HitchBots");
            DropIndex("dbo.TabletSerials", new[] { "HitchBotId" });
            DropTable("dbo.TabletSerials");
        }
    }
}
