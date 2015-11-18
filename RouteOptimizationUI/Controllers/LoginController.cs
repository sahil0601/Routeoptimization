using RouteOptimization.Infrastructure;
using RouteOptimization.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RouteOptimizationUI.Controllers
{
    public class LoginController : Controller
    {

        private RouteOptimizationDBEntities db = new RouteOptimizationDBEntities();

        //
        // GET: /Login/
        public ActionResult Index()
        {
            return View();
        }
       // [HttpGet]
                LoginDAL ld = new LoginDAL();

                public ActionResult ChangePassword(LoginViewModel user)
                {
                    if (user.ChangedUserName != null)
                    {
                        Login log = new Login();
                        List<Login> l = new List<Login>();
                        l = ld.checkLog(user.ChangedUserName, user.Password);
                        if (l != null)
                        {
                            log.LoginID = l[0].LoginID;
                            log.Password = user.ChangedPassword;
                            log.UserID = l[0].UserID;
                            log.UserName = l[0].UserName;
                            log.UserTypeID = l[0].UserTypeID;
                            db.Entry(log).State = EntityState.Modified;
                            db.SaveChanges();
                            return View("Login");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Username and password is incorrect!");
                        }
                    }
                    return View();
                }

        

                public ActionResult Login(LoginViewModel user)
                {
                    // return View();
                    if (user.UserName != null)
                    {
                        List<Login> l = new List<Login>();
                        l = ld.checkLog(user.UserName, user.Password);
                        if (l != null || !l.Equals(0))
                        {
                            Session["UserID"] = l[0].UserID;
                            Session["UserName"] = l[0].UserName;
                            Session["role"] = l[0].UserTypeID.ToString();
                            if (Session["role"].ToString() == "1")
                            {
                                FormsAuthentication.SetAuthCookie(user.UserName, user.RememberMe);
                                return RedirectToAction("Index", "Main");
                            }
                            else if (Session["role"].ToString() == "2")
                            {
                                FormsAuthentication.SetAuthCookie(user.UserName, user.RememberMe);
                                return RedirectToAction("Index", "FieldUser");
                            }
                            else
                            {
                                ModelState.AddModelError("", "Login data is incorrect!");

                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Login data is incorrect!");
                        }
                        //   string role= user.IsValid(user.UserName, user.Password);

                        //if (role=="admin")
                        //{
                        //    FormsAuthentication.SetAuthCookie(user.UserName, user.RememberMe);
                        //    return RedirectToAction("Index", "Product");
                        //}
                        //else if(role=="employee")
                        //{
                        //    FormsAuthentication.SetAuthCookie(user.UserName, user.RememberMe);
                        //    return RedirectToAction("Index", "Stores");
                        //}
                        //
                    }
                    return View(user);
                }

        //public ActionResult Login(Models.User user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (user.IsValid(user.UserName, user.Password))
        //        {
        //             FormsAuthentication.SetAuthCookie(user.UserName, user.RememberMe);
        //            return RedirectToAction("Index", "Home");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "Login data is incorrect!");
        //        }
        //    }
        //    return View(user);
        //}
        public ActionResult Logout()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }
    }
}