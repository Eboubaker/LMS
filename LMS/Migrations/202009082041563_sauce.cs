namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sauce : DbMigration
    {
        public override void Up()
        {

        }
        
        public override void Down()
        {
            AddColumn("dbo.Rentals", "BookId", c => c.Int(nullable: false));
            AddColumn("dbo.Rentals", "BookCopyId", c => c.Int(nullable: false));
            CreateIndex("dbo.Rentals", "BookId");
            CreateIndex("dbo.Rentals", "BookCopyId");
            AddForeignKey("dbo.Rentals", "BookCopyId", "dbo.BookCopies", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Rentals", "BookId", "dbo.Books", "Id", cascadeDelete: true);
        }
    }
}
