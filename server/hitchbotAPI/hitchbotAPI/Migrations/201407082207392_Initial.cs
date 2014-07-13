namespace hitchbotAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CleverScriptAPIkeys",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        APIkey = c.String(),
                        Description = c.String(),
                        TimeAdded = c.DateTime(nullable: false),
                        HitchBOT_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.hitchBOTs", t => t.HitchBOT_ID)
                .Index(t => t.HitchBOT_ID);
            
            CreateTable(
                "dbo.hitchBOTs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreationTime = c.DateTime(nullable: false),
                        TimeAdded = c.DateTime(nullable: false),
                        Project_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Projects", t => t.Project_ID)
                .Index(t => t.Project_ID);
            
            CreateTable(
                "dbo.Conversations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(),
                        TimeAdded = c.DateTime(nullable: false),
                        StartLocation_ID = c.Int(),
                        hitchBOT_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Locations", t => t.StartLocation_ID)
                .ForeignKey("dbo.hitchBOTs", t => t.hitchBOT_ID)
                .Index(t => t.StartLocation_ID)
                .Index(t => t.hitchBOT_ID);
            
            CreateTable(
                "dbo.ListenEvents",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SpeechHeard = c.String(),
                        HeardTime = c.DateTime(nullable: false),
                        TimeAdded = c.DateTime(nullable: false),
                        Conversation_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Conversations", t => t.Conversation_ID)
                .Index(t => t.Conversation_ID);
            
            CreateTable(
                "dbo.SpeechEvents",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SpeechSaid = c.String(),
                        OccuredTime = c.DateTime(nullable: false),
                        TimeAdded = c.DateTime(nullable: false),
                        Conversation_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Conversations", t => t.Conversation_ID)
                .Index(t => t.Conversation_ID);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NearestCity = c.String(),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        Altitude = c.Double(),
                        Accuracy = c.Single(),
                        Velocity = c.Single(),
                        TakenTime = c.DateTime(nullable: false),
                        TimeAdded = c.DateTime(nullable: false),
                        hitchBOT_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.hitchBOTs", t => t.hitchBOT_ID)
                .Index(t => t.hitchBOT_ID);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        url = c.String(),
                        TimeTaken = c.DateTime(nullable: false),
                        TimeApproved = c.DateTime(),
                        TimeDenied = c.DateTime(),
                        TimeAdded = c.DateTime(nullable: false),
                        HitchBOT_ID = c.Int(),
                        Location_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.hitchBOTs", t => t.HitchBOT_ID)
                .ForeignKey("dbo.Locations", t => t.Location_ID)
                .Index(t => t.HitchBOT_ID)
                .Index(t => t.Location_ID);
            
            CreateTable(
                "dbo.Passwords",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Description = c.String(),
                        Hash = c.String(),
                        hitchBOT_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.hitchBOTs", t => t.hitchBOT_ID)
                .Index(t => t.hitchBOT_ID);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(),
                        Description = c.String(),
                        TimeAdded = c.DateTime(nullable: false),
                        EndLocation_ID = c.Int(),
                        StartLocation_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Locations", t => t.EndLocation_ID)
                .ForeignKey("dbo.Locations", t => t.StartLocation_ID)
                .Index(t => t.EndLocation_ID)
                .Index(t => t.StartLocation_ID);
            
            CreateTable(
                "dbo.GoogleMapsStatics",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        URL = c.String(),
                        TimeGenerated = c.DateTime(nullable: false),
                        TimeAdded = c.DateTime(nullable: false),
                        HitchBot_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.hitchBOTs", t => t.HitchBot_ID)
                .Index(t => t.HitchBot_ID);
            
            CreateTable(
                "dbo.TwitterAccounts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.String(),
                        consumerKey = c.String(),
                        consumerSecret = c.String(),
                        accessToken = c.String(),
                        accessTokenSecret = c.String(),
                        TimeAdded = c.DateTime(nullable: false),
                        HitchBot_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.hitchBOTs", t => t.HitchBot_ID)
                .Index(t => t.HitchBot_ID);
            
            CreateTable(
                "dbo.TwitterFriends",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.String(),
                        ScreenName = c.String(),
                        TimeAdded = c.DateTime(nullable: false),
                        TimeFollowed = c.DateTime(),
                        TwitterAccount_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.TwitterAccounts", t => t.TwitterAccount_ID)
                .Index(t => t.TwitterAccount_ID);
            
            CreateTable(
                "dbo.TwitterLocationTargets",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TweetText = c.String(),
                        RadiusKM = c.Double(nullable: false),
                        TimeAdded = c.DateTime(nullable: false),
                        HitchBot_ID = c.Int(),
                        Status_ID = c.Int(),
                        TargetLocation_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.hitchBOTs", t => t.HitchBot_ID)
                .ForeignKey("dbo.TwitterStatus", t => t.Status_ID)
                .ForeignKey("dbo.Locations", t => t.TargetLocation_ID)
                .Index(t => t.HitchBot_ID)
                .Index(t => t.Status_ID)
                .Index(t => t.TargetLocation_ID);
            
            CreateTable(
                "dbo.TwitterStatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TweetID = c.String(),
                        Text = c.String(maxLength: 140),
                        TimeAdded = c.DateTime(nullable: false),
                        TimeTweeted = c.DateTime(nullable: false),
                        TwitterAccount_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.TwitterAccounts", t => t.TwitterAccount_ID)
                .Index(t => t.TwitterAccount_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TwitterLocationTargets", "TargetLocation_ID", "dbo.Locations");
            DropForeignKey("dbo.TwitterLocationTargets", "Status_ID", "dbo.TwitterStatus");
            DropForeignKey("dbo.TwitterStatus", "TwitterAccount_ID", "dbo.TwitterAccounts");
            DropForeignKey("dbo.TwitterLocationTargets", "HitchBot_ID", "dbo.hitchBOTs");
            DropForeignKey("dbo.TwitterFriends", "TwitterAccount_ID", "dbo.TwitterAccounts");
            DropForeignKey("dbo.TwitterAccounts", "HitchBot_ID", "dbo.hitchBOTs");
            DropForeignKey("dbo.GoogleMapsStatics", "HitchBot_ID", "dbo.hitchBOTs");
            DropForeignKey("dbo.Projects", "StartLocation_ID", "dbo.Locations");
            DropForeignKey("dbo.hitchBOTs", "Project_ID", "dbo.Projects");
            DropForeignKey("dbo.Projects", "EndLocation_ID", "dbo.Locations");
            DropForeignKey("dbo.Passwords", "hitchBOT_ID", "dbo.hitchBOTs");
            DropForeignKey("dbo.Images", "Location_ID", "dbo.Locations");
            DropForeignKey("dbo.Images", "HitchBOT_ID", "dbo.hitchBOTs");
            DropForeignKey("dbo.CleverScriptAPIkeys", "HitchBOT_ID", "dbo.hitchBOTs");
            DropForeignKey("dbo.Locations", "hitchBOT_ID", "dbo.hitchBOTs");
            DropForeignKey("dbo.Conversations", "hitchBOT_ID", "dbo.hitchBOTs");
            DropForeignKey("dbo.Conversations", "StartLocation_ID", "dbo.Locations");
            DropForeignKey("dbo.SpeechEvents", "Conversation_ID", "dbo.Conversations");
            DropForeignKey("dbo.ListenEvents", "Conversation_ID", "dbo.Conversations");
            DropIndex("dbo.TwitterStatus", new[] { "TwitterAccount_ID" });
            DropIndex("dbo.TwitterLocationTargets", new[] { "TargetLocation_ID" });
            DropIndex("dbo.TwitterLocationTargets", new[] { "Status_ID" });
            DropIndex("dbo.TwitterLocationTargets", new[] { "HitchBot_ID" });
            DropIndex("dbo.TwitterFriends", new[] { "TwitterAccount_ID" });
            DropIndex("dbo.TwitterAccounts", new[] { "HitchBot_ID" });
            DropIndex("dbo.GoogleMapsStatics", new[] { "HitchBot_ID" });
            DropIndex("dbo.Projects", new[] { "StartLocation_ID" });
            DropIndex("dbo.Projects", new[] { "EndLocation_ID" });
            DropIndex("dbo.Passwords", new[] { "hitchBOT_ID" });
            DropIndex("dbo.Images", new[] { "Location_ID" });
            DropIndex("dbo.Images", new[] { "HitchBOT_ID" });
            DropIndex("dbo.Locations", new[] { "hitchBOT_ID" });
            DropIndex("dbo.SpeechEvents", new[] { "Conversation_ID" });
            DropIndex("dbo.ListenEvents", new[] { "Conversation_ID" });
            DropIndex("dbo.Conversations", new[] { "hitchBOT_ID" });
            DropIndex("dbo.Conversations", new[] { "StartLocation_ID" });
            DropIndex("dbo.hitchBOTs", new[] { "Project_ID" });
            DropIndex("dbo.CleverScriptAPIkeys", new[] { "HitchBOT_ID" });
            DropTable("dbo.TwitterStatus");
            DropTable("dbo.TwitterLocationTargets");
            DropTable("dbo.TwitterFriends");
            DropTable("dbo.TwitterAccounts");
            DropTable("dbo.GoogleMapsStatics");
            DropTable("dbo.Projects");
            DropTable("dbo.Passwords");
            DropTable("dbo.Images");
            DropTable("dbo.Locations");
            DropTable("dbo.SpeechEvents");
            DropTable("dbo.ListenEvents");
            DropTable("dbo.Conversations");
            DropTable("dbo.hitchBOTs");
            DropTable("dbo.CleverScriptAPIkeys");
        }
    }
}
