using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BillingWeb;
using BillingWeb.Models;

namespace BillingWeb.Controllers
{
    [CustomAuthorize("1")]
    public class TaxesController : Controller
    {
        private Billing4Entities db = new Billing4Entities();

        // GET: Taxes
        public ActionResult Index()
        {
            var tblTaxes = db.tblTaxes.Include(t => t.tblUser).Include(t => t.tblUser1).Where(a=>a.IsActive==true);
            ViewBag.Tax = new tblTax();
            return View(tblTaxes.ToList());
        }      

        // GET: Taxes/Create
        public ActionResult Create()
        {        
            return View();
        }

        // POST: Taxes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TaxID,TaxName,TaxPercentage,EffectiveFrom,Description,IsActive,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy")] tblTax tblTax)
        {
            if (ModelState.IsValid)
            {
                tblUser objSource = (tblUser)Session["UserDetails"];
                tblTax.CreatedBy = objSource.Id;
                tblTax.CreatedOn = DateTime.Now;
                tblTax.IsActive = true;
                db.tblTaxes.Add(tblTax);
                db.SaveChanges();
                TempData["Success"] = "Tax added successfully.";
                ViewBag.Tax = new tblTax();
                return RedirectToAction("Index");
            }
            ViewBag.Tax = new tblTax();
            var tblTaxes = db.tblTaxes.Include(t => t.tblUser).Include(t => t.tblUser1).Where(a => a.IsActive == true);
            return View("Index", tblTaxes.ToList());
        }

        // GET: Taxes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblTax tblTax = db.tblTaxes.Find(id);
            if (tblTax == null)
            {
                return HttpNotFound();
            }
            ViewBag.Tax = tblTax;
            var tblTaxes = db.tblTaxes.Include(t => t.tblUser).Include(t => t.tblUser1).Where(a => a.IsActive == true);
            return View("Index", tblTaxes.ToList());
        }

        // POST: Taxes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TaxID,TaxName,TaxPercentage,EffectiveFrom,Description,IsActive,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy")] tblTax tblTax)
        {
            if (ModelState.IsValid)
            {
                tblUser objSource = (tblUser)Session["UserDetails"];
                tblTax.UpdatedBy = objSource.Id;
                tblTax.UpdatedOn = DateTime.Now;
              
                db.Entry(tblTax).State = EntityState.Modified;
                ViewBag.Tax = new tblTax();
                TempData["Success"] = "Tax updated successfully.";
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Tax = new tblTax();
            var tblTaxes = db.tblTaxes.Include(t => t.tblUser).Include(t => t.tblUser1).Where(a => a.IsActive == true);
            return View("Index", tblTaxes.ToList());
        }

        // GET: Taxes/Delete/5
        public ActionResult Delete(int? id)
        {
            tblTax tblUser = db.tblTaxes.Find(id);
            tblUser objSource = (tblUser)Session["UserDetails"];
            if (tblUser == null)
            {
                return HttpNotFound();
            }
            else
            {
                tblUser.IsActive = false;
                tblUser.UpdatedBy = objSource.Id;
                tblUser.UpdatedOn = DateTime.Now;
                db.Entry(tblUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
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
