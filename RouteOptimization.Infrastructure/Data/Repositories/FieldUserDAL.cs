using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RouteOptimization.Infrastructure.Data.ViewModel;

namespace RouteOptimization.Infrastructure
{
    public class FieldUserDAL
    {
        RouteOptimizationDBEntities db = new RouteOptimizationDBEntities();
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

        public void OrderDelivered(int orderid, String status)
        {
            if (status.Equals("DE"))
            {
                var result = (from order in db.Orders
                              where order.OrderID == orderid
                              select order).Single();

                result.Status = "DE";
                db.SaveChanges();
            }

            else if (status.Equals("ND"))
            {
                var result = (from order in db.Orders
                              where order.OrderID == orderid
                              select order).Single();

                result.Status = "ND";
                db.SaveChanges();
            }
        }

        public String checkRosterStatus(int userid)
        {
            var datef = Convert.ToDateTime("" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + " 00:00:00.000");
            var result = (from r in db.Rosters
                          where r.UserID == userid && r.ActualDeliveryDate == datef && r.DeliveryStatus =="PE"
                          select r).SingleOrDefault();
            if (result != null)
            {
                return result.DeliveryStatus;
            }
            else
            {
                return null;
            }

        }

        public void completeRoster(int rosterid,int userid)
        {

            var result = (from roster in db.Rosters
                          where roster.RosterID == rosterid
                          select roster).Single();

            result.DeliveryStatus = "DE";
            db.SaveChanges();

            var res = (from user in db.UserAvailabilities
                       where user.UserID == userid
                       select user).Single();

            res.Status = "AV";
            db.SaveChanges();
        }
    }
}
