namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class diddj548 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BookCopies", "InventoryId", c => c.Int(nullable: true));
        }
        
        public override void Down()
        {
        }
    }
}
