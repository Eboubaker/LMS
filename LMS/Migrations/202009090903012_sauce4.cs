namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sauce4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rentals", "Expires", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rentals", "Expires");
        }
    }
}
