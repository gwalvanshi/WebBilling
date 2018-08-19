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
            FillDropdownProductCategory(null);
            FillDropdownProductSubCategory(0, null);
            FillUnit(null);
            FillSize(null,null);
            FillTax(null);

             var tblProducts = db.tblProducts.Include(t => t.tblProductCategory).Include(t => t.tblProductSubCategory).Include(t => t.tblSize).Include(t => t.tblTax).Include(t => t.tblUnit).Include(t => t.tblUser).Include(t => t.tblUser1).Where(a=>a.IsActive==true);
            ViewBag.Product = new tblProduct();
            return View(tblProducts.ToList());
        }
        public JsonResult GetSubCategories(int Id)
        {
            var productSubCategories = from s in db.tblProductSubCategories
                                       where s.ProductCategoryID == Id
                                       select s;
            return Json(new SelectList(productSubCategories.ToArray(), "ProductSubCategoryID", "SubCategoryName"), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSize(int Id)
        {
            var size = from s in db.tblSizes
                       where s.UnitID == Id
                       select s;
            return Json(new SelectList(size.ToArray(), "SizeID", "SizeName"), JsonRequestBehavior.AllowGet);
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
            FillDropdownProductCategory(null);
            FillDropdownProductSubCategory(0, null);
            FillUnit(null);
            FillSize(null, null);
            FillTax(null);
            return View();
        }

        public void FillDropdown()
        {
            ViewBag.ProductCategoryID = new SelectList(db.tblProductCategories, "ProductCategoryID", "CategoryName");
            ViewBag.ProductSubCategoryID = new SelectList(db.tblProductSubCategories, "ProductSubCategoryID", "SubCategoryName");
            ViewBag.SizeID = new SelectList(db.tblSizes, "SizeID", "SizeName");
            ViewBag.TaxID = new SelectList(db.tblTaxes, "TaxID", "TaxName");
            ViewBag.UnitID = new SelectList(db.tblUnits, "UnitID", "Name");
         
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
                tblUser objSource = (tblUser)Session["UserDetails"];
                tblProduct.IsActive = true;
                tblProduct.CreatedOn = DateTime.Now;
                tblProduct.CreatedBy = objSource.Id;
                db.tblProducts.Add(tblProduct);
                db.SaveChanges();
                TempData["Success"] = "Product created successfuly.";
                ViewBag.Product = new tblProduct();
                return RedirectToAction("Index");
                
            }       

            FillDropdownProductCategory(tblProduct.ProductCategoryID);
            FillDropdownProductSubCategory(Convert.ToInt32(tblProduct.ProductCategoryID), tblProduct.ProductSubCategoryID);
            FillUnit(tblProduct.UnitID);
            FillSize(tblProduct.UnitID, tblProduct.SizeID);
            FillTax(tblProduct.TaxID);
            return View(tblProduct);
        }

        public void FillDropdownProductCategory(int ? ProductCategoryID)
        {       
            var list = new SelectList(db.tblProductCategories.ToList(), "ProductCategoryID", "CategoryName", ProductCategoryID);
            ViewData["ProductCategoryID"] = list;
        }
        public void FillDropdownProductSubCategory(int ProductCategoryID,int ? ProductSubCategoryID)
        {
            var items = db.tblProductSubCategories.Where(a => a.ProductCategoryID == ProductCategoryID).ToList();
            var list = new SelectList(items, "ProductSubCategoryID", "SubCategoryName", ProductSubCategoryID);
            ViewData["ProductSubCategoryID"] = list;
        }
        public void FillUnit(int ? UnitID)
        {
            var items = db.tblUnits.ToList();
            var list = new SelectList(items, "UnitID", "Name", UnitID);
            ViewData["UnitID"] = list;
        }
        public void FillSize(int ? UnitID,int ? SizeID)
        {
            var items = db.tblSizes.Where(a=>a.UnitID== UnitID).ToList();
            var list = new SelectList(items, "SizeID", "SizeName", SizeID);
            ViewData["SizeID"] = list;
        }
        public void FillTax(int? TaxID)
        {
            var items = db.tblTaxes.ToList();
            var list = new SelectList(items, "TaxID", "TaxName", TaxID);
            ViewData["TaxID"] = list;
        }
        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
           // FillDropdown();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProduct tblProduct = db.tblProducts.Find(id);
            if (tblProduct == null)
            {
                return HttpNotFound();
            }
            ViewBag.Product = tblProduct;

            FillDropdownProductCategory(tblProduct.ProductCategoryID);
            FillDropdownProductSubCategory(Convert.ToInt32(tblProduct.ProductCategoryID), tblProduct.ProductSubCategoryID);
            FillUnit(tblProduct.UnitID);
            FillSize(tblProduct.UnitID, tblProduct.SizeID);
            FillTax(tblProduct.TaxID);

            var tblProducts = db.tblProducts.Include(t => t.tblProductCategory).Include(t => t.tblProductSubCategory).Include(t => t.tblSize).Include(t => t.tblTax).Include(t => t.tblUnit).Include(t => t.tblUser).Include(t => t.tblUser1).Where(a => a.IsActive == true); 

            return View("Index", tblProducts.ToList());
            //return View(tblProduct);
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
                tblUser objSource = (tblUser)Session["UserDetails"];
                tblProduct.UpdatedBy = objSource.Id;
                tblProduct.UpdatedOn = DateTime.Now;

                db.Entry(tblProduct).State = EntityState.Modified;
                ViewBag.Product = new tblProduct();
                TempData["Success"] = "Product updated successfully.";
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            FillDropdownProductCategory(tblProduct.ProductCategoryID);
            FillDropdownProductSubCategory(Convert.ToInt32(tblProduct.ProductCategoryID), tblProduct.ProductSubCategoryID);
            FillUnit(tblProduct.UnitID);
            FillSize(tblProduct.UnitID, tblProduct.SizeID);
            FillTax(tblProduct.TaxID);

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
            tblUser objSource = (tblUser)Session["UserDetails"];
            if (tblProduct == null)
            {
                return HttpNotFound();
            }
            else
            {
                tblProduct.IsActive = false;
                tblProduct.UpdatedBy = objSource.Id;
                tblProduct.UpdatedOn = DateTime.Now;
                db.Entry(tblProduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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
