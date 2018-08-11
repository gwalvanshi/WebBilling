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
            var tblProductCategories = db.tblProductCategories.ToList().Where(a => a.IsActive == true).ToList();
            ViewBag.ProductCategory = new tblProductCategory();
            return View(tblProductCategories.ToList());
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
                ViewBag.ProductCategory = new tblProductCategory();
                return RedirectToAction("Index");
            }

            ViewBag.ProductCategory = new tblProductCategory();
            var tblProductCategories = db.tblProductCategories.ToList().Where(a => a.IsActive == true).ToList();
            return View("Index", tblProductCategories.ToList());
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
            ViewBag.ProductCategory = tblProductCategory;
            var tblProductCategories = db.tblProductCategories.ToList().Where(a => a.IsActive == true).ToList();
            return View("Index", tblProductCategories.ToList());
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
                
                tblUser objSource = (tblUser)Session["UserDetails"];
                tblProductCategory.UpdatedBy = objSource.Id;
                tblProductCategory.UpdatedOn = DateTime.Now;
                db.Entry(tblProductCategory).State = EntityState.Modified;
                ViewBag.ProductCategory = new tblProductCategory();
                TempData["Success"] = "Product Category updated successfully.";
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductCategory = new tblProductCategory();
            var tblProductCategories = db.tblProductCategories.ToList().Where(a => a.IsActive == true).ToList();
            return View("Index", tblProductCategories.ToList());
        }

        // GET: ProductCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblUser objSource = (tblUser)Session["UserDetails"];
            tblProductCategory tblProductCategory = db.tblProductCategories.Find(id);
            if (tblProductCategory == null)
            {
                return HttpNotFound();
            }
            else
            {
                tblProductCategory.IsActive = false;
                tblProductCategory.UpdatedBy = objSource.Id;
                tblProductCategory.UpdatedOn = DateTime.Now;
                db.Entry(tblProductCategory).State = EntityState.Modified;
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
