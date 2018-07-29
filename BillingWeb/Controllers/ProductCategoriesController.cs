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
    public class ProductCategoriesController : Controller
    {
        private Billing4Entities db = new Billing4Entities();

        // GET: ProductCategories
        public ActionResult Index()
        {
            ViewBag.ProductCategories = new tblProductCategory();
            return View(db.tblProductCategories.ToList().Where(a=>a.IsActive==true).ToList());
        }

        // GET: ProductCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductCategoryID,CategoryName,Description,HSN_SAC,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy,IsActive")] tblProductCategory tblProductCategory)
        {
            if (ModelState.IsValid)
            {
                tblUser objSource = (tblUser)Session["UserDetails"];
                tblProductCategory.CreatedBy = objSource.Id;
                tblProductCategory.CreatedOn = DateTime.Now;
                tblProductCategory.IsActive = true; 
                db.tblProductCategories.Add(tblProductCategory);
                db.SaveChanges();
                TempData["Success"] = "Product Category added successfully.";
                ViewBag.Tax = new tblProductCategory();
                return RedirectToAction("Index");
            }

            ViewBag.Tax = new tblProductCategory();
            var tblProductCategory1 = db.tblProductCategories.ToList().Where(a => a.IsActive == true).ToList();
            return View("Index", tblProductCategory);
        }

        // GET: ProductCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProductCategory tblProductCategory = db.tblProductCategories.Find(id);
            if (tblProductCategory == null)
            {
                return HttpNotFound();
            }
            return View(tblProductCategory);
        }

        // POST: ProductCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductCategoryID,CategoryName,Description,HSN_SAC,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy,IsActive")] tblProductCategory tblProductCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblProductCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblProductCategory);
        }

        // GET: ProductCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProductCategory tblProductCategory = db.tblProductCategories.Find(id);
            if (tblProductCategory == null)
            {
                return HttpNotFound();
            }
            return View(tblProductCategory);
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
