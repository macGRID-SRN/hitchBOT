namespace hitchbotAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSpeechLogEvent : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SpeechLogEvents",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        HitchBOTID = c.Int(nullable: false),
                        SpeechSaid = c.String(),
                        SpeechHeard = c.String(),
                        Person = c.String(),
                        Notes = c.String(),
                        MatchedLineLabel = c.String(),
                        MatchAccuracy = c.Int(),
                        EnvironmentType = c.Int(),
                        RmsDecibalLevel = c.Double(),
                        TimeOccured = c.DateTime(nullable: false),
                        TimeAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.hitchBOTs", t => t.HitchBOTID, cascadeDelete: true)
                .Index(t => t.HitchBOTID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SpeechLogEvents", "HitchBOTID", "dbo.hitchBOTs");
            DropIndex("dbo.SpeechLogEvents", new[] { "HitchBOTID" });
            DropTable("dbo.SpeechLogEvents");
        }
    }
}
