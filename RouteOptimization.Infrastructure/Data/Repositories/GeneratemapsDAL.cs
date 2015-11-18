using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RouteOptimization.Infrastructure.Data.ViewModel;
using System.Collections;

namespace RouteOptimization.Infrastructure.Data.Repositories
{
    public class GeneratemapsDAL
    {
        RouteOptimizationDBEntities db = new RouteOptimizationDBEntities();

        public IQueryable<GeneratemapsDAL> GetAdreessForMap()
        {
            var result = from r in db.RosterOrders
                         join o in db.Orders on r.OrderID equals o.OrderID into ordId
                         from ord in ordId
                         join s in db.Stores on ord.StoreID equals s.StoreID into strId
                         from strd in strId
                         join a in db.Addresses on strd.AddressID equals a.AddressID
                         select new GeneratemapsDAL()
                         {
                             Address1 = a.Address1,
                             Address2 = a.Address2,
                             CityId = a.CityID,
                             CountryId = a.CountryID,
                             ProvinceId = a.ProvinceID,
                             PostalCode = a.PostalCode
                         };
            return result;

        }



        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string PostalCode { get; set; }
        public int CityId { get; set; }
        public int CountryId { get; set; }
        public int ProvinceId { get; set; }
        public int UserTypeId { get; set; }
        public string Type { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
    }
}
