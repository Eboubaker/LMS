namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addbasebooktorentalforeaseofuse2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rentals", "BookId", c => c.Int(nullable: false));
            CreateIndex("dbo.Rentals", "BookId");
            AddForeignKey("dbo.Rentals", "BookId", "dbo.Books", "Id");
        }
        
        public override void Down()
        {
        }
    }
}
