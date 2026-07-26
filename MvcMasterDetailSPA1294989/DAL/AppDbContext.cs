using MvcMasterDetailSPA1294989.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MvcMasterDetailSPA1294989.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(): base("AppDbContext")
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<InvoiceItem> InvoiceItems { get; set; }
    }
}