namespace LibraryService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tryingtoaddbookstouser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.User", "Book_id", "dbo.Book");
            DropIndex("dbo.User", new[] { "Book_id" });
            CreateTable(
                "dbo.UserBook",
                c => new
                    {
                        User_UserId = c.String(nullable: false, maxLength: 128),
                        Book_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_UserId, t.Book_id })
                .ForeignKey("dbo.User", t => t.User_UserId, cascadeDelete: true)
                .ForeignKey("dbo.Book", t => t.Book_id, cascadeDelete: true)
                .Index(t => t.User_UserId)
                .Index(t => t.Book_id);
            
            DropColumn("dbo.User", "Book_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "Book_id", c => c.Int());
            DropForeignKey("dbo.UserBook", "Book_id", "dbo.Book");
            DropForeignKey("dbo.UserBook", "User_UserId", "dbo.User");
            DropIndex("dbo.UserBook", new[] { "Book_id" });
            DropIndex("dbo.UserBook", new[] { "User_UserId" });
            DropTable("dbo.UserBook");
            CreateIndex("dbo.User", "Book_id");
            AddForeignKey("dbo.User", "Book_id", "dbo.Book", "id");
        }
    }
}
