namespace hitchbotAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddTwitterAccounts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TwitterAccounts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        consumerKey = c.String(nullable: false),
                        consumerSecret = c.String(nullable: false),
                        TimeAdded = c.DateTime(nullable: false),
                        HitchBot_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.hitchBOTs", t => t.HitchBot_ID)
                .Index(t => t.HitchBot_ID);

        }

        public override void Down()
        {
            DropForeignKey("dbo.TwitterAccounts", "HitchBot_ID", "dbo.hitchBOTs");
            DropIndex("dbo.TwitterAccounts", new[] { "HitchBot_ID" });
            DropTable("dbo.TwitterAccounts");
        }
    }
}
