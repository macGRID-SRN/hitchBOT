namespace hitchbotAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.hitchBOTs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        Altitude = c.Double(nullable: false),
                        Accuracy = c.Single(nullable: false),
                        Velocity = c.Single(nullable: false),
                        TakenTime = c.DateTime(nullable: false),
                        hitchBOT_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.hitchBOTs", t => t.hitchBOT_ID)
                .Index(t => t.hitchBOT_ID);
            
            CreateTable(
                "dbo.SpeechEvents",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SpeechSaid = c.String(),
                        OccuredTime = c.DateTime(nullable: false),
                        hitchBOT_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.hitchBOTs", t => t.hitchBOT_ID)
                .Index(t => t.hitchBOT_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SpeechEvents", "hitchBOT_ID", "dbo.hitchBOTs");
            DropForeignKey("dbo.Locations", "hitchBOT_ID", "dbo.hitchBOTs");
            DropIndex("dbo.SpeechEvents", new[] { "hitchBOT_ID" });
            DropIndex("dbo.Locations", new[] { "hitchBOT_ID" });
            DropTable("dbo.SpeechEvents");
            DropTable("dbo.Locations");
            DropTable("dbo.hitchBOTs");
        }
    }
}
