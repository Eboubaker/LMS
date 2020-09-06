namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lflfl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "RentalsCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "RentalsCount");
        }
    }
}
