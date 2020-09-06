namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class diddji : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Inventory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Column = c.Int(nullable: false),
                        Row = c.Int(nullable: false),
                        Size = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Inventory");
        }
    }
}
