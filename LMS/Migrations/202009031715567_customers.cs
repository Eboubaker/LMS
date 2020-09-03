namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class customers : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Customers", new[] { "StateId" });
            AddColumn("dbo.Customers", "CardId", c => c.String(nullable: false, maxLength: 25));
            CreateIndex("dbo.Customers", "CardId", unique: true);
            DropColumn("dbo.Customers", "StateId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "StateId", c => c.String(nullable: false, maxLength: 25));
            DropIndex("dbo.Customers", new[] { "CardId" });
            DropColumn("dbo.Customers", "CardId");
            CreateIndex("dbo.Customers", "StateId", unique: true);
        }
    }
}
