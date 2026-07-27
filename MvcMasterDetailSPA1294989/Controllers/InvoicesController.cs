using MvcMasterDetailSPA1294989.DAL;
using MvcMasterDetailSPA1294989.Models;
using MvcMasterDetailSPA1294989.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;

namespace MvcMasterDetailSPA1294989.Controllers
{

    public class InvoicesController : Controller
    {
        AppDbContext db = new AppDbContext();

        public ActionResult Index()
        {
            IEnumerable<Invoice> invoices = db.Invoices.Include(i => i.Employee).Include(i => i.InvoiceItems).ToList();
            return View(invoices);
        }

        [HttpGet]
        public ActionResult CreatePartial()
        {
            InvoiceViewModel vobj = new InvoiceViewModel();
            vobj.Employees = db.Employees.ToList();
            vobj.Employees = db.Employees
.ToList()
.Select(e => new Employee
{
EmployeeId = e.EmployeeId,
EmployeeName = e.EmployeeName + " - " + e.EmployeeId
})
.ToList();
            vobj.InvoiceItems.Add(new InvoiceItem() { InvoiceItemId = 0 });
            return PartialView("_CreateInvoicePartial", vobj);
        }

        public ActionResult InvoiceListPartial()
        {
            IEnumerable<Invoice> invoices = db.Invoices.Include(i => i.Employee).Include(i => i.InvoiceItems).ToList();
            return PartialView("_InvoiceListPartial", invoices);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CreateInvoice(InvoiceViewModel vobj)
        {
            if (!ModelState.IsValid)
            {
                vobj.Employees = db.Employees.ToList();
                return Json(new { success = false });
            }

            Invoice invoice = new Invoice
            {
                InvoiceDate = vobj.InvoiceDate,
                CustomerName = vobj.CustomerName,
                CustomerAddress = vobj.CustomerAddress,
                IsPaid=vobj.IsPaid,
                EmployeeId = vobj.EmployeeId,
                InvoiceItems = vobj.InvoiceItems
            };

            db.Invoices.Add(invoice);
            try
            {
                db.SaveChanges();
                return Json(new { success = true, redirectUrl = Url.Action("Index") });
            }
            catch (Exception)
            {
                vobj.Employees = db.Employees.ToList();
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public JsonResult DeleteInvoice(int id)
        {
            Invoice invoice = db.Invoices.Find(id);
            if (invoice != null)
            {
                var items = db.InvoiceItems.Where(x => x.InvoiceId == id).ToList();
                db.InvoiceItems.RemoveRange(items);
                db.Entry(invoice).State = EntityState.Deleted;
                db.SaveChanges();
                return Json(new { success = true, redirectUrl = Url.Action("Index") });
            }
            return Json(new { success = false, message = "Invoice not found." });
        }

        public ActionResult EditPartial(int id)
        {
            var invoice = db.Invoices.Include(i => i.InvoiceItems).FirstOrDefault(i => i.InvoiceId == id);
            if (invoice == null) return HttpNotFound("Invoice Not found");

            var vObj = new InvoiceViewModel
            {
                InvoiceId = invoice.InvoiceId,
                InvoiceDate = invoice.InvoiceDate,
                CustomerName = invoice.CustomerName,
                CustomerAddress = invoice.CustomerAddress,
                IsPaid = invoice.IsPaid,
                EmployeeId = invoice.EmployeeId,
                InvoiceItems = invoice.InvoiceItems.ToList(),
                Employees = db.Employees.ToList()
            };
            return PartialView("_EditInvoicePartial", vObj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult EditInvoice(InvoiceViewModel vobj)
        {
            if (!ModelState.IsValid)
            {
                vobj.Employees = db.Employees.ToList();
                vobj.Employees = db.Employees
.ToList()
.Select(e => new Employee
{
EmployeeId = e.EmployeeId,
EmployeeName = e.EmployeeName + " - " + e.EmployeeId
})
.ToList();
                return Json(new { success = false });
            }

            Invoice obj = db.Invoices.Include(a => a.InvoiceItems).FirstOrDefault(x => x.InvoiceId == vobj.InvoiceId);
            if (obj != null)
            {
                obj.InvoiceDate = vobj.InvoiceDate;
                obj.CustomerName = vobj.CustomerName;
                obj.CustomerAddress = vobj.CustomerAddress;
                obj.IsPaid = vobj.IsPaid;
                obj.EmployeeId = vobj.EmployeeId;

                var updatedItemIds = vobj.InvoiceItems.Where(m => m.InvoiceItemId > 0).Select(m => m.InvoiceItemId).ToList();
                var itemsToRemove = obj.InvoiceItems.Where(m => !updatedItemIds.Contains(m.InvoiceItemId)).ToList();
                foreach (var item in itemsToRemove)
                {
                    db.InvoiceItems.Remove(item);
                }

                foreach (var item in vobj.InvoiceItems)
                {
                    if (item.InvoiceItemId > 0)
                    {
                        var existing = obj.InvoiceItems.FirstOrDefault(m => m.InvoiceItemId == item.InvoiceItemId);
                        if (existing != null)
                        {
                            existing.ProductName = item.ProductName;
                            existing.Quantity = item.Quantity;
                            existing.UnitPrice = item.UnitPrice;
                        }
                    }
                    else
                    {
                        obj.InvoiceItems.Add(new InvoiceItem
                        {
                            InvoiceId = obj.InvoiceId,
                            ProductName = item.ProductName,
                            Quantity = item.Quantity,
                            UnitPrice = item.UnitPrice
                        });
                    }
                }

                try
                {
                    db.SaveChanges();
                    return Json(new { success = true, redirectUrl = Url.Action("Index") });
                }
                catch (Exception)
                {
                    vobj.Employees = db.Employees.ToList();
                    return Json(new { success = false });
                }
            }
            return Json(new { success = false, errors = new[] { "Invoice not found." } });
        }

    }
}