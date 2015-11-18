using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Infrastructure
{
    public class ClientOrderDAL
    {
        RouteOptimizationDBEntities dbcontext = new RouteOptimizationDBEntities();
        public IEnumerable<Product> ListProducts()
        {
            return dbcontext.Products.ToList();
        }

        public IEnumerable<OrderProduct> ListOrderProducts()
        {
            return dbcontext.OrderProducts.ToList();
        }


        public Order GetOrderById(int orderId)
        {
            var result = (from order in dbcontext.Orders
                          where order.OrderID == orderId
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


        public OrderProduct GetOrderProdById(int orderprodId)
        {
            var result = (from orderprod in dbcontext.OrderProducts
                          where orderprod.OrderProductID == orderprodId
                          select orderprod).FirstOrDefault<OrderProduct>();
            if (result != null)
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public void AddorUpdate(Order order)
        {
            if (order.OrderID > 0)
            {   
                Order oid = this.GetOrderById(order.OrderID);
                dbcontext.Entry(oid).CurrentValues.SetValues(order);
            }
            else
            {
                dbcontext.Orders.Add(order);
            }
             dbcontext.SaveChanges();
             
        }

        public IEnumerable<OrderProduct> AddorUpdateOrderProducts(OrderProduct orderproduct)
        {
            if (orderproduct.OrderProductID > 0)
            {
                OrderProduct oid = this.GetOrderProdById(orderproduct.OrderProductID);
                dbcontext.Entry(oid).CurrentValues.SetValues(orderproduct);
            }
            else
            {
                dbcontext.OrderProducts.Add(orderproduct);
            }
            dbcontext.SaveChanges();
            return dbcontext.OrderProducts.ToList();
        }

        public IEnumerable<OrderProduct> AllOrderedProduct(int orderId)
        {
            var result = (from order in dbcontext.OrderProducts
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

      
    }
}
