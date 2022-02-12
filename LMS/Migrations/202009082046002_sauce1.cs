namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sauce1 : DbMigration
    {
        public override void Up()
        {

            AddColumn("dbo.Rentals", "CustomerId", c => c.Int(nullable: false));

            CreateIndex("dbo.Rentals", "CustomerId");
            AddForeignKey("dbo.Rentals", "CustomerId", "dbo.Customers", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rentals", "BookCopyId", "dbo.BookCopies");
            DropForeignKey("dbo.Rentals", "BookId", "dbo.Books");
            DropIndex("dbo.Rentals", new[] { "BookId" });
            DropIndex("dbo.Rentals", new[] { "BookCopyId" });
            DropColumn("dbo.Rentals", "BookId");
            DropColumn("dbo.Rentals", "BookCopyId");
        }
    }
}
