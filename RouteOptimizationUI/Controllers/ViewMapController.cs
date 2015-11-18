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
    public class ViewMapController : Controller
    {
        GeneratemapsDAL gd = new GeneratemapsDAL();
        GenerateMapsViewModel gvm = new GenerateMapsViewModel();

        // GET: ViewMap
        public ActionResult Index()
        {
            return View(gvm.GetAdreessForMap());
        }

        public ActionResult GetMap()
        {
            return View();
        }

        public ActionResult tryanother()
        {
            return PartialView(gvm.GetAdreessForMap());
        }
    }
}