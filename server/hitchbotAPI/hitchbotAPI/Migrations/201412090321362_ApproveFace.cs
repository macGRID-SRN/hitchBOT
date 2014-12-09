namespace hitchbotAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApproveFace : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Faces", "Approved", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Faces", "Approved");
        }
    }
}
