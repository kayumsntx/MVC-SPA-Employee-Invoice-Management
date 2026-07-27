namespace MvcMasterDetailSPA1294989.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class invoice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "IsPaid", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoices", "IsPaid");
        }
    }
}
