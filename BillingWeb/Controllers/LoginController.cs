using BillingWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BillingWeb.Controllers
{
    public class LoginController : Controller
    {
        private Billing4Entities db = new Billing4Entities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Welcome()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult LogOff()
        {
            Globals.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
        [HttpPost]
        public ActionResult loginSubmit(string userID, string password)
        {
            string msg = string.Empty;
            string controllerName = string.Empty;
            string actionName = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(userID.Trim()) && !string.IsNullOrEmpty(password.Trim()))
                {
                    tblUser objUserDetails = db.tblUsers.Where(a => a.UserName == userID && a.Password == password&&a.IsActive==true).FirstOrDefault();

                    if (objUserDetails != null)
                    {
                        Globals.TheUserSession = objUserDetails;

                        if (objUserDetails.RoleId == 1)
                        {
                            //Admin
                            controllerName = "Login";
                            actionName = "Welcome";
                        }
                        else
                        {
                            //User
                            controllerName = "Login";
                            actionName = "Welcome";
                        }
                        return Json(new
                        {
                            redirectUrl = Url.Action(actionName, controllerName),
                            isRedirect = true,
                            Message = ""
                        });
                    }
                    else
                    {
                        msg = "Invalid user login.Please provide valid login details.";
                    }
                }
                else
                {
                    msg = "Please provide valid login details.";
                }
                return Json(new
                {
                    Message = msg
                });               
            }
            catch (Exception ex)
            {

                return Json(new
                {
                    Message = ex.Message
                });
            }

        }
    }
}