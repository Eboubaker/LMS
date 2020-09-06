namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class diddjiscsg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BookCopies", "Inventory_Id", c => c.Int());
            AlterColumn("dbo.BookCopies", "Inventory_Id", c => c.Int(nullable:true));
            CreateIndex("dbo.BookCopies", "Inventory_Id");
            AddForeignKey("dbo.BookCopies", "Inventory_Id", "dbo.Inventory", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "Inventory_Id", "dbo.Inventory");
            DropIndex("dbo.Books", new[] { "Inventory_Id" });
            DropColumn("dbo.Books", "Inventory_Id");
        }
    }
}
