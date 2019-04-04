namespace LibraryService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Book",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Author = c.String(),
                        BookGenre = c.Int(nullable: false),
                        Pages = c.Int(nullable: false),
                        Title = c.String(),
                        Publisher = c.String(),
                        AgeRestriction = c.Int(nullable: false),
                        PublishedAt = c.DateTime(),
                        Status = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        Genre = c.Int(nullable: false),
                        PurchaseValue = c.Single(nullable: false),
                        DateAdded = c.DateTime(),
                        LibraryId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        ReturnDate = c.DateTime(),
                        Library_LibraryId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Library", t => t.Library_LibraryId)
                .Index(t => t.Library_LibraryId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(),
                        Password = c.String(),
                        Address = c.String(),
                        Role = c.String(),
                        MemberSince = c.DateTime(),
                        DateOfBirth = c.DateTime(),
                        Book_id = c.Int(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Book", t => t.Book_id)
                .Index(t => t.Book_id);
            
            CreateTable(
                "dbo.Library",
                c => new
                    {
                        LibraryId = c.String(nullable: false, maxLength: 128),
                        Address = c.String(),
                        PostCode = c.String(),
                        Name = c.String(),
                        Capacity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LibraryId);
            
            CreateTable(
                "dbo.DVD",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Director = c.String(),
                        Duration = c.Int(nullable: false),
                        DVDGenre = c.Int(nullable: false),
                        Title = c.String(),
                        Publisher = c.String(),
                        AgeRestriction = c.Int(nullable: false),
                        PublishedAt = c.DateTime(),
                        Status = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        Genre = c.Int(nullable: false),
                        PurchaseValue = c.Single(nullable: false),
                        DateAdded = c.DateTime(),
                        LibraryId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        ReturnDate = c.DateTime(),
                        Library_LibraryId = c.String(maxLength: 128),
                        User_UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Library", t => t.Library_LibraryId)
                .ForeignKey("dbo.User", t => t.User_UserId)
                .Index(t => t.Library_LibraryId)
                .Index(t => t.User_UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DVD", "User_UserId", "dbo.User");
            DropForeignKey("dbo.DVD", "Library_LibraryId", "dbo.Library");
            DropForeignKey("dbo.Book", "Library_LibraryId", "dbo.Library");
            DropForeignKey("dbo.User", "Book_id", "dbo.Book");
            DropIndex("dbo.DVD", new[] { "User_UserId" });
            DropIndex("dbo.DVD", new[] { "Library_LibraryId" });
            DropIndex("dbo.User", new[] { "Book_id" });
            DropIndex("dbo.Book", new[] { "Library_LibraryId" });
            DropTable("dbo.DVD");
            DropTable("dbo.Library");
            DropTable("dbo.User");
            DropTable("dbo.Book");
        }
    }
}
