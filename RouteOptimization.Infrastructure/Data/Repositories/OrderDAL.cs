using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Infrastructure
{
    public class OrderDAL
    {
        RouteOptimizationDBEntities db = new RouteOptimizationDBEntities();
        public IEnumerable<Order> ListOrders()
        {
            var result = (from order in db.Orders
                                  where order.Status == "PE"
                                  select order).ToList();
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }
        }

        public IEnumerable<Product> ListProducts()
        {
            return db.Products.ToList();
        }


            public IEnumerable<OrderProduct> AllOrderedProduct(int orderId)
        {
                var result = (from order in db.OrderProducts
                                  where order.OrderID == orderId
                                  select order).ToList();
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }
        }

            public Order GetOrderByID(int orderID)
            {
                var result = (from order in db.Orders
                              where order.OrderID == orderID
                              select order).FirstOrDefault<Order>();
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }



            public IEnumerable<Order> FillAll()
            {
                var list = from order in db.Orders
                           where order.Status == "PE" || order.Status=="ND"

                           orderby order.OrderID
                           select order;

                if (list != null)
                {
                    return list.ToList();
                }

                else
                    return null;
            }
    }
}
