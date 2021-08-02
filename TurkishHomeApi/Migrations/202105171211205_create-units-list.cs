namespace TurkishHomeApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createunitslist : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Units",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        MaterialId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Materials", t => t.MaterialId, cascadeDelete: true)
                .Index(t => t.MaterialId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Units", "MaterialId", "dbo.Materials");
            DropIndex("dbo.Units", new[] { "MaterialId" });
            DropTable("dbo.Units");
        }
    }
}
