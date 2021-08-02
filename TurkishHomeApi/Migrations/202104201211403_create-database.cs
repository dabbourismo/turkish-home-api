namespace TurkishHomeApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createdatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Exams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        MaterialId = c.Int(nullable: false),
                        ExamType = c.Int(nullable: false),
                        ApperanceDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Materials", t => t.MaterialId, cascadeDelete: true)
                .Index(t => t.MaterialId);
            
            CreateTable(
                "dbo.Materials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LevelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Levels", t => t.LevelId, cascadeDelete: true)
                .Index(t => t.LevelId);
            
            CreateTable(
                "dbo.Levels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ApperanceDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        LevelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Levels", t => t.LevelId, cascadeDelete: true)
                .Index(t => t.LevelId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notifications", "LevelId", "dbo.Levels");
            DropForeignKey("dbo.Exams", "MaterialId", "dbo.Materials");
            DropForeignKey("dbo.Materials", "LevelId", "dbo.Levels");
            DropIndex("dbo.Notifications", new[] { "LevelId" });
            DropIndex("dbo.Materials", new[] { "LevelId" });
            DropIndex("dbo.Exams", new[] { "MaterialId" });
            DropTable("dbo.Notifications");
            DropTable("dbo.Levels");
            DropTable("dbo.Materials");
            DropTable("dbo.Exams");
        }
    }
}
