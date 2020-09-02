namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lalala : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customers", "MembershipTypeId", "dbo.MembershipTypes");
            DropIndex("dbo.Customers", new[] { "MembershipTypeId" });
            AddColumn("dbo.Customers", "StateId", c => c.String(nullable: false, maxLength: 25));
            CreateIndex("dbo.Customers", "StateId", unique: true);
            DropColumn("dbo.Customers", "IsSubscripedToNewsLetter");
            DropColumn("dbo.Customers", "MembershipTypeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "MembershipTypeId", c => c.Int(nullable: false));
            AddColumn("dbo.Customers", "IsSubscripedToNewsLetter", c => c.Boolean(nullable: false));
            DropIndex("dbo.Customers", new[] { "StateId" });
            DropColumn("dbo.Customers", "StateId");
            CreateIndex("dbo.Customers", "MembershipTypeId");
            AddForeignKey("dbo.Customers", "MembershipTypeId", "dbo.MembershipTypes", "Id", cascadeDelete: true);
        }
    }
}
