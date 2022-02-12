namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sauce2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rentals", "CustomerId", c => c.Int(nullable: false));

            CreateIndex("dbo.Rentals", "CustomerId");
            AddForeignKey("dbo.Rentals", "CustomerId", "dbo.Customers", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
        }
    }
}
