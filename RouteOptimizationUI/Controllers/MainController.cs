using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RouteOptimization.Infrastructure;

namespace RouteOptimizationUI.Controllers
{
    public class MainController : Controller
    {
        // GET: Main
        public ActionResult Index()
        {
            RouteOptimizationDBEntities db=new RouteOptimizationDBEntities();
            if (Session["UserName"] != null)
            {
                string s=Session["UserName"].ToString();
                List<User> res = (from r in db.Users
                        where r.EmailID.Equals(s)
                        select r).ToList();
               Session["Name"]= res[0].FirstName.ToString() + " " + res[0].LastName.ToString();
            }
            return View();
        }
    }
}