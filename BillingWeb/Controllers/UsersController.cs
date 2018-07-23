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
    public class UsersController : Controller
    {
        private Billing4Entities db = new Billing4Entities();

        // GET: Users
        public ActionResult Index()
        {
            var tblUsers = db.tblUsers.Include(t => t.tblRole).Where(a=>a.IsActive==true);
            ViewBag.Users = new tblUser();
            ViewBag.RoleId = new SelectList(db.tblRoles, "RoleId", "Role");
            return View(tblUsers.ToList());
        }     

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.RoleId = new SelectList(db.tblRoles, "RoleId", "Role");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserName,Password,CreatedDate,IsActive,RoleId,AddedBy")] tblUser tblUser)
        {
            if (ModelState.IsValid)
            {
                tblUser objSource = (tblUser)Session["UserDetails"];
                tblUser.AddedBy = objSource.Id;
                tblUser.IsActive = true;
                tblUser.CreatedDate = DateTime.Now;
                db.tblUsers.Add(tblUser);
                db.SaveChanges();
                ViewBag.Users = new tblUser();
                TempData["Success"] = "User added successfully.";
                return RedirectToAction("Index");
            }

            ViewBag.RoleId = new SelectList(db.tblRoles, "RoleId", "Role", tblUser.RoleId);
            return View(tblUser);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblUser tblUser = db.tblUsers.Find(id);
            if (tblUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.Users = tblUser;
            ViewBag.RoleId = new SelectList(db.tblRoles, "RoleId", "Role", tblUser.RoleId);
            var tblUsersList = db.tblUsers.Include(t => t.tblRole).Where(a=>a.IsActive==true);
            return View("Index", tblUsersList.ToList());
           // return View(tblUser);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserName,Password,CreatedDate,IsActive,RoleId,AddedBy")] tblUser tblUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblUser).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.Users = new tblUser();
                TempData["Success"] = "User updated successfully.";
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(db.tblRoles, "RoleId", "Role", tblUser.RoleId);
            return View(tblUser);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tblUser tblUser = db.tblUsers.Find(id);
            if (tblUser == null)
            {
                return HttpNotFound();
            }
            else
            {
                tblUser.IsActive = false;
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
