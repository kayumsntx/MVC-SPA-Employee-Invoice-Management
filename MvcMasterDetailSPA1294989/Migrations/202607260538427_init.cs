namespace MvcMasterDetailSPA1294989.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        EmployeeName = c.String(nullable: false),
                        Designation = c.String(),
                        Department = c.String(),
                        JoinDate = c.DateTime(nullable: false),
                        Email = c.String(),
                        Phone = c.String(),
                        ImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        InvoiceId = c.Int(nullable: false, identity: true),
                        InvoiceDate = c.DateTime(nullable: false),
                        CustomerName = c.String(nullable: false),
                        CustomerAddress = c.String(),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InvoiceId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.InvoiceItems",
                c => new
                    {
                        InvoiceItemId = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        Quantity = c.Int(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        InvoiceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InvoiceItemId)
                .ForeignKey("dbo.Invoices", t => t.InvoiceId, cascadeDelete: true)
                .Index(t => t.InvoiceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InvoiceItems", "InvoiceId", "dbo.Invoices");
            DropForeignKey("dbo.Invoices", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.InvoiceItems", new[] { "InvoiceId" });
            DropIndex("dbo.Invoices", new[] { "EmployeeId" });
            DropTable("dbo.InvoiceItems");
            DropTable("dbo.Invoices");
            DropTable("dbo.Employees");
        }
    }
}
