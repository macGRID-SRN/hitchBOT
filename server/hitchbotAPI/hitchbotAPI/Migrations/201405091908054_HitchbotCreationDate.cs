namespace hitchbotAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class HitchbotCreationDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.hitchBOTs", "CreationTime", c => c.DateTime(nullable: false, defaultValue: DateTime.UtcNow));
        }

        public override void Down()
        {
            DropColumn("dbo.hitchBOTs", "CreationTime");
        }
    }
}
