namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class customers11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "RentalsCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "RentalsCount");
        }
    }
}
