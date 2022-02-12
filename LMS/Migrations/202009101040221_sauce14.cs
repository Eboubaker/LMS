namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sauce14 : DbMigration
    {
        public override void Up()
        {

        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rentals", "BookCopyId", "dbo.BookCopies");
            DropPrimaryKey("dbo.Rentals");
            AlterColumn("dbo.Rentals", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Rentals", "Id");
            RenameIndex(table: "dbo.Rentals", name: "IX_BookCopyId", newName: "IX_Id");
            RenameColumn(table: "dbo.Rentals", name: "BookCopyId", newName: "Id");
            AddColumn("dbo.Rentals", "BookCopyId", c => c.Int(nullable: false));
            AddForeignKey("dbo.Rentals", "Id", "dbo.BookCopies", "Id");
        }
    }
}
