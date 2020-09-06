namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class diddj5489 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BookCopies", "InventoryId", "dbo.Inventory");
            DropForeignKey("dbo.BookCopies", "Inventory_Id", "dbo.Inventory");
            DropIndex("dbo.BookCopies", new[] { "InventoryId" });
            DropIndex("dbo.BookCopies", new[] { "Inventory_Id" });
            DropColumn("dbo.BookCopies", "InventoryId");
            DropColumn("dbo.BookCopies", "Inventory_Id");
        }
        
        public override void Down()
        {
            CreateIndex("dbo.BookCopies", "InventoryId");
            AddForeignKey("dbo.BookCopies", "InventoryId", "dbo.Inventory", "Id", cascadeDelete: true);
        }
    }
}
