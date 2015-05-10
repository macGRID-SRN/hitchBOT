namespace hitchbotAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSpeechLogScoreAndType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SpeechLogEvents", "RecognitionScore", c => c.Int());
            AddColumn("dbo.SpeechLogEvents", "GoogleRecognitionScore", c => c.Int());
            AddColumn("dbo.SpeechLogEvents", "ResponseScore", c => c.Int());
            AddColumn("dbo.SpeechLogEvents", "RecognizerType", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SpeechLogEvents", "RecognizerType");
            DropColumn("dbo.SpeechLogEvents", "ResponseScore");
            DropColumn("dbo.SpeechLogEvents", "GoogleRecognitionScore");
            DropColumn("dbo.SpeechLogEvents", "RecognitionScore");
        }
    }
}
