using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BillingWeb.Models
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        // Entities context = new Entities(); // my entity  
        private readonly string[] allowedroles;
        public CustomAuthorizeAttribute(params string[] roles)
        {
            this.allowedroles = roles;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;

            tblUser up = (tblUser)Globals.TheUserSession;
            if (up.Id > 0)
            {
                foreach (var role in allowedroles)
                {
                    if (up.RoleId == Convert.ToInt32(role))
                    {
                        authorize = true;
                    }
                }
            }
            else
            {
                Globals.TheUserSession = null;
            }
            return authorize;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("~/Login/Index");
        }
    }
}