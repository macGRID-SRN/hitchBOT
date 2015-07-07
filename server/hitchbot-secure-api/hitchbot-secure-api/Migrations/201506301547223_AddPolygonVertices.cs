namespace hitchbot_secure_api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPolygonVertices : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PolgonVertexes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LocationId = c.Int(),
                        CleverscriptContentId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CleverscriptContents", t => t.CleverscriptContentId)
                .ForeignKey("dbo.Locations", t => t.LocationId)
                .Index(t => t.LocationId)
                .Index(t => t.CleverscriptContentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PolgonVertexes", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.PolgonVertexes", "CleverscriptContentId", "dbo.CleverscriptContents");
            DropIndex("dbo.PolgonVertexes", new[] { "CleverscriptContentId" });
            DropIndex("dbo.PolgonVertexes", new[] { "LocationId" });
            DropTable("dbo.PolgonVertexes");
        }
    }
}
