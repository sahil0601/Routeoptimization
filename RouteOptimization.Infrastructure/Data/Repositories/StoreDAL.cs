using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Infrastructure
{
    public class StoreDAL
    {
        RouteOptimizationDBEntities dbcontext = new RouteOptimizationDBEntities();

        public IEnumerable<Store> ListStores()
        {
            return dbcontext.Stores.ToList();
        }

        public List<Store> FillAll()
        {
            var result = from stores in dbcontext.Stores
                         orderby stores.StoreID
                         select stores;

            if (result != null)
            {
                return result.ToList();
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<Store> AddorUpdate(Store st)
        {
            if (dbcontext == null)
            {
                dbcontext = new RouteOptimizationDBEntities();
            }

            if (st.StoreID > 0)
            {
                Store store;
                store = GetById(st.StoreID);
                dbcontext.Entry(store).CurrentValues.SetValues(st);
                // dbcontext.Entry(store).State = EntityState.Modified;
            }
            else
            {
                dbcontext.Stores.Add(st);
            }
            dbcontext.SaveChanges();
            return dbcontext.Stores.ToList();

        }

        public IEnumerable<Address> AddorUpdateAddress(Address st)
        {
            if (dbcontext == null)
            {
                dbcontext = new RouteOptimizationDBEntities();
            }

            if (st.AddressID > 0)
            {
                Address store;
                store = GetAddById(st.AddressID);
                dbcontext.Entry(store).CurrentValues.SetValues(st);
                // dbcontext.Entry(store).State = EntityState.Modified;
            }
            else
            {
                dbcontext.Addresses.Add(st);
            }
            dbcontext.SaveChanges();
            return dbcontext.Addresses.ToList();

        }


        public string Delete(Store st)
        {
            if (dbcontext == null)
            {
                dbcontext = new RouteOptimizationDBEntities();
            }

            //dbcontext.Entry(st).State = EntityState.Deleted;

            dbcontext.Stores.Remove(st);
            dbcontext.SaveChanges();
            return "";
        }



        public string DeleteP(Int64 id)
        {
            if (dbcontext == null)
            {
                dbcontext = new RouteOptimizationDBEntities();

            }

            var temp = (from x in dbcontext.Stores
                        where x.StoreID == id
                        select x).FirstOrDefault();
            dbcontext.Stores.Remove(temp);
            dbcontext.SaveChanges();
            return "";
        }

        public Store GetById(Int64 Id)
        {
            if (dbcontext == null)
            {
                dbcontext = new RouteOptimizationDBEntities();
            }
            var result = (from store in dbcontext.Stores
                          where store.StoreID == Id
                          select store).FirstOrDefault<Store>();

            if (result != null)
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public Address GetAddById(Int64 Id)
        {
            if (dbcontext == null)
            {
                dbcontext = new RouteOptimizationDBEntities();
            }
            var result = (from store in dbcontext.Addresses
                          where store.AddressID == Id
                          select store).FirstOrDefault<Address>();

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
