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
    public class InvoicesController : Controller
    {
        private Billing4Entities db = new Billing4Entities();

        // GET: Invoices
        public ActionResult Index()
        {
            var tblInvoices = db.tblInvoices.Include(t => t.tblPaymentMode).Include(t => t.tblUser).Include(t => t.tblUser1).Where(a=>a.IsActive==true);
            return View(tblInvoices.ToList());
        }

        // GET: Invoices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblInvoice tblInvoice = db.tblInvoices.Find(id);
            if (tblInvoice == null)
            {
                return HttpNotFound();
            }
            return View(tblInvoice);
        }

        // GET: Invoices/Create
        public ActionResult Create()
        {
            ViewBag.PaymentModeID = new SelectList(db.tblPaymentModes, "PaymentModeID", "PaymentMode");
            ViewBag.CreatedBy = new SelectList(db.tblUsers, "Id", "UserName");
            ViewBag.UpdatedBy = new SelectList(db.tblUsers, "Id", "UserName");
            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InvoiceID,InvoiceNo,BillingAddress,ShippingAddress,InvoiceType,GSTIN,CustomerName,ContactNumber,Email,Website,PaymentModeID,IsPaid,IsOnCredit,InvoiceDate,PaymentExpectedBy,Remarks,IsActive,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy")] tblInvoice tblInvoice)
        {
            if (ModelState.IsValid)
            {
                db.tblInvoices.Add(tblInvoice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PaymentModeID = new SelectList(db.tblPaymentModes, "PaymentModeID", "PaymentMode", tblInvoice.PaymentModeID);
            ViewBag.CreatedBy = new SelectList(db.tblUsers, "Id", "UserName", tblInvoice.CreatedBy);
            ViewBag.UpdatedBy = new SelectList(db.tblUsers, "Id", "UserName", tblInvoice.UpdatedBy);
            return View(tblInvoice);
        }

        // GET: Invoices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblInvoice tblInvoice = db.tblInvoices.Find(id);
            if (tblInvoice == null)
            {
                return HttpNotFound();
            }
            ViewBag.PaymentModeID = new SelectList(db.tblPaymentModes, "PaymentModeID", "PaymentMode", tblInvoice.PaymentModeID);
            ViewBag.CreatedBy = new SelectList(db.tblUsers, "Id", "UserName", tblInvoice.CreatedBy);
            ViewBag.UpdatedBy = new SelectList(db.tblUsers, "Id", "UserName", tblInvoice.UpdatedBy);
            return View(tblInvoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InvoiceID,InvoiceNo,BillingAddress,ShippingAddress,InvoiceType,GSTIN,CustomerName,ContactNumber,Email,Website,PaymentModeID,IsPaid,IsOnCredit,InvoiceDate,PaymentExpectedBy,Remarks,IsActive,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy")] tblInvoice tblInvoice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblInvoice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PaymentModeID = new SelectList(db.tblPaymentModes, "PaymentModeID", "PaymentMode", tblInvoice.PaymentModeID);
            ViewBag.CreatedBy = new SelectList(db.tblUsers, "Id", "UserName", tblInvoice.CreatedBy);
            ViewBag.UpdatedBy = new SelectList(db.tblUsers, "Id", "UserName", tblInvoice.UpdatedBy);
            return View(tblInvoice);
        }

        // GET: Invoices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblInvoice tblInvoice = db.tblInvoices.Find(id);
            if (tblInvoice == null)
            {
                return HttpNotFound();
            }
            return View(tblInvoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblInvoice tblInvoice = db.tblInvoices.Find(id);
            db.tblInvoices.Remove(tblInvoice);
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
