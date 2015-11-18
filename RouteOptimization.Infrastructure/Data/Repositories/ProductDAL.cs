using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Infrastructure
{
    public class ProductDAL
    {
        RouteOptimizationDBEntities db = new RouteOptimizationDBEntities();
        public IEnumerable<Product> ListProducts()
        {
            return db.Products.ToList();
        }

        public Product GetProductById(int productId)
        {
            var result = (from product in db.Products
                          where product.ProductID == productId
                          select product).FirstOrDefault<Product>();
            if (result != null)
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<Product> AddorUpdate(Product product)
        {
            if (product.ProductID > 0)
            {
                Product pid = this.GetProductById(product.ProductID);
                db.Entry(pid).CurrentValues.SetValues(product);
            }
            else
            {
                db.Products.Add(product);
            }
            db.SaveChanges();
            return db.Products.ToList();
        }

        public void Delete(int id)
        {
            //var result = (from o in db.OrderProducts
            //              where o.ProductID == id
            //              select o).ToList();
            //foreach(result
            var temp = (from x in db.Products
                        where x.ProductID == id
                        select x).FirstOrDefault();
            db.Products.Remove(temp);
            db.SaveChanges();
        }
    }

}
    