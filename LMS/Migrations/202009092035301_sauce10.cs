namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sauce10 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Books", "RentalsCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "RentalsCount", c => c.Int(nullable: false));
        }
    }
}
