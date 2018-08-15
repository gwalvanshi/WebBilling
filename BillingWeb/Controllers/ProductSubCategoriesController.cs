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
    public class ProductSubCategoriesController : Controller
    {
        private Billing4Entities db = new Billing4Entities();

        // GET: ProductSubCategories
        public ActionResult Index()
        {
            var tblProductSubCategories = db.tblProductSubCategories.Include(t => t.tblProductCategory).Where(a => a.IsActive == true);
            ViewBag.ProductCategoryID = new SelectList(db.tblProductCategories, "ProductCategoryID", "CategoryName");
            ViewBag.SubCategory = new tblProductSubCategory();
            return View(tblProductSubCategories.ToList());
        }
        
        // GET: ProductSubCategories/Create
        public ActionResult Create()
        {
            //ViewBag.ProductCategoryID = new tblProductCategory();
            ViewBag.ProductCategoryID = new SelectList(db.tblProductCategories, "ProductCategoryID", "CategoryName");
            //ViewBag.ProductCategoryID = new SelectList(db.tblProductCategories, "ProductCategoryID", "CategoryName");


            return View();

        }

        // POST: ProductSubCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductSubCategoryID,ProductCategoryID,SubCategoryName,Description,IsActive,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy")] tblProductSubCategory tblProductSubCategory)
        {
            ViewBag.ProductCategoryID = new SelectList(db.tblProductCategories, "ProductCategoryID", "CategoryName");
            if (ModelState.IsValid)
            {
                tblUser objSource = (tblUser)Session["UserDetails"];
                tblProductSubCategory.CreatedBy = objSource.Id;
                tblProductSubCategory.CreatedOn = DateTime.Now;
                tblProductSubCategory.IsActive = true;
                db.tblProductSubCategories.Add(tblProductSubCategory);
                db.SaveChanges();
                TempData["Success"] = "Sub Category added successfully.";
                ViewBag.SubCategory = new tblProductSubCategory();
                return RedirectToAction("Index");
            }
            ViewBag.SubCategory = new tblProductSubCategory();
            var tblProductSubCategories = db.tblProductSubCategories.Include(t => t.tblProductCategory);
            return View("Index", tblProductSubCategories.ToList());
            
        }

        // GET: ProductSubCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProductSubCategory tblProductSubCategory = db.tblProductSubCategories.Find(id);
            if (tblProductSubCategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductCategoryID = new SelectList(db.tblProductCategories, "ProductCategoryID", "CategoryName", tblProductSubCategory.ProductCategoryID);
            ViewBag.SubCategory = tblProductSubCategory;
            var tblProductSubCategories = db.tblProductSubCategories.Include(t => t.tblProductCategory);
            return View("Index", tblProductSubCategories.ToList());

            //ViewBag.ProductCategoryID = new SelectList(db.tblProductCategories, "ProductCategoryID", "CategoryName", tblProductSubCategory.ProductCategoryID);
            //return View(tblProductSubCategory);
        }

        // POST: ProductSubCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductSubCategoryID,ProductCategoryID,SubCategoryName,Description,IsActive,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy")] tblProductSubCategory tblProductSubCategory)
        {
            if (ModelState.IsValid)
            {
                tblUser objSource = (tblUser)Session["UserDetails"];
                tblProductSubCategory.UpdatedBy = objSource.Id;
                tblProductSubCategory.UpdatedOn = DateTime.Now;

                db.Entry(tblProductSubCategory).State = EntityState.Modified;
                ViewBag.SubCategory = new tblProductSubCategory();
                TempData["Success"] = "Sub Category updated successfully.";
                db.SaveChanges();
                return RedirectToAction("Index");
                
            }
            ViewBag.ProductCategoryID = new SelectList(db.tblProductCategories, "ProductCategoryID", "CategoryName", tblProductSubCategory.ProductCategoryID);
            ViewBag.SubCategory = new tblProductSubCategory();
            var tblProductSubCategories = db.tblProductSubCategories.Include(t => t.tblProductCategory);
            return View("Index", tblProductSubCategories.ToList());
            
            //return View(tblProductSubCategory);
        }

        // GET: ProductSubCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProductSubCategory tblProductSubCategory = db.tblProductSubCategories.Find(id);
            tblUser objSource = (tblUser)Session["UserDetails"];
            if (tblProductSubCategory == null)
            {
                return HttpNotFound();
            }
            else
            {
                tblProductSubCategory.IsActive = false;
                tblProductSubCategory.UpdatedBy = objSource.Id;
                tblProductSubCategory.UpdatedOn = DateTime.Now;
                db.Entry(tblProductSubCategory).State = EntityState.Modified;
                db.SaveChanges();
                var tblProductSubCategories = db.tblProductSubCategories.Include(t => t.tblProductCategory);
                return RedirectToAction("Index", tblProductSubCategories.ToList());
            }
            //return View(tblProductSubCategory);
        }

        // POST: ProductSubCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblProductSubCategory tblProductSubCategory = db.tblProductSubCategories.Find(id);
            db.tblProductSubCategories.Remove(tblProductSubCategory);
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
