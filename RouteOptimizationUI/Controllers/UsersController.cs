using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RouteOptimization.Infrastructure.Data;
using RouteOptimization.Infrastructure.Data.Repositories;
using RouteOptimization.Infrastructure.Data.ViewModel;
using RouteOptimization.Infrastructure;

namespace RouteOptimizationUI.Controllers
{
    public class UsersController : Controller
    {
        private RouteOptimizationDBEntities db = new RouteOptimizationDBEntities();
       // private RouteOptimizationDBEntities db = new RouteOptimizationDBEntities();
        UserDAL ud = new UserDAL();
        // GET: Users
        public ActionResult Index()
        {
            //   var users = db.Users.Include(u => u.UserType);
            //  return View(users.ToList());

            return PartialView(ud.GetUserViewModel());

        }

        public ActionResult Partial(int i)
        {
            return PartialView("PartialList", ud.GetUserViewModel());
        }


        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        UserViewModel uvm = new UserViewModel();

        // GET: Users/Create
        public ActionResult Create()
        {

            var usertype = ud.GetUserTypes();
            var countries = ud.GetCountry();
            var city = ud.GetCity();
            var province = ud.GetProvince();
            uvm.CountryDetails = countries;
            uvm.ProvinceDetails = province;
            uvm.CityDetails = city;
            uvm.UserTypeDetails = usertype;
            ViewData["country"] = ud.GetCountry();
            // ViewBag.Countries = new SelectList(db.Countries, "CountryId", "Name");
            //ViewBag.Province = new SelectList(db.Provinces, "ProvinceId", "Name","CountryId");
            //ViewBag.City = new SelectList(db.Cities, "CityId", "Name","ProvinceId");
            //ViewBag.UserTypeID = new SelectList(db.UserTypes, "UserTypeID", "Description");
            return View(uvm);
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserViewModel uv)// [Bind(Include = "UserTypeID,FirstName,LastName,EmailID,Phone,AddressID,Address1,Address2,PostalCode,CountryDetails,ProvinceDetails,CityDetails")] UserViewModel RedeemTransaction)//[Bind(Include = "UserID,UserTypeID,FirstName,LastName,EmailID,Phone,AddressID")] User user, [Bind(Include = "Address1,Address2,CityId,ProvinceId,PostalCode,CountryId")] Address add)
        {
            User usr = new RouteOptimization.Infrastructure.User();
            usr.EmailID = uv.EmailID;
            usr.FirstName = uv.FirstName;
            usr.LastName = uv.LastName;
            usr.Phone = uv.Phone;
            usr.UserTypeID = uv.UserTypeId;

            Address ad = new Address();
            ad.Address1 = uv.Address1;
            ad.Address2 = uv.Address2;
            ad.PostalCode = uv.PostalCode;
            ad.CityID = uv.CityId;
            ad.CountryID = uv.CountryId;
            ad.ProvinceID = uv.ProvinceId;

            //     ad.CityID = RedeemTransaction.CityDetails;
            //    ad.CountryID = RedeemTransaction.CountryDetails;
            //     ad.ProvinceID = RedeemTransaction.ProvinceDetails;
            // int i=0;

            Login lg = new Login();
            lg.UserName = uv.EmailID;
            lg.Password = uv.EmailID;
            lg.UserTypeID = uv.UserTypeId;

            int addid = 0;
            if (ModelState.IsValid)
            {
                ud.AddAddress(ad);
                var result = db.Addresses.OrderByDescending(x => x.AddressID).First();
                addid = result.AddressID;
            }

            if (ModelState.IsValid)
            {
                if (addid != 0)
                {
                    usr.AddressID = addid;
                    ud.AddUser(usr);
                }
            }

            if (ModelState.IsValid)
            {
                var res = db.Users.OrderByDescending(x => x.UserID).First();
                lg.UserID = res.UserID;

                ud.AddLogin(lg);
            }

            if (ModelState.IsValid)
            {
                UserAvailability ua = new UserAvailability();
                ua.UserID = usr.UserID;
                ua.Status = "AV";
                ud.AddUAvail(ua);
                return RedirectToAction("Create");
            }



            //   ViewBag.UserTypeID = new SelectList(db.UserTypes, "UserTypeID", "Description");
            return View(uv);//user);
        }

