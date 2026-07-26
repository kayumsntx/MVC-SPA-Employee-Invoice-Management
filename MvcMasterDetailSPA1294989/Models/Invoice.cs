using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcMasterDetailSPA1294989.Models
{
    public class Invoice
    {
        public Invoice()
        {
            this.InvoiceItems = new HashSet<InvoiceItem>();
        }

        [Key]
        public int InvoiceId { get; set; }


        [Required, Display(Name = "Invoice Date"), DataType(DataType.Date),
         DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime InvoiceDate { get; set; }

        [Required, Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }

        
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }
    }
}