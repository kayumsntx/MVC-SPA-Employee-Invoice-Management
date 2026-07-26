using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcMasterDetailSPA1294989.Models
{
    public class InvoiceItem
    {
        [Key]
        public int InvoiceItemId { get; set; }

       
        public string ProductName { get; set; }

        
        public int Quantity { get; set; }



        [Column(TypeName = "decimal")]
        public decimal UnitPrice { get; set; }


        [ForeignKey("Invoice")]
        public int InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; }
    }
}