namespace Tender.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(),
                        Description = c.String(),
                        Sponsor = c.String(),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PublicationDate = c.DateTime(nullable: false),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(nullable: false),
                        TypeId = c.Int(),
                        СategoryId = c.Int(),
                        СurrencyId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Types", t => t.TypeId)
                .ForeignKey("dbo.Сategory", t => t.СategoryId)
                .ForeignKey("dbo.Сurrency", t => t.СurrencyId)
                .Index(t => t.TypeId)
                .Index(t => t.СategoryId)
                .Index(t => t.СurrencyId);
            
            CreateTable(
                "dbo.Types",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameType = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Сategory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameCategory = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Сurrency",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameСurrency = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cards", "СurrencyId", "dbo.Сurrency");
            DropForeignKey("dbo.Cards", "СategoryId", "dbo.Сategory");
            DropForeignKey("dbo.Cards", "TypeId", "dbo.Types");
            DropIndex("dbo.Cards", new[] { "СurrencyId" });
            DropIndex("dbo.Cards", new[] { "СategoryId" });
            DropIndex("dbo.Cards", new[] { "TypeId" });
            DropTable("dbo.Сurrency");
            DropTable("dbo.Сategory");
            DropTable("dbo.Types");
            DropTable("dbo.Cards");
        }
    }
}
