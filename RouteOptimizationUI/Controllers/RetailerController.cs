using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RouteOptimization.Infrastructure;


namespace RouteOptimizationUI.Controllers
{
    public class RetailerController : Controller
    {
        private RouteOptimizationDBEntities db = new RouteOptimizationDBEntities();

        public ActionResult Index()
        {
            RetailerViewModel retailer = new RetailerViewModel();

            RetailerDAL rd = new RetailerDAL();
            retailer.allRetailers = rd.ListRetailers();

            //ViewBag.AddressID = new SelectList(db.Addresses, "AddressID", "Address1");
            //      ViewBag.RetailerID = new SelectList(db.Retailers, "RetailerID", "Name");

            return View(retailer);
        }





        [HttpPost]
       
        public ActionResult Create(int ID, string Name, string RetailerNumber, string EmailID, string Phone)
        {
            Retailer ret=new Retailer();
            if (!ID.Equals(0))
            {
                ret.RetailerID = ID;
            }
            
            ret.Name = Name;
            ret.RetailerNumber = RetailerNumber;
            ret.EmailID = EmailID;
            ret.Phone = Phone;
           // ret.AddressID = AddressID;


            RetailerDAL retailerBO = new RetailerDAL();
            retailerBO.AddorUpdate(ret);

            return RedirectToAction("Index");


        }

      
        public ActionResult Edit([Bind(Include = "RetailerID,Name,RetailerNumber,EmailID,Phone,AddressID")] Retailer retailer)
        {

            RetailerViewModel rt = new RetailerViewModel();
            RetailerDAL retailerBO = new RetailerDAL();

            if (ModelState.IsValid)
            {
                rt.allRetailers = retailerBO.ListRetailers();
                rt.retailer = retailer;

             //   ViewBag.AddressID = new SelectList(db.Addresses, "AddressID", "Address1", retailer.AddressID);
                //   ViewBag.RetailerID = new SelectList(db.Retailers, "RetailerID", "Name", store.RetailerID);
                return View("Index", rt);
            }
          //  ViewBag.AddressID = new SelectList(db.Addresses, "AddressID", "Address1", retailer.AddressID);
            //  ViewBag.RetailerID = new SelectList(db.Retailers, "RetailerID", "Name", store.RetailerID);
            return View("Index", rt);

        }


        public ActionResult Delete(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Retailer retailer = db.Retailers.Find(id);

            //if (retailer == null)
            //{
            //    return HttpNotFound();
            //}
            //RetailerDAL retailerBO = new RetailerDAL();
            //retailerBO.DeleteP(retailer.RetailerID);

            //RetailerViewModel rvm = new RetailerViewModel();
            //RetailerDAL rd = new RetailerDAL();
            //rvm.allRetailers = rd.ListRetailers();

            //ViewBag.AddressID = new SelectList(db.Addresses, "AddressID", "Address1");
            ////   ViewBag.RetailerID = new SelectList(db.Retailers, "RetailerID", "Name");
            //return View("Index", rvm);

            RetailerDAL rd = new RetailerDAL();
            rd.DeleteP(id);
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
    }
}