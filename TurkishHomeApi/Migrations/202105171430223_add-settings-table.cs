namespace TurkishHomeApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class addsettingstable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Settings",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                })
                .PrimaryKey(t => t.Id);
            Sql("Insert into dbo.Settings (Name) values ('Settings!')");

        }

        public override void Down()
        {
            DropTable("dbo.Settings");
        }
    }
}
