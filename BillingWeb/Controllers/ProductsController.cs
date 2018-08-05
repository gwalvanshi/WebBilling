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
    public class ProductsController : Controller
    {
        private Billing4Entities db = new Billing4Entities();

        // GET: Products
        public ActionResult Index()
        {
            var tblProducts = db.tblProducts.Include(t => t.tblProductCategory).Include(t => t.tblProductSubCategory).Include(t => t.tblSize).Include(t => t.tblTax).Include(t => t.tblUnit).Include(t => t.tblUser).Include(t => t.tblUser1);
            return View(tblProducts.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProduct tblProduct = db.tblProducts.Find(id);
            if (tblProduct == null)
            {
                return HttpNotFound();
            }
            return View(tblProduct);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.ProductCategoryID = new SelectList(db.tblProductCategories, "ProductCategoryID", "CategoryName");
            ViewBag.ProductSubCategoryID = new SelectList(db.tblProductSubCategories, "ProductSubCategoryID", "SubCategoryName");
            ViewBag.SizeID = new SelectList(db.tblSizes, "SizeID", "SizeName");
            ViewBag.TaxID = new SelectList(db.tblTaxes, "TaxID", "TaxName");
            ViewBag.UnitID = new SelectList(db.tblUnits, "UnitID", "Name");
            ViewBag.CreatedBy = new SelectList(db.tblUsers, "Id", "UserName");
            ViewBag.UpdatedBy = new SelectList(db.tblUsers, "Id", "UserName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,ProductCategoryID,ProductSubCategoryID,ProductName,ProductDescription,Make,TaxID,SizeID,RatePerUnit,Discount,Remark,IsActive,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy,UnitID,SGST,CGST")] tblProduct tblProduct)
        {
            if (ModelState.IsValid)
            {
                db.tblProducts.Add(tblProduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductCategoryID = new SelectList(db.tblProductCategories, "ProductCategoryID", "CategoryName", tblProduct.ProductCategoryID);
            ViewBag.ProductSubCategoryID = new SelectList(db.tblProductSubCategories, "ProductSubCategoryID", "SubCategoryName", tblProduct.ProductSubCategoryID);
            ViewBag.SizeID = new SelectList(db.tblSizes, "SizeID", "SizeName", tblProduct.SizeID);
            ViewBag.TaxID = new SelectList(db.tblTaxes, "TaxID", "TaxName", tblProduct.TaxID);
            ViewBag.UnitID = new SelectList(db.tblUnits, "UnitID", "Name", tblProduct.UnitID);
            ViewBag.CreatedBy = new SelectList(db.tblUsers, "Id", "UserName", tblProduct.CreatedBy);
            ViewBag.UpdatedBy = new SelectList(db.tblUsers, "Id", "UserName", tblProduct.UpdatedBy);
            return View(tblProduct);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProduct tblProduct = db.tblProducts.Find(id);
            if (tblProduct == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductCategoryID = new SelectList(db.tblProductCategories, "ProductCategoryID", "CategoryName", tblProduct.ProductCategoryID);
            ViewBag.ProductSubCategoryID = new SelectList(db.tblProductSubCategories, "ProductSubCategoryID", "SubCategoryName", tblProduct.ProductSubCategoryID);
            ViewBag.SizeID = new SelectList(db.tblSizes, "SizeID", "SizeName", tblProduct.SizeID);
            ViewBag.TaxID = new SelectList(db.tblTaxes, "TaxID", "TaxName", tblProduct.TaxID);
            ViewBag.UnitID = new SelectList(db.tblUnits, "UnitID", "Name", tblProduct.UnitID);
            ViewBag.CreatedBy = new SelectList(db.tblUsers, "Id", "UserName", tblProduct.CreatedBy);
            ViewBag.UpdatedBy = new SelectList(db.tblUsers, "Id", "UserName", tblProduct.UpdatedBy);
            return View(tblProduct);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,ProductCategoryID,ProductSubCategoryID,ProductName,ProductDescription,Make,TaxID,SizeID,RatePerUnit,Discount,Remark,IsActive,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy,UnitID,SGST,CGST")] tblProduct tblProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblProduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductCategoryID = new SelectList(db.tblProductCategories, "ProductCategoryID", "CategoryName", tblProduct.ProductCategoryID);
            ViewBag.ProductSubCategoryID = new SelectList(db.tblProductSubCategories, "ProductSubCategoryID", "SubCategoryName", tblProduct.ProductSubCategoryID);
            ViewBag.SizeID = new SelectList(db.tblSizes, "SizeID", "SizeName", tblProduct.SizeID);
            ViewBag.TaxID = new SelectList(db.tblTaxes, "TaxID", "TaxName", tblProduct.TaxID);
            ViewBag.UnitID = new SelectList(db.tblUnits, "UnitID", "Name", tblProduct.UnitID);
            ViewBag.CreatedBy = new SelectList(db.tblUsers, "Id", "UserName", tblProduct.CreatedBy);
            ViewBag.UpdatedBy = new SelectList(db.tblUsers, "Id", "UserName", tblProduct.UpdatedBy);
            return View(tblProduct);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProduct tblProduct = db.tblProducts.Find(id);
            if (tblProduct == null)
            {
                return HttpNotFound();
            }
            return View(tblProduct);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblProduct tblProduct = db.tblProducts.Find(id);
            db.tblProducts.Remove(tblProduct);
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
