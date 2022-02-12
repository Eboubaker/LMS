namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sauce11 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Books", "NumberInStock");
            DropColumn("dbo.Books", "NumberAvailable");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "NumberAvailable", c => c.Int(nullable: false));
            AddColumn("dbo.Books", "NumberInStock", c => c.Int(nullable: false));
        }
    }
}
