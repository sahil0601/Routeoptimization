using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RouteOptimization.Infrastructure;

namespace RouteOptimizationUI.Controllers
{

     
    public class ProductController : Controller
    {

        private RouteOptimizationDBEntities db = new RouteOptimizationDBEntities();
        // GET: /Products/
        public ActionResult Index(ProductViewModel product)
        {
            ProductDAL pd = new ProductDAL();
            product.AllProducts = pd.ListProducts();
            return View(product);
        }

        [HttpPost]
        public ActionResult GetProductList()
        {
            ProductDAL pd = new ProductDAL();
            return PartialView(pd.ListProducts());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,Name,ProductNumber,ListPrice,Size,Weight")] Product product)
        {
            ProductDAL create = new ProductDAL();
            create.AddorUpdate(product);
            return RedirectToAction("Index");
        }

        public ActionResult Edit([Bind(Include = "ProductID,Name,ProductNumber,ListPrice,Size,Weight")] Product product)
        {
            ProductViewModel prod = new ProductViewModel();
            ProductDAL pd = new ProductDAL();
            prod.AllProducts = pd.ListProducts();
            prod.Product = product;
            return View("Index", prod);
        }

        // GET: /Products/Delete
        public ActionResult Delete(int id)
        {
            ProductDAL product = new ProductDAL();
            product.Delete(id);
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