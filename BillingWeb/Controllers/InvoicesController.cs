using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BillingWeb;
using System.Configuration;
using System.Text;

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
           
            Guid hg = Guid.NewGuid();
            string uni = hg.ToString().Substring(0, 8);            

            ViewBag.PaymentModeID = new SelectList(db.tblPaymentModes, "PaymentModeID", "PaymentMode");
            tblInvoice tblInvoice = new tblInvoice();
            tblInvoice.InvoiceNo = ConfigurationManager.AppSettings["InvoicePrefix"]+"00"+ uni.ToUpper();
            ViewBag.ProductID = new SelectList(db.tblProducts, "ProductID", "ProductName");
            ViewBag.SizeID = new SelectList(db.tblSizes.Where(a=>a.UnitID==null), "SizeID", "SizeName");
            ViewBag.UnitID = new SelectList(db.tblUnits, "UnitID", "Name", tblInvoice.UnitID);
            return View(tblInvoice);
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tblInvoice tblInvoice, List<tblInvoiceItem> invItem, string submit)
        {

            List<tblInvoiceItem> tblItem = new List<tblInvoiceItem>();
            tblInvoiceItem objtblInvoiceItem = new tblInvoiceItem();
            tblUser objSource = (tblUser)Session["UserDetails"];
            if (submit == "Add Row")
            {
                tblProduct objpro = db.tblProducts.Where(p => p.ProductID == tblInvoice.ProductID).FirstOrDefault();
                tblUnit objUnit = db.tblUnits.Where(t => t.UnitID == tblInvoice.UnitID).FirstOrDefault();
                tblSize objSize = db.tblSizes.Where(t => t.SizeID == tblInvoice.SizeID).FirstOrDefault();
                objtblInvoiceItem.ProductID = tblInvoice.ProductID;
                objtblInvoiceItem.ProductName = objpro.ProductName;
                objtblInvoiceItem.SizeID = tblInvoice.SizeID;
                objtblInvoiceItem.UnitID = tblInvoice.UnitID;
                objtblInvoiceItem.TaxID = tblInvoice.TaxID;
                objtblInvoiceItem.Tax = tblInvoice.Tax;
                objtblInvoiceItem.TaxAmount = tblInvoice.TaxAmount;
                objtblInvoiceItem.Quantity = tblInvoice.Quantity;
                objtblInvoiceItem.RatePerUnit = tblInvoice.RatePerUnit;
                objtblInvoiceItem.IsDeleted = false;
                objtblInvoiceItem.UnitName = objUnit.Name;
                objtblInvoiceItem.SizeName = objSize.SizeName;
                objtblInvoiceItem.HSN_SAC = tblInvoice.HSN_SAC;
                objtblInvoiceItem.Discount = tblInvoice.Discount;
                objtblInvoiceItem.DiscountAmount = tblInvoice.DiscountAmount;
                objtblInvoiceItem.SGST = tblInvoice.SGST;
                objtblInvoiceItem.Make = tblInvoice.Make;
                if (invItem == null)
                {             
                    tblItem.Add(objtblInvoiceItem);
                    tblInvoice.tblInvoiceItems = tblItem;
                }
                else
                {
                    invItem.Add(objtblInvoiceItem);
                    tblInvoice.tblInvoiceItems = invItem;
                }
            }
            if (submit == "Delete Row")
            {
                ModelState.Clear();
                invItem.RemoveAll(a=>a.IsDeleted==true);               
                tblInvoice.tblInvoiceItems = invItem;
            }
            if (submit == "Save Invoice")
            {
                GetInvoiceDetails(tblInvoice, invItem, objSource.Id);
            }
            if (submit == "Print Preview")
            {

            }
            if (submit == "Print")
            {

            }           

                //if (ModelState.IsValid)
                //{               
                //    db.tblInvoices.Add(tblInvoice);
                //    db.SaveChanges();
                //    return RedirectToAction("Index");
                //}

            ViewBag.PaymentModeID = new SelectList(db.tblPaymentModes, "PaymentModeID", "PaymentMode", tblInvoice.PaymentModeID);
            ViewBag.ProductID = new SelectList(db.tblProducts, "ProductID", "ProductName");
            ViewBag.SizeID = new SelectList(db.tblSizes.Where(a => a.UnitID == tblInvoice.UnitID), "SizeID", "SizeName", tblInvoice.SizeID);
            ViewBag.UnitID = new SelectList(db.tblUnits, "UnitID", "Name", tblInvoice.UnitID);
            // ViewBag.CreatedBy = new SelectList(db.tblUsers, "Id", "UserName", tblInvoice.CreatedBy);
            // ViewBag.UpdatedBy = new SelectList(db.tblUsers, "Id", "UserName", tblInvoice.UpdatedBy);
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
        [HttpPost]
        public JsonResult BindSize(string unitId)
        {
            int pId = Convert.ToInt32(unitId);
            var location = (from loc in db.tblSizes
                            where loc.UnitID == pId
                            select new
                            {
                                label = loc.SizeName,
                                val = loc.SizeID
                            }).ToList();
            return Json(location);
        }
        [HttpPost]
        public JsonResult GetProductDetails(string prodID)
        {
            int pId = Convert.ToInt32(prodID);
            // var prod = db.tblProducts.Where(p => p.ProductID == pId).ToList();

            var prod = (from loc in db.tblProducts
                        where loc.ProductID == pId
                        select new
                        {
                           loc.TaxID,
                           loc.ProductName,
                           loc.SGST,
                           loc.tblTax.TaxPercentage
                        }).FirstOrDefault();
            return Json(prod);
        }

        public bool GetInvoiceDetails(tblInvoice objtblInvoice, IList<tblInvoiceItem> tblInvoiceItemList, int userID)
        {
            bool isValid = false;
            try
            {
                DataTable dtInvoice = new DataTable();
                dtInvoice.TableName = "Invoice";
                dtInvoice.Columns.Add("CustomerName", typeof(String));
                dtInvoice.Columns.Add("ContactNumber", typeof(String));
                dtInvoice.Columns.Add("Email", typeof(String));
                dtInvoice.Columns.Add("Website", typeof(String));
                dtInvoice.Columns.Add("PaymentModeID", typeof(Int32));
                dtInvoice.Columns.Add("IsPaid", typeof(Int32));
                dtInvoice.Columns.Add("IsOnCredit", typeof(Int32));
                dtInvoice.Columns.Add("InvoiceDate", typeof(String));
                dtInvoice.Columns.Add("PaymentExpectedBy", typeof(String));
                dtInvoice.Columns.Add("Remarks", typeof(String));
                dtInvoice.Columns.Add("InvoiceNo", typeof(String));
                dtInvoice.Columns.Add("BillingAddress", typeof(String));
                dtInvoice.Columns.Add("ShippingAddress", typeof(String));
                dtInvoice.Columns.Add("InvoiceType", typeof(Int32));
                dtInvoice.Columns.Add("GSTIN", typeof(String));

                DataRow rowInvoice = dtInvoice.NewRow();


                rowInvoice["CustomerName"] = objtblInvoice.CustomerName.Trim();
                rowInvoice["ContactNumber"] = objtblInvoice.ContactNumber.Trim();
                rowInvoice["Email"] = objtblInvoice.Email.Trim();
                rowInvoice["Website"] = objtblInvoice.Website.Trim();
                rowInvoice["PaymentModeID"] = Convert.ToInt32(objtblInvoice.PaymentModeID);
                rowInvoice["IsPaid"] = objtblInvoice.IsPaid;
                rowInvoice["IsOnCredit"] = objtblInvoice.IsOnCredit;
                rowInvoice["InvoiceDate"] = objtblInvoice.InvoiceDate;
                rowInvoice["PaymentExpectedBy"] = objtblInvoice.PaymentExpectedBy;
                rowInvoice["Remarks"] = objtblInvoice.Remarks;
                rowInvoice["InvoiceNo"] = objtblInvoice.InvoiceNo;
                rowInvoice["BillingAddress"] = objtblInvoice.BillingAddress.Trim();
                rowInvoice["ShippingAddress"] = objtblInvoice.ShippingAddress.Trim();
                rowInvoice["InvoiceType"] = objtblInvoice.InvoiceType;
                rowInvoice["GSTIN"] = objtblInvoice.GSTIN.Trim();

                dtInvoice.Rows.Add(rowInvoice);

                DataTable dt1 = new DataTable();
                dt1.TableName = "InvoiceDetails";
                dt1.Columns.Add("InvoiceItemID", typeof(Int32));
                dt1.Columns.Add("InvoiceID", typeof(Int32));
                dt1.Columns.Add("ProductID", typeof(Int32));
                dt1.Columns.Add("Product", typeof(String));
                dt1.Columns.Add("Make", typeof(String));
                dt1.Columns.Add("Quantity", typeof(Decimal));
                dt1.Columns.Add("UnitID", typeof(String));
                dt1.Columns.Add("SizeId", typeof(Int32));
                dt1.Columns.Add("Size", typeof(String));
                dt1.Columns.Add("RatePerUnit", typeof(Decimal));
                dt1.Columns.Add("TaxID", typeof(Int32));
                dt1.Columns.Add("HSN_SAC", typeof(Int32));
                dt1.Columns.Add("TaxPercentage", typeof(Int32));
                dt1.Columns.Add("TaxAmount", typeof(Decimal));
                dt1.Columns.Add("Discount", typeof(Decimal));
                dt1.Columns.Add("DiscountAmount", typeof(Decimal));
                dt1.Columns.Add("TotalAmount", typeof(Decimal));
                dt1.Columns.Add("Remark", typeof(String));
                dt1.Columns.Add("Delete", typeof(Boolean));
                DataRow row;
                foreach (var item in tblInvoiceItemList)
                {
                    row = dt1.NewRow();
                    row["InvoiceItemID"] = item.InvoiceItemID;
                    row["InvoiceID"] = objtblInvoice.InvoiceID;
                    row["ProductID"] = item.ProductID;
                    row["Product"] = item.ProductName;
                    row["Make"] = item.Make;
                    row["Quantity"] = item.Quantity;
                    row["UnitID"] = item.UnitID;
                    row["TaxID"] = item.TaxID;
                    row["SizeId"] = item.SizeID;
                    row["RatePerUnit"] = item.RatePerUnit;
                    row["HSN_SAC"] = item.HSN_SAC;
                    row["TaxPercentage"] = item.Tax;
                    row["Discount"] = item.Discount;
                    row["TaxAmount"] = item.TaxAmount;
                    row["DiscountAmount"] = item.DiscountAmount;
                    row["TotalAmount"] = item.TotalAmount;
                    row["Remark"] = item.Remark;
                    row["Delete"] = item.IsDeleted;
                    dt1.Rows.Add(row);

                }
                System.IO.StringWriter swSQL;
                DataTable dtInvoiceItems = dt1;
                StringBuilder sbSQL1 = new StringBuilder();
                swSQL = new System.IO.StringWriter(sbSQL1);
                dtInvoiceItems.WriteXml(swSQL);
                swSQL.Dispose();

                System.IO.StringWriter swSQL2;

                StringBuilder sbSQL2 = new StringBuilder();
                swSQL2 = new System.IO.StringWriter(sbSQL2);
                dtInvoice.WriteXml(swSQL2);
                swSQL2.Dispose();
                db.spInvoice_InvoiceEntrySave(sbSQL2.ToString(), sbSQL1.ToString(), userID, objtblInvoice.InvoiceID);
                isValid = true;
            }
            catch (Exception)
            {
                isValid = false;
            }
            return isValid;
        }
    }
}
