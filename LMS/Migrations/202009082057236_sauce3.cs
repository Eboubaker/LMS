namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sauce3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.BookCopies", "InventoryColumn");
            DropColumn("dbo.BookCopies", "InventoryRow");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BookCopies", "InventoryRow", c => c.Int(nullable: false));
            AddColumn("dbo.BookCopies", "InventoryColumn", c => c.Int(nullable: false));
        }
    }
}
