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
    public class StocksController : Controller
    {
        private Billing4Entities db = new Billing4Entities();

        // GET: Stocks
        public ActionResult Index()
        {
            var tblStocks = db.tblStocks.Include(t => t.tblSize).Include(t => t.tblTax).Include(t => t.tblUnit).Include(t => t.tblUser).Include(t => t.tblUser1);
            return View(tblStocks.ToList());
        }

        // GET: Stocks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblStock tblStock = db.tblStocks.Find(id);
            if (tblStock == null)
            {
                return HttpNotFound();
            }
            return View(tblStock);
        }

        // GET: Stocks/Create
        public ActionResult Create()
        {
            ViewBag.SizeID = new SelectList(db.tblSizes, "SizeID", "SizeName");
            ViewBag.TaxID = new SelectList(db.tblTaxes, "TaxID", "TaxName");
            ViewBag.UnitID = new SelectList(db.tblUnits, "UnitID", "Name");
            ViewBag.CreatedBy = new SelectList(db.tblUsers, "Id", "UserName");
            ViewBag.UpdatedBy = new SelectList(db.tblUsers, "Id", "UserName");
            return View();
        }

        // POST: Stocks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StockID,ProductID,Make,Quantity,UnitID,SizeID,RatePerUnit,TaxID,Tax,TaxAmount,Discount,DiscountAmount,TotalAmount,Remark,IsActive,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy,SGST,CGST")] tblStock tblStock)
        {
            if (ModelState.IsValid)
            {
                db.tblStocks.Add(tblStock);               
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SizeID = new SelectList(db.tblSizes, "SizeID", "SizeName", tblStock.SizeID);
            ViewBag.TaxID = new SelectList(db.tblTaxes, "TaxID", "TaxName", tblStock.TaxID);
            ViewBag.UnitID = new SelectList(db.tblUnits, "UnitID", "Name", tblStock.UnitID);
            ViewBag.CreatedBy = new SelectList(db.tblUsers, "Id", "UserName", tblStock.CreatedBy);
            ViewBag.UpdatedBy = new SelectList(db.tblUsers, "Id", "UserName", tblStock.UpdatedBy);
            return View(tblStock);
        }

        // GET: Stocks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblStock tblStock = db.tblStocks.Find(id);
            if (tblStock == null)
            {
                return HttpNotFound();
            }
            ViewBag.SizeID = new SelectList(db.tblSizes, "SizeID", "SizeName", tblStock.SizeID);
            ViewBag.TaxID = new SelectList(db.tblTaxes, "TaxID", "TaxName", tblStock.TaxID);
            ViewBag.UnitID = new SelectList(db.tblUnits, "UnitID", "Name", tblStock.UnitID);
            ViewBag.CreatedBy = new SelectList(db.tblUsers, "Id", "UserName", tblStock.CreatedBy);
            ViewBag.UpdatedBy = new SelectList(db.tblUsers, "Id", "UserName", tblStock.UpdatedBy);
            return View(tblStock);
        }

        // POST: Stocks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StockID,ProductID,Make,Quantity,UnitID,SizeID,RatePerUnit,TaxID,Tax,TaxAmount,Discount,DiscountAmount,TotalAmount,Remark,IsActive,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy,SGST,CGST")] tblStock tblStock)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblStock).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SizeID = new SelectList(db.tblSizes, "SizeID", "SizeName", tblStock.SizeID);
            ViewBag.TaxID = new SelectList(db.tblTaxes, "TaxID", "TaxName", tblStock.TaxID);
            ViewBag.UnitID = new SelectList(db.tblUnits, "UnitID", "Name", tblStock.UnitID);
            ViewBag.CreatedBy = new SelectList(db.tblUsers, "Id", "UserName", tblStock.CreatedBy);
            ViewBag.UpdatedBy = new SelectList(db.tblUsers, "Id", "UserName", tblStock.UpdatedBy);
            return View(tblStock);
        }

        // GET: Stocks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblStock tblStock = db.tblStocks.Find(id);
            if (tblStock == null)
            {
                return HttpNotFound();
            }
            return View(tblStock);
        }

        // POST: Stocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblStock tblStock = db.tblStocks.Find(id);
            db.tblStocks.Remove(tblStock);
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
