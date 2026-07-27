using MvcMasterDetailSPA1294989.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcMasterDetailSPA1294989.ViewModels
{
    public class InvoiceViewModel
    {

        public int InvoiceId { get; set; }

        [Required(ErrorMessage = "Invoice Date is required.")]
        [Display(Name = "Invoice Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime InvoiceDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Customer Name is required.")]
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Display(Name = "Customer Address")]
        public string CustomerAddress { get; set; }

        public bool IsPaid { get; set; }

        [Required(ErrorMessage = "Employee is required.")]
        [Display(Name = "Employee")]
        public int EmployeeId { get; set; }

      
        public virtual IList<Employee> Employees { get; set; }

        
        public virtual IList<Invoice> Invoices { get; set; }

     
        public virtual IList<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();
    }
}