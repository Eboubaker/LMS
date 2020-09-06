namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kdvndknv : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.BookCopies", name: "BaseBookId", newName: "BookId");
            RenameIndex(table: "dbo.BookCopies", name: "IX_BaseBookId", newName: "IX_BookId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.BookCopies", name: "IX_BookId", newName: "IX_BaseBookId");
            RenameColumn(table: "dbo.BookCopies", name: "BookId", newName: "BaseBookId");
        }
    }
}
