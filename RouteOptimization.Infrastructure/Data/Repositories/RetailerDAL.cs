using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Infrastructure
{
    public class RetailerDAL
    {
        RouteOptimizationDBEntities db = new RouteOptimizationDBEntities();

        public IEnumerable<Retailer> ListRetailers()
        {
            return db.Retailers.ToList();
        }

        public List<Retailer> FillAll()
        {
            var result = from retailer in db.Retailers
                         orderby retailer.RetailerID
                         select retailer;

            if (result != null)
            {
                return result.ToList();
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<Retailer> AddorUpdate(Retailer rt)
        {
            if (db == null)
            {
                db = new RouteOptimizationDBEntities();
            }

            if (rt.RetailerID > 0)
            {
                Retailer retailer;
                retailer = GetById(rt.RetailerID);
                db.Entry(retailer).CurrentValues.SetValues(rt);

            }
            else
            {
                db.Retailers.Add(rt);
            }
            db.SaveChanges();
            return db.Retailers.ToList();

        }


        public string Delete(Retailer st)
        {
            if (db == null)
            {
                db = new RouteOptimizationDBEntities();
            }



            db.Retailers.Remove(st);
            db.SaveChanges();
            return "";
        }


        public void DeleteP(Int64 id)
        {
            if (db == null)
            {
                db = new RouteOptimizationDBEntities();

            }

            var temp = (from x in db.Retailers
                        where x.RetailerID == id
                        select x).FirstOrDefault();
            db.Retailers.Remove(temp);
           db.SaveChanges();
           
        }


        public Retailer GetById(Int64 Id)
        {
            if (db == null)
            {
                db = new RouteOptimizationDBEntities();
            }
            var result = (from retailer in db.Retailers
                          where retailer.RetailerID == Id
                          select retailer).FirstOrDefault<Retailer>();

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
