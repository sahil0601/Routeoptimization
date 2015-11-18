using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RouteOptimization.Infrastructure;
using RouteOptimization.Infrastructure.Data.Repositories;
using RouteOptimization.Infrastructure.Data.ViewModel;

namespace RouteOptimizationUI.Controllers
{
    public class FieldUserController : Controller
    {
        FieldUserViewModel fuser = new FieldUserViewModel();
        // GET: FieldUser
        public ActionResult Index()
        {
            FieldUserDAL obj = new FieldUserDAL();
            int uid = (int)Session["UserID"];
            String status = obj.checkRosterStatus(uid);
            fuser.checkdeliveryStatus = status;
            return View(fuser);
        }


        public ActionResult OrderedProduct([Bind(Include = "orderid")]Order obj)
        {
            FieldUserDAL cd = new FieldUserDAL();
            return View(cd.AllOrderedProduct(obj.OrderID).ToList());
        }


        public ActionResult OrderDelivered([Bind(Include = "orderid,status")]Order obj)
        {
            FieldUserDAL cd = new FieldUserDAL();
            cd.OrderDelivered(obj.OrderID, obj.Status);
            return RedirectToAction("Index");
        }

        public ActionResult RosterStatus(int id)
        {
            FieldUserDAL field = new FieldUserDAL();
            int uid = (int)Session["UserID"];
            field.completeRoster(id, uid);
            return RedirectToAction("Index");
        }

    }
}