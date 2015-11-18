using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RouteOptimization.Infrastructure;

namespace RouteOptimizationUI.Controllers
{
    public class OrderController : Controller
    {
        private RouteOptimizationDBEntities db = new RouteOptimizationDBEntities();
        //
        // GET: /Order/
        public ActionResult Index()
        {
            OrderViewModel order = new OrderViewModel();
            OrderDAL cd = new OrderDAL();
            order.AllOrders = cd.ListOrders();
            ViewBag.StoreID = new SelectList(db.Stores, "StoreID", "Name");
            ViewBag.RetailerID = new SelectList(db.Retailers, "RetailerID", "Name");
            return View(order);
        }

        // GET: Stores/Edit/5
        public ActionResult Edit(int id)
        {
            OrderViewModel ord = new OrderViewModel();
            OrderDAL od = new OrderDAL();
            ord.AllOrders = od.ListOrders();
            ord.OrderedProducts = od.AllOrderedProduct(id);
            ord.Order = od.GetOrderByID(id);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name");
            ViewBag.StoreID = new SelectList(db.Stores, "StoreID", "Name");
            ViewBag.RetailerID = new SelectList(db.Retailers, "RetailerID", "Name");
            return View("Index", ord);
        }
    }
}