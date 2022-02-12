namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sauce6 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.BookCopies", "Rented");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BookCopies", "Rented", c => c.Boolean(nullable: false));
        }
    }
}
