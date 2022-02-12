namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sauce9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BookCopies", "Rented", c => c.Boolean(nullable: false, defaultValue:false));
        }
        
        public override void Down()
        {
        }
    }
}
