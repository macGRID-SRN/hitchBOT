namespace hitchbotAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPassword : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Passwords",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Description = c.String(),
                        Hash = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Passwords");
        }
    }
}
