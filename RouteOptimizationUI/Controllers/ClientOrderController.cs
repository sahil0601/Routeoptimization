using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RouteOptimization.Infrastructure;

namespace RouteOptimizationUI.Controllers
{
    public class ClientOrderController : Controller
    {
        private RouteOptimizationDBEntities db = new RouteOptimizationDBEntities();
        // GET: ClientOrder
        ClientOrderDAL cd = new ClientOrderDAL();

        ClientOrderViewModel clientorder = new ClientOrderViewModel();
        static IEnumerable<Product> AllProducts;
        static IEnumerable<OrderProduct> AllProductsOrder;

        static int orderID;


        public ActionResult Create()
        {
            AllProducts = cd.ListProducts();
            clientorder.AllProducts = AllProducts;
            AllProductsOrder = cd.ListOrderProducts();

            ViewBag.StoreID = new SelectList(db.Stores, "StoreID", "Name");
            ViewBag.RetailerID = new SelectList(db.Retailers, "RetailerID", "Name");
            return View(clientorder);
        }

        static Dictionary<int, int> prodDict = new Dictionary<int, int>();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "prod,quantity")] ClientOrderViewModel cc)
        {
            if (prodDict.ContainsKey(cc.prod))
            {
                prodDict[cc.prod] = prodDict[cc.prod] + cc.Quantity;
            }
            else
            {

                prodDict.Add(cc.prod, cc.Quantity);
            }



            //    int test = orderID;
            if (orderID != 0)
            {
                cc.OrderedProducts = GetOrderList(orderID);
            }
            AllProducts = cd.ListProducts();
            cc.AllProducts = AllProducts;
            ViewBag.StoreID = new SelectList(db.Stores, "StoreID", "Name");
            ViewBag.RetailerID = new SelectList(db.Retailers, "RetailerID", "Name");
            // cc.OrderedProducts = AllProductsOrder;
            return View("Create", cc);

        }


        public void CreateFinal(int orderID)
        {
            ClientOrderViewModel cc = new ClientOrderViewModel();


            cc.OrderedProducts = GetOrderList(orderID);

            AllProducts = cd.ListProducts();
            cc.AllProducts = AllProducts;
            ViewBag.StoreID = new SelectList(db.Stores, "StoreID", "Name");
            ViewBag.RetailerID = new SelectList(db.Retailers, "RetailerID", "Name");
            //Create();
            RedirectToAction("Create", cc);

        }




        //public void CreateDict([Bind(Include = "prod,quantity")] ClientOrderViewModel cc)
        //{
        //    if (prodDict.ContainsKey(cc.prod))
        //    {
        //        prodDict[cc.prod] = prodDict[cc.prod] + cc.Quantity;
        //    }
        //    else
        //    {

        //        prodDict.Add(cc.prod, cc.Quantity);
        //    }
        //}

        //    //cc.OrderedProducts = GetOrderList(orderID);
        //    //AllProducts = cd.ListProducts();
        //    //cc.AllProducts = AllProducts;

        //    //// cc.OrderedProducts = AllProductsOrder;
        //    //return View("Create", cc);

        //}




        // public ActionResult CreateOrder()
        public void CreateOrder(int RetailerID, int StoreID, string datepicker)
        {
            Order order = new Order();
            order.RetailerID = RetailerID;
            order.StoreID = StoreID;
            order.Status = "PE";
            order.DeliveryDate = Convert.ToDateTime(datepicker);
            //order.RetailerID = 4;
            //order.StoreID = 4;
            //order.Status = "PE";
            //order.DeliveryDate = null;


            ClientOrderDAL clientorder = new ClientOrderDAL();
            clientorder.AddorUpdate(order);

            foreach (KeyValuePair<int, int> entry in prodDict)
            {
                OrderProduct orderproduct = new OrderProduct();
                orderproduct.OrderID = order.OrderID;
                orderproduct.ProductID = entry.Key;
                orderproduct.Quantity = entry.Value;
                clientorder.AddorUpdateOrderProducts(orderproduct);
            }

            //ClientOrderViewModel orderprod = new ClientOrderViewModel();
            //orderprod.AllProducts = AllProducts;
            //orderprod.OrderedProducts = cd.AllOrderedProduct(order.OrderID);
            //ViewBag.StoreID = new SelectList(db.Stores, "StoreID", "Name");
            //ViewBag.RetailerID = new SelectList(db.Retailers, "RetailerID", "Name");
            //return View("Create", orderprod);

            orderID = order.OrderID;
            ViewBag.StoreID = new SelectList(db.Stores, "StoreID", "Name");
            ViewBag.RetailerID = new SelectList(db.Retailers, "RetailerID", "Name");
            //return orderID; 
            CreateFinal(orderID);
            //return RedirectToAction("CreateFinal");
        }
        //return RedirectToAction("Create");


        public IEnumerable<OrderProduct> GetOrderList(int orderID)
        //public IEnumerable<OrderProduct> GetOrderList()
        {

            ClientOrderDAL clientorder = new ClientOrderDAL();
            // orderid = clientorder.GetLastOrderID();


            ClientOrderViewModel orderprod = new ClientOrderViewModel();
            orderprod.AllProducts = AllProducts;
            orderprod.OrderedProducts = cd.AllOrderedProduct(orderID);
            // orderprod.OrderedProducts=clientorder.GetLastOrderID();
            ViewBag.StoreID = new SelectList(db.Stores, "StoreID", "Name");
            ViewBag.RetailerID = new SelectList(db.Retailers, "RetailerID", "Name");
            return orderprod.OrderedProducts;



        }




    }
}