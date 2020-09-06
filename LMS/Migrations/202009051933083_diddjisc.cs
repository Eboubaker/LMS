namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class diddjisc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BookCopies", "InventoryId", c => c.Int(nullable: true));
            CreateIndex("dbo.BookCopies", "InventoryId");
            AddForeignKey("dbo.BookCopies", "InventoryId", "dbo.Inventory", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookCopies", "InventoryId", "dbo.Inventory");
            DropIndex("dbo.BookCopies", new[] { "InventoryId" });
            DropColumn("dbo.BookCopies", "InventoryId");
        }
    }
}
