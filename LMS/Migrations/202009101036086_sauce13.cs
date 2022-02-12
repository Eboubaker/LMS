namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sauce13 : DbMigration
    {
        public override void Up()
        {
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rentals", "Id", "dbo.BookCopies");
            DropIndex("dbo.Rentals", new[] { "Id" });
            DropPrimaryKey("dbo.Rentals");
            AlterColumn("dbo.Rentals", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Rentals", "Id");
            RenameColumn(table: "dbo.Rentals", name: "Id", newName: "BookCopyId");
            AddColumn("dbo.Rentals", "Id", c => c.Int(nullable: false, identity: true));
            CreateIndex("dbo.Rentals", "BookCopyId");
            AddForeignKey("dbo.Rentals", "BookCopyId", "dbo.BookCopies", "Id", cascadeDelete: true);
        }
    }
}
