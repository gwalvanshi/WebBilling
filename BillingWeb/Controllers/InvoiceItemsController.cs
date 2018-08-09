using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BillingWeb;

namespace BillingWeb.Controllers
{
    public class InvoiceItemsController : Controller
    {
        private Billing4Entities db = new Billing4Entities();

        // GET: InvoiceItems
        public ActionResult Index()
        {
            var tblInvoiceItems = db.tblInvoiceItems.Include(t => t.tblInvoice).Include(t => t.tblInvoiceItem1).Include(t => t.tblInvoiceItem2).Include(t => t.tblProduct).Include(t => t.tblSize).Include(t => t.tblTax).Include(t => t.tblUnit);
            return View(tblInvoiceItems.ToList());
        }

        // GET: InvoiceItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblInvoiceItem tblInvoiceItem = db.tblInvoiceItems.Find(id);
            if (tblInvoiceItem == null)
            {
                return HttpNotFound();
            }
            return View(tblInvoiceItem);
        }

        // GET: InvoiceItems/Create
        public ActionResult Create()
        {
            ViewBag.InvoiceID = new SelectList(db.tblInvoices, "InvoiceID", "InvoiceNo");
            ViewBag.InvoiceItemID = new SelectList(db.tblInvoiceItems, "InvoiceItemID", "Make");
            ViewBag.InvoiceItemID = new SelectList(db.tblInvoiceItems, "InvoiceItemID", "Make");
            ViewBag.ProductID = new SelectList(db.tblProducts, "ProductID", "ProductName");
            ViewBag.SizeID = new SelectList(db.tblSizes, "SizeID", "SizeName");
            ViewBag.TaxID = new SelectList(db.tblTaxes, "TaxID", "TaxName");
            ViewBag.UnitID = new SelectList(db.tblUnits, "UnitID", "Name");
            return View();
        }

        // POST: InvoiceItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InvoiceItemID,InvoiceID,ProductID,Make,Quantity,UnitID,SizeID,RatePerUnit,TaxID,Tax,TaxAmount,Discount,DiscountAmount,TotalAmount,Remark,HSN_SAC,IsActive,SGST,CGST")] tblInvoiceItem tblInvoiceItem)
        {
            if (ModelState.IsValid)
            {
                db.tblInvoiceItems.Add(tblInvoiceItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InvoiceID = new SelectList(db.tblInvoices, "InvoiceID", "InvoiceNo", tblInvoiceItem.InvoiceID);
            ViewBag.InvoiceItemID = new SelectList(db.tblInvoiceItems, "InvoiceItemID", "Make", tblInvoiceItem.InvoiceItemID);
            ViewBag.InvoiceItemID = new SelectList(db.tblInvoiceItems, "InvoiceItemID", "Make", tblInvoiceItem.InvoiceItemID);
            ViewBag.ProductID = new SelectList(db.tblProducts, "ProductID", "ProductName", tblInvoiceItem.ProductID);
            ViewBag.SizeID = new SelectList(db.tblSizes, "SizeID", "SizeName", tblInvoiceItem.SizeID);
            ViewBag.TaxID = new SelectList(db.tblTaxes, "TaxID", "TaxName", tblInvoiceItem.TaxID);
            ViewBag.UnitID = new SelectList(db.tblUnits, "UnitID", "Name", tblInvoiceItem.UnitID);
            return View(tblInvoiceItem);
        }

        // GET: InvoiceItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblInvoiceItem tblInvoiceItem = db.tblInvoiceItems.Find(id);
            if (tblInvoiceItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.InvoiceID = new SelectList(db.tblInvoices, "InvoiceID", "InvoiceNo", tblInvoiceItem.InvoiceID);
            ViewBag.InvoiceItemID = new SelectList(db.tblInvoiceItems, "InvoiceItemID", "Make", tblInvoiceItem.InvoiceItemID);
            ViewBag.InvoiceItemID = new SelectList(db.tblInvoiceItems, "InvoiceItemID", "Make", tblInvoiceItem.InvoiceItemID);
            ViewBag.ProductID = new SelectList(db.tblProducts, "ProductID", "ProductName", tblInvoiceItem.ProductID);
            ViewBag.SizeID = new SelectList(db.tblSizes, "SizeID", "SizeName", tblInvoiceItem.SizeID);
            ViewBag.TaxID = new SelectList(db.tblTaxes, "TaxID", "TaxName", tblInvoiceItem.TaxID);
            ViewBag.UnitID = new SelectList(db.tblUnits, "UnitID", "Name", tblInvoiceItem.UnitID);
            return View(tblInvoiceItem);
        }

        // POST: InvoiceItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InvoiceItemID,InvoiceID,ProductID,Make,Quantity,UnitID,SizeID,RatePerUnit,TaxID,Tax,TaxAmount,Discount,DiscountAmount,TotalAmount,Remark,HSN_SAC,IsActive,SGST,CGST")] tblInvoiceItem tblInvoiceItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblInvoiceItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InvoiceID = new SelectList(db.tblInvoices, "InvoiceID", "InvoiceNo", tblInvoiceItem.InvoiceID);
            ViewBag.InvoiceItemID = new SelectList(db.tblInvoiceItems, "InvoiceItemID", "Make", tblInvoiceItem.InvoiceItemID);
            ViewBag.InvoiceItemID = new SelectList(db.tblInvoiceItems, "InvoiceItemID", "Make", tblInvoiceItem.InvoiceItemID);
            ViewBag.ProductID = new SelectList(db.tblProducts, "ProductID", "ProductName", tblInvoiceItem.ProductID);
            ViewBag.SizeID = new SelectList(db.tblSizes, "SizeID", "SizeName", tblInvoiceItem.SizeID);
            ViewBag.TaxID = new SelectList(db.tblTaxes, "TaxID", "TaxName", tblInvoiceItem.TaxID);
            ViewBag.UnitID = new SelectList(db.tblUnits, "UnitID", "Name", tblInvoiceItem.UnitID);
            return View(tblInvoiceItem);
        }

        // GET: InvoiceItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblInvoiceItem tblInvoiceItem = db.tblInvoiceItems.Find(id);
            if (tblInvoiceItem == null)
            {
                return HttpNotFound();
            }
            return View(tblInvoiceItem);
        }

        // POST: InvoiceItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblInvoiceItem tblInvoiceItem = db.tblInvoiceItems.Find(id);
            db.tblInvoiceItems.Remove(tblInvoiceItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
