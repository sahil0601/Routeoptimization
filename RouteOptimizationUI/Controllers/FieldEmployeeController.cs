using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RouteOptimizationUI.Models;


namespace RouteOptimizationUI.Controllers
{
    public class FieldEmployeeController : Controller
    {
        //
        // GET: /FieldEmployee/
        public ActionResult Index(FieldEmployee fe)
        {

            _fieldEmployees.Add(fe);

            var model = from r in _fieldEmployees
                        select r;

            return View(model);
        }

        //
        // GET: /FieldEmployee/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /FieldEmployee/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /FieldEmployee/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /FieldEmployee/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /FieldEmployee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /FieldEmployee/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /FieldEmployee/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        static List<FieldEmployee> _fieldEmployees = new List<FieldEmployee>
    {
        new FieldEmployee{
        employeeId=1,
        employeeName="John Smith",
        employeeAddress="32 James street"
    },
 new FieldEmployee{
        employeeId=2,
        employeeName="John Smith lana",
        employeeAddress="32 James street West"
    },
     new FieldEmployee{
        employeeId=3,
        employeeName="John Smith brown",
        employeeAddress="32 James street sout"
    }

    };

    }
}
