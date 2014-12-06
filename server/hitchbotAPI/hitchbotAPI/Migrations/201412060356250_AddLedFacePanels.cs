namespace hitchbotAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLedFacePanels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Faces",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        TimeAdded = c.DateTime(nullable: false),
                        UserAccount_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Passwords", t => t.UserAccount_ID)
                .Index(t => t.UserAccount_ID);
            
            CreateTable(
                "dbo.LedPanels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TimeAdded = c.DateTime(nullable: false),
                        Face_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Faces", t => t.Face_ID)
                .Index(t => t.Face_ID);
            
            CreateTable(
                "dbo.Rows",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ColSet0 = c.Byte(nullable: false),
                        ColSet1 = c.Byte(nullable: false),
                        ColSet2 = c.Byte(nullable: false),
                        LedPanel_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.LedPanels", t => t.LedPanel_ID)
                .Index(t => t.LedPanel_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Faces", "UserAccount_ID", "dbo.Passwords");
            DropForeignKey("dbo.LedPanels", "Face_ID", "dbo.Faces");
            DropForeignKey("dbo.Rows", "LedPanel_ID", "dbo.LedPanels");
            DropIndex("dbo.Rows", new[] { "LedPanel_ID" });
            DropIndex("dbo.LedPanels", new[] { "Face_ID" });
            DropIndex("dbo.Faces", new[] { "UserAccount_ID" });
            DropTable("dbo.Rows");
            DropTable("dbo.LedPanels");
            DropTable("dbo.Faces");
        }
    }
}
