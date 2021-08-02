namespace TurkishHomeApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class noidea : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Exams", "MaterialId", "dbo.Materials");
            DropIndex("dbo.Exams", new[] { "MaterialId" });
            AddColumn("dbo.Exams", "UnitId", c => c.Int(nullable: false));
            CreateIndex("dbo.Exams", "UnitId");
            AddForeignKey("dbo.Exams", "UnitId", "dbo.Units", "Id", cascadeDelete: true);
            DropColumn("dbo.Exams", "MaterialId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Exams", "MaterialId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Exams", "UnitId", "dbo.Units");
            DropIndex("dbo.Exams", new[] { "UnitId" });
            DropColumn("dbo.Exams", "UnitId");
            CreateIndex("dbo.Exams", "MaterialId");
            AddForeignKey("dbo.Exams", "MaterialId", "dbo.Materials", "Id", cascadeDelete: true);
        }
    }
}
