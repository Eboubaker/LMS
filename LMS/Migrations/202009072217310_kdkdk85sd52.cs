namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kdkdk85sd52 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Customers", "RentalsCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "RentalsCount", c => c.Int(nullable: false));
        }
    }
}
