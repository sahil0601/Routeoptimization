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
    public class StoreController : Controller
    {
        private RouteOptimizationDBEntities db = new RouteOptimizationDBEntities();

        UserDAL ud = new UserDAL();

        public ActionResult Index()
        {

            StoreViewModel store = new StoreViewModel();
            StoreDAL sd = new StoreDAL();
            store.AllStores = sd.ListStores();

            var usertype = ud.GetUserTypes();
            var countries = ud.GetCountry();
            var city = ud.GetCity();
            var province = ud.GetProvince();
            store.CountryDetails = countries;
            store.ProvinceDetails = province;
            store.CityDetails = city;
            ViewData["country"] = ud.GetCountry();

            ViewBag.AddressID = new SelectList(db.Addresses, "AddressID", "Address1");
            ViewBag.RetailerID = new SelectList(db.Retailers, "RetailerID", "Name");
            ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "Name");
            ViewBag.ProvinceID = new SelectList(db.Provinces, "ProvinceID", "Name");
            ViewBag.CityID = new SelectList(db.Cities, "CityID", "Name");
            return View(store);
        }

        public ActionResult Partialndex()
        {
            StoreViewModel store = new StoreViewModel();
            StoreDAL sd = new StoreDAL();
            store.AllStores = sd.ListStores();
            return View("_Store",store.AllStores.ToList());
        }


        [HttpPost]
       // [ValidateAntiForgeryToken]
       // public ActionResult Create([Bind(Include = "StoreID,Name,StoreNumber,RetailerID,EmailID,Phone,AddressID")] Store store)
        public string Create(string Address1,string Address2, string Postal, int Country, int Province, int City, int ID, string Name, string StoreNumber, int RetailerID, string EmailID, string Phone)
        {
            StoreDAL storeBO = new StoreDAL();
   
            Store store = new Store();
            Address ad = new Address();
            ad.Address1 = Address1;
            ad.Address2 = Address2;
            ad.PostalCode = Postal;
            ad.CityID = City;
            ad.CountryID = Country;
            ad.ProvinceID = Province;
            int addid = 0;
            if (ModelState.IsValid)
            {


                storeBO.AddorUpdateAddress(ad);
                var result = db.Addresses.OrderByDescending(x => x.AddressID).First();
                addid = result.AddressID;
            }
            if (!ID.Equals(0))
            {
                store.StoreID = ID;
            }
            store.Name = Name;
            store.StoreNumber = StoreNumber;
            store.RetailerID = RetailerID;
            store.EmailID = EmailID;
            store.Phone = Phone;
            if (addid != 0)
            {

                store.AddressID = addid;
            }
            storeBO.AddorUpdate(store);
            return ("Added");
        }
        
        public ActionResult Edit([Bind(Include = "StoreID,Name,StoreNumber,RetailerID,EmailID,Phone")] Store store)
        {
            StoreViewModel st = new StoreViewModel();
            StoreDAL storeBO = new StoreDAL();

            Store user = db.Stores.Find(store.StoreID);
            Address add = db.Addresses.Find(user.AddressID);
            if (ModelState.IsValid)
            {             
                st.AllStores = storeBO.ListStores();
                st.Store = store;
               // db.Entry(store).State = EntityState.Modified;
               // db.SaveChanges();
             //   ViewBag.AddressID = new SelectList(db.Addresses, "AddressID", "Address1", store.AddressID);
                ViewBag.RetailerID = new SelectList(db.Retailers, "RetailerID", "Name", store.RetailerID);
                ViewBag.CityId = new SelectList(db.Cities, "CityID", "Name", add.CityID);
                ViewBag.ProvinceId = new SelectList(db.Provinces, "ProvinceId", "Name", add.ProvinceID);

                ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "Name", add.CountryID);
                ViewBag.ac = "Edit";

                st.Address1 = add.Address1;
                st.Address2 = add.Address2;
                st.AddressID = add.AddressID;
                st.PostalCode = add.PostalCode;
                var usertype = ud.GetUserTypes();
                var countries = ud.GetCountry();
                var city = ud.GetCity();
                var province = ud.GetProvince();
                st.CountryDetails = countries;
                st.ProvinceDetails = province;
                st.CityDetails = city;
                return View("Index",st);
            }
           // ViewBag.AddressID = new SelectList(db.Addresses, "AddressID", "Address1", store.AddressID);
            ViewBag.RetailerID = new SelectList(db.Retailers, "RetailerID", "Name", store.RetailerID);
            return View("Index",st);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Store store = db.Stores.Find(id);

            if (store == null)
            {
                return HttpNotFound();
            }
            StoreDAL storeBO = new StoreDAL();
            storeBO.DeleteP(store.StoreID);

            StoreViewModel str = new StoreViewModel();
            StoreDAL sd = new StoreDAL();
            str.AllStores = sd.ListStores();

            ViewBag.AddressID = new SelectList(db.Addresses, "AddressID", "Address1");
            ViewBag.RetailerID = new SelectList(db.Retailers, "RetailerID", "Name");
            return View("Index", str);
            
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
