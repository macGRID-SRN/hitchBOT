namespace hitchbot_secure_api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExceptionLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        Exception = c.String(),
                        Arguments = c.String(),
                        Method = c.String(),
                        TimeOccured = c.DateTime(nullable: false),
                        TimeAdded = c.DateTime(nullable: false),
                        Data = c.String(),
                        Action = c.String(),
                        HitchBotId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HitchBots", t => t.HitchBotId, cascadeDelete: true)
                .Index(t => t.HitchBotId);
            
            CreateTable(
                "dbo.HitchBots",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        JourneyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Journeys", t => t.JourneyId, cascadeDelete: true)
                .Index(t => t.JourneyId);
            
            CreateTable(
                "dbo.Journeys",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(),
                        TimeCreated = c.DateTime(nullable: false),
                        EndLocation_Id = c.Int(),
                        StartLocation_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locations", t => t.EndLocation_Id)
                .ForeignKey("dbo.Locations", t => t.StartLocation_Id)
                .Index(t => t.EndLocation_Id)
                .Index(t => t.StartLocation_Id);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Latitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Longitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Altitude = c.Decimal(precision: 18, scale: 2),
                        Accuracy = c.Decimal(precision: 18, scale: 2),
                        Velocity = c.Decimal(precision: 18, scale: 2),
                        LocationProvider = c.Int(nullable: false),
                        NearestCity = c.String(),
                        ForceProduction = c.Boolean(nullable: false),
                        HitchBotId = c.Int(),
                        TakenTime = c.DateTime(),
                        TimeAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HitchBots", t => t.HitchBotId)
                .Index(t => t.HitchBotId);
            
            CreateTable(
                "dbo.SpeechLogEvents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HitchBotId = c.Int(nullable: false),
                        Speech_Said = c.String(),
                        Speech_Heard = c.String(),
                        Speech_Person = c.String(),
                        Speech_Notes = c.String(),
                        Speech_MatchedLineLabel = c.String(),
                        MatchAccuracy = c.Int(),
                        EnvironmentType = c.Int(),
                        RecognitionScore = c.Int(),
                        GoogleRecognitionScore = c.Int(),
                        ResponseScore = c.Int(),
                        RecognizerType = c.Int(),
                        RmsDecibalLevel = c.Double(),
                        TimeOccured = c.DateTime(nullable: false),
                        TimeAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HitchBots", t => t.HitchBotId, cascadeDelete: true)
                .Index(t => t.HitchBotId);
            
            CreateTable(
                "dbo.TabletStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HitchBotId = c.Int(nullable: false),
                        TimeTaken = c.DateTime(nullable: false),
                        TimeAdded = c.DateTime(nullable: false),
                        BatteryTemp = c.Double(nullable: false),
                        BatteryVoltage = c.Double(nullable: false),
                        IsCharging = c.Boolean(nullable: false),
                        BatteryPercentage = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HitchBots", t => t.HitchBotId, cascadeDelete: true)
                .Index(t => t.HitchBotId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TabletStatus", "HitchBotId", "dbo.HitchBots");
            DropForeignKey("dbo.ExceptionLogs", "HitchBotId", "dbo.HitchBots");
            DropForeignKey("dbo.SpeechLogEvents", "HitchBotId", "dbo.HitchBots");
            DropForeignKey("dbo.HitchBots", "JourneyId", "dbo.Journeys");
            DropForeignKey("dbo.Journeys", "StartLocation_Id", "dbo.Locations");
            DropForeignKey("dbo.Journeys", "EndLocation_Id", "dbo.Locations");
            DropForeignKey("dbo.Locations", "HitchBotId", "dbo.HitchBots");
            DropIndex("dbo.TabletStatus", new[] { "HitchBotId" });
            DropIndex("dbo.SpeechLogEvents", new[] { "HitchBotId" });
            DropIndex("dbo.Locations", new[] { "HitchBotId" });
            DropIndex("dbo.Journeys", new[] { "StartLocation_Id" });
            DropIndex("dbo.Journeys", new[] { "EndLocation_Id" });
            DropIndex("dbo.HitchBots", new[] { "JourneyId" });
            DropIndex("dbo.ExceptionLogs", new[] { "HitchBotId" });
            DropTable("dbo.TabletStatus");
            DropTable("dbo.SpeechLogEvents");
            DropTable("dbo.Locations");
            DropTable("dbo.Journeys");
            DropTable("dbo.HitchBots");
            DropTable("dbo.ExceptionLogs");
        }
    }
}
