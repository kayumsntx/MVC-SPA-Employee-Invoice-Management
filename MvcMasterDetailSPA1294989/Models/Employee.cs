using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcMasterDetailSPA1294989.Models
{
    public class Employee
    {
        public Employee()
        {
            this.Invoices = new HashSet<Invoice>();
        }

        [Key]
        public int EmployeeId { get; set; }

        [Required,Display(Name ="Employee Name")]
        public string EmployeeName { get; set; }

      
        public string Designation { get; set; }

       
        public string Department { get; set; }

        [Required,Display(Name ="Joining Date"),DataType(DataType.Date),
         DisplayFormat(ApplyFormatInEditMode =true,DataFormatString ="{0:yyyy-MM-dd}")]
     
        public DateTime JoinDate { get; set; }

     
        public string Email { get; set; }

     
        public string Phone { get; set; }

        public string ImageUrl { get; set; }

        
        public virtual ICollection<Invoice> Invoices { get; set; }

    }
}