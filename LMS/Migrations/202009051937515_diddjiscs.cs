namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class diddjiscs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Inventory", "Shelf", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Inventory", "Shelf");
        }
    }
}