        public ActionResult ResetPassword(UserViewModel uv)
        {
            List<Login> l = new List<Login>();

            Login log = new Login();
            // ll.LoginID = r.LoginID;
            LoginDAL ld = new LoginDAL();
            l = ld.checkLogdet(uv.EmailID);
            if (l != null)
            {
                log.LoginID = l[0].LoginID;
                log.Password = l[0].UserName;
                log.UserID = l[0].UserID;
                log.UserName = l[0].UserName;
                log.UserTypeID = l[0].UserTypeID;
                db.Entry(log).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.ac = "Create";

            }
            return RedirectToAction("Create");
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            Address add = db.Addresses.Find(user.AddressID);

            if (user == null)
            {
                return HttpNotFound();
            }

            ViewBag.ac = "Edit";
            ViewBag.UserTypeID = new SelectList(db.UserTypes, "UserTypeID", "Description", user.UserTypeID);
            ViewBag.CityId = new SelectList(db.Cities, "CityID", "Name", add.CityID);
            ViewBag.ProvinceId = new SelectList(db.Provinces, "ProvinceId", "Name", add.ProvinceID);

            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "Name", add.CountryID);
            uvm.Address1 = add.Address1;
            uvm.Address2 = add.Address2;
            uvm.AddressID = add.AddressID;
            uvm.EmailID = user.EmailID;
            uvm.FirstName = user.FirstName;
            uvm.LastName = user.LastName;
            uvm.Phone = user.Phone;
            uvm.PostalCode = add.PostalCode;
            uvm.UserId = user.UserID;
            var usertype = ud.GetUserTypes();
            var countries = ud.GetCountry();
            var city = ud.GetCity();
            var province = ud.GetProvince();
            uvm.CountryDetails = countries;
            uvm.ProvinceDetails = province;
            uvm.CityDetails = city;
            uvm.UserTypeDetails = usertype;

            // ViewBag.Countries = new SelectList(db.Countries, "CountryId", "Name");
            //ViewBag.Province = new SelectList(db.Provinces, "ProvinceId", "Name","CountryId");
            //ViewBag.City = new SelectList(db.Cities, "CityId", "Name","ProvinceId");
            //ViewBag.UserTypeID = new SelectList(db.UserTypes, "UserTypeID", "Description");
            return View("Create", uvm);
        }



        public JsonResult GetStates(string country)
        {
            int c = Convert.ToInt32(country);
            List<Province> states = new List<Province>();

            states = ud.GetProvinceById(c);
            return Json(new SelectList(states, "ProvinceID", "Name"));
        }

        public JsonResult GetCity(string state)
        {
            int c = Convert.ToInt32(state);
            List<City> cities = new List<City>();

            cities = ud.GetCityById(c);
            return Json(new SelectList(cities, "CityID", "Name"));
        }


        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserViewModel uv)// [Bind(Include = "UserTypeID,FirstName,LastName,EmailID,Phone,AddressID,Address1,Address2,PostalCode,CountryDetails,ProvinceDetails,CityDetails")] UserViewModel RedeemTransaction)//[Bind(Include = "UserID,UserTypeID,FirstName,LastName,EmailID,Phone,AddressID")] User user, [Bind(Include = "Address1,Address2,CityId,ProvinceId,PostalCode,CountryId")] Address add)
        {
            User usr = new RouteOptimization.Infrastructure.User();
            //userid , addressid
            usr.UserID = uv.UserId;
            usr.EmailID = uv.EmailID;
            usr.FirstName = uv.FirstName;
            usr.LastName = uv.LastName;
            usr.Phone = uv.Phone;
            usr.UserTypeID = uv.UserTypeId;
            usr.AddressID = uv.AddressID;

            Address ad = new Address();
            ad.Address1 = uv.Address1;
            ad.Address2 = uv.Address2;
            ad.PostalCode = uv.PostalCode;
            ad.CityID = uv.CityId;
            ad.CountryID = uv.CountryId;
            ad.ProvinceID = uv.ProvinceId;
            ad.AddressID = uv.AddressID;

            //     ad.CityID = RedeemTransaction.CityDetails;
            //    ad.CountryID = RedeemTransaction.CountryDetails;
            //     ad.ProvinceID = RedeemTransaction.ProvinceDetails;
            int i = 0;
            int addid = 0;
            if (ModelState.IsValid)
            {
                db.Entry(ad).State = EntityState.Modified;
                //  db.Addresses.Add(ad);
                i = db.SaveChanges();
                var result = db.Addresses.OrderByDescending(x => x.AddressID).First();
                addid = result.AddressID;
            }
            if (ModelState.IsValid)
            {
                if (addid != 0)
                {
                    // usr.AddressID = addid;
                    db.Entry(usr).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Create");
                }
            }

            //   ViewBag.UserTypeID = new SelectList(db.UserTypes, "UserTypeID", "Description");
            return View(uv);//user);       
            //      db.Entry(user).State = EntityState.Modified;
            //    db.SaveChanges();
            //  return RedirectToAction("Index");
            // }
            // ViewBag.UserTypeId = new SelectList(db.UserTypes, "UserTypeID", "Description", user.UserTypeID);
            // return View(user);
        }
        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            int i = 1;
            if (i == 1)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                User user = db.Users.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }

                List<UserAvailability> uu = new List<UserAvailability>();
                uu = ud.checkUserAvai(user.UserID);
                UserAvailability uaa = db.UserAvailabilities.Find(uu[0].AvailabilityID);
                db.UserAvailabilities.Remove(uaa);

                List<Login> l = new List<Login>();

               
                // ll.LoginID = r.LoginID;
                LoginDAL ld = new LoginDAL();
                l = ld.checkLogdet(user.EmailID);
                Login log = db.Logins.Find(l[0].LoginID);
                db.Logins.Remove(log);

                Address add = db.Addresses.Find(user.AddressID);
                db.Addresses.Remove(add);
                db.Users.Remove(user);
                db.SaveChanges();
                i++;

            }
            return RedirectToAction("Create");
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Create");
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
