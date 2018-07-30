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
    public class SizesController : Controller
    {
        private Billing4Entities db = new Billing4Entities();

        // GET: Sizes
        public ActionResult Index()
        {
            ViewBag.UnitID = new SelectList(db.tblUnits, "UnitID", "Name");
            var tblSizes = db.tblSizes.Include(t => t.tblUnit).Include(t => t.tblUser).Include(t => t.tblUser1);
            ViewBag.Size = new tblSize();
            return View(tblSizes.ToList());
        }

        // GET: Sizes/Create
        public ActionResult Create()
        {
            //tblSize tblSize = new tblSize();
            //tblUser objSource = (tblUser)Session["UserDetails"];
            //ViewBag.UnitID = new SelectList(db.tblUnits, "UnitID", "Name");
            //tblSize.CreatedBy = objSource.Id;
            //tblSize.UpdatedBy = objSource.Id;
            return View();

        }

        // POST: Sizes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SizeID,SizeName,SizeDescription,UnitID,IsActive,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy")] tblSize tblSize)
        {
            if (ModelState.IsValid)
            {
                tblUser objSource = (tblUser)Session["UserDetails"];
                tblSize.IsActive = true;
                tblSize.CreatedOn = DateTime.Now;
                tblSize.CreatedBy = objSource.Id;
                db.tblSizes.Add(tblSize);
                db.SaveChanges();
                ViewBag.Size = new tblSize();
                return RedirectToAction("Index");
            }
            ViewBag.Size = new tblSize();
            var tblSizes = db.tblSizes.Include(t => t.tblUser).Include(t => t.tblUser1).Where(a => a.IsActive == true);
            return View("Index", tblSizes.ToList());
        }

        // GET: Sizes/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.UnitID = new SelectList(db.tblUnits, "UnitID", "Name");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSize tblSize = db.tblSizes.Find(id);
            if (tblSize == null)
            {
                return HttpNotFound();
            }
            ViewBag.Size = tblSize;
            var tblSizes = db.tblSizes.Include(t => t.tblUser).Include(t => t.tblUser1).Where(a => a.IsActive == true);
            return View("Index", tblSizes.ToList());
        }

        // POST: Sizes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SizeID,SizeName,SizeDescription,UnitID,IsActive,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy")] tblSize tblSize)
        {
            if (ModelState.IsValid)
            {
                ViewBag.UnitID = new SelectList(db.tblUnits, "UnitID", "Name");
                tblUser objSource = (tblUser)Session["UserDetails"];
                tblSize.UpdatedOn = DateTime.Now;
                tblSize.UpdatedBy = objSource.Id;
                db.Entry(tblSize).State = EntityState.Modified;
                ViewBag.Size = new tblSize();
                TempData["Success"] = "Size updated successfully.";
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            ViewBag.Size = new tblSize();
            var tblSizes = db.tblSizes.Include(t => t.tblUser).Include(t => t.tblUser1).Where(a => a.IsActive == true);
            return View("Index", tblSizes.ToList());

        }

        // GET: Sizes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSize tblSize = db.tblSizes.Find(id);
            if (tblSize == null)
            {
                return HttpNotFound();
            }
            return View(tblSize);
        }

        // POST: Sizes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblSize tblSize = db.tblSizes.Find(id);
            db.tblSizes.Remove(tblSize);
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
