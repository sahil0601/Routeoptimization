using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Infrastructure
{
    public class RosterDAL
    {

        RouteOptimizationDBEntities db = new RouteOptimizationDBEntities();

        public IEnumerable<Roster> AddorUpdate(Roster rt, int[] orderID, int userID)
        {
            if (db == null)
            {
                db = new RouteOptimizationDBEntities();
            }

            if (rt.RosterID > 0)
            {
                Roster roster;
                roster = GetById(rt.RosterID);
                db.Entry(roster).CurrentValues.SetValues(rt);

            }
            else
            {
                db.Rosters.Add(rt);
            }
            db.SaveChanges();

            var id = rt.RosterID;

            RosterOrder ro = new RosterOrder();

            for (int i = 0; i < orderID.Length; i++)
            {
                ro.RosterID = id;
                ro.OrderID = orderID[i];


                db.RosterOrders.Add(ro);

                db.SaveChanges();

        


                var res=(from order in db.Orders
                     
                        where order.OrderID == ro.OrderID
                        select order).SingleOrDefault();

                res.Status = "REC";

                db.SaveChanges();
        

            }

            var ua = (from user in db.UserAvailabilities
                      where user.UserID == userID
                      select user).SingleOrDefault();

            ua.Status = "OC";

            db.SaveChanges();

            return db.Rosters.ToList();

        }


        public Roster GetById(Int64 Id)
        {
            if (db == null)
            {
                db = new RouteOptimizationDBEntities();
            }
            var result = (from roster in db.Rosters
                          where roster.RosterID == Id
                          select roster).FirstOrDefault<Roster>();

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
