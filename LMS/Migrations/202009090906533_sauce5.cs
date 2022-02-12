namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sauce5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BookCopies", "RentalId", c => c.Int(nullable: true));
            CreateIndex("dbo.BookCopies", "RentalId");
            AddForeignKey("dbo.BookCopies", "RentalId", "dbo.Rentals", "Id");
        }
        
        public override void Down()
        {
        }
    }
}
