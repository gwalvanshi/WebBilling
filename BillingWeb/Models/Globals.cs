using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingWeb.Models
{
    public static class Globals
    {
        public static tblUser TheUserSession
        {
            get
            {
                if ((HttpContext.Current.Session["UserDetails"] == null))
                {
                    HttpContext.Current.Session.Add("UserDetails", new tblUser());
                    return (tblUser)HttpContext.Current.Session["UserDetails"];
                }
                else
                {
                    return (tblUser)HttpContext.Current.Session["UserDetails"];
                }
            }
            set { HttpContext.Current.Session["UserDetails"] = value; }
        }

        public static void SignOut()
        {
            HttpContext.Current.Session["UserDetails"] = null;
            HttpContext.Current.Session.Abandon();
        }
    }
}