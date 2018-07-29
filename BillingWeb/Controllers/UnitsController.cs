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
    public class UnitsController : Controller
    {
        private Billing4Entities db = new Billing4Entities();

        // GET: Units
        public ActionResult Index()
        {
            var tblUnits = db.tblUnits.Include(t => t.tblUser).Include(t => t.tblUser1).Where(u=>u.IsActive==true);
            ViewBag.Unit=new tblUnit();
            return View(tblUnits.ToList());
        }
        
        // GET: Units/Create
        public ActionResult Create()
        {
            tblUnit tblUnit = new tblUnit();
            tblUser objSource = (tblUser)Session["UserDetails"];
            tblUnit.CreatedBy = objSource.Id;
            tblUnit.UpdatedBy = objSource.Id;
            return View();
        }

        // POST: Units/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UnitID,Name,Description,IsActive,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy")] tblUnit tblUnit)
        {
            if (ModelState.IsValid)
            {
                tblUser objSource = (tblUser)Session["UserDetails"];
                tblUnit.Name = Request["Name"];
                tblUnit.Description = Request["Description"];
                tblUnit.IsActive = true;
                tblUnit.CreatedOn = DateTime.Now;
                tblUnit.CreatedBy = objSource.Id;
                db.tblUnits.Add(tblUnit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        // GET: Units/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblUnit tblUnit = db.tblUnits.Find(id);
            if (tblUnit == null)
            {
                return HttpNotFound();
            }
            ViewBag.Unit = tblUnit;
            var tblUnits = db.tblUnits.Include(t => t.tblUser).Include(t => t.tblUser1).Where(a => a.IsActive == true);
            return View("Index", tblUnits.ToList());
           
        }

        // POST: Units/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UnitID,Name,Description,IsActive,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy")] tblUnit tblUnit)
        {
            if (ModelState.IsValid)
            {
                tblUser objSource = (tblUser)Session["UserDetails"];
                tblUnit.UnitID = Int32.Parse(Request["UnitID"]);
                tblUnit.Name = Request["Name"];
                tblUnit.Description = Request["Description"];
                tblUnit.UpdatedOn = DateTime.Now;
                tblUnit.UpdatedBy = objSource.Id;
                db.Entry(tblUnit).State = EntityState.Modified;
                ViewBag.Unit = new tblUnit();
                TempData["Success"] = "Unit updated successfully.";
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        // GET: Units/Delete/5
        public ActionResult Delete(int? id)
        {
            tblUnit tblUnit = db.tblUnits.Find(id);
            tblUser objSource = (tblUser)Session["UserDetails"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            if (tblUnit == null)
            {
                return HttpNotFound();
            }
            else
            {
                tblUnit.IsActive = false;
                tblUnit.UpdatedBy = objSource.Id;
                tblUnit.UpdatedOn = DateTime.Now;
                db.Entry(tblUnit).State = EntityState.Modified;
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
