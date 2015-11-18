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
    public class RosterController : Controller
    {
        // GET: Roster
        private RouteOptimizationDBEntities db = new RouteOptimizationDBEntities();

        GeneratemapsDAL gd = new GeneratemapsDAL();
        RosterViewModel gvm = new RosterViewModel();

        public ActionResult Index()
        {
            RosterViewModel rvm = new RosterViewModel();
            OrderDAL od = new OrderDAL();
            rvm.allOrders = od.FillAll();
            UserDAL ud = new UserDAL();
            rvm.allUsers = ud.FillAll();
            ViewBag.StoreID = new SelectList(db.Stores, "StoreID", "StoreID");
            ViewBag.RetailerID = new SelectList(db.Retailers, "RetailerID", "RetailerID");
            return View(rvm);
        }

        //[HttpPost]
        public ActionResult Create(int[] selectOrder, int selectUser, string datepicker)
        {
            Roster roster = new Roster();
            RosterDAL rd = new RosterDAL();
            RosterViewModel rvm = new RosterViewModel();
            roster.UserID = selectUser;
            roster.DeliveryStatus = "PE";
            datepicker = datepicker + " 00:00:00";
            roster.ActualDeliveryDate = Convert.ToDateTime(datepicker);
            rd.AddorUpdate(roster, selectOrder,selectUser);
            // Console.Write("hello");
        //    return View("Index",rvm);
            return RedirectToAction("Index");
        }

        public ActionResult ViewMap()
        {
            return PartialView(gvm.GetAdreessForMap());
        }
    }
}
