namespace hitchbotAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TabletStatus : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TabletStatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TimeTaken = c.DateTime(nullable: false),
                        TimeAdded = c.DateTime(nullable: false),
                        BatteryTemp = c.Double(nullable: false),
                        BatteryVoltage = c.Double(nullable: false),
                        IsCharging = c.Boolean(nullable: false),
                        BatteryPercentage = c.Double(nullable: false),
                        HitchBOT_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.hitchBOTs", t => t.HitchBOT_ID)
                .Index(t => t.HitchBOT_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TabletStatus", "HitchBOT_ID", "dbo.hitchBOTs");
            DropIndex("dbo.TabletStatus", new[] { "HitchBOT_ID" });
            DropTable("dbo.TabletStatus");
        }
    }
}
