namespace hitchbotAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CleverScriptAPI : DbMigration
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
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CleverScriptAPIkeys");
        }
    }
}
