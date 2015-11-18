using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RouteOptimization.Infrastructure.Data.ViewModel;

namespace RouteOptimization.Infrastructure
{
    public class FieldUserViewModel
    {
        RouteOptimizationDBEntities db = new RouteOptimizationDBEntities();
        public Roster Roster { get; set; }

        public IEnumerable<OrderProduct> AllOrderedProduct { get; set; }
        public String checkdeliveryStatus { get; set; }
        public List<FieldUserViewModel> GetAddress()
        {
            int sess = (int)System.Web.HttpContext.Current.Session["UserID"];
            var datef = Convert.ToDateTime("" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + " 00:00:00.000");
            var res = from r in db.Rosters
                      join rr in db.RosterOrders on r.RosterID equals rr.RosterID
                      join o in db.Orders on rr.OrderID equals o.OrderID
                      join s in db.Stores on o.StoreID equals s.StoreID
                      join a in db.Addresses on s.AddressID equals a.AddressID
                      where r.UserID == sess && r.ActualDeliveryDate == datef && r.DeliveryStatus=="PE"
                      select new FieldUserViewModel()
                      {
                          Address1 = a.Address1,
                          Address2 = a.Address2,
                          City = a.City.Name,
                          Province = a.Province.Name,
                          PostalCode = a.PostalCode,
                          OrderID = o.OrderID,
                          Status = o.Status,
                          RosterID = rr.RosterID
                      };
            return res.ToList();
        }

        public IQueryable<FieldUserViewModel> GetAdreessForMap()
        {
            int sess = (int)System.Web.HttpContext.Current.Session["UserID"];
            var datef = Convert.ToDateTime("" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + " 00:00:00.000");
            var res = from r in db.Rosters
                      join rr in db.RosterOrders on r.RosterID equals rr.RosterID
                      join o in db.Orders on rr.OrderID equals o.OrderID
                      join s in db.Stores on o.StoreID equals s.StoreID
                      join a in db.Addresses on s.AddressID equals a.AddressID
                      where r.UserID == sess && r.ActualDeliveryDate == datef && r.DeliveryStatus=="PE"
                      select new FieldUserViewModel()
                      {
                          Address1 = a.Address1,
                          Address2 = a.Address2,
                          CityId = a.CityID,
                          CountryId = a.CountryID,
                          ProvinceId = a.ProvinceID,
                          PostalCode = a.PostalCode,
                          City = a.City.Name,
                          Country = a.Country.Name,
                          Province = a.Province.Name,
                      };
            return res;
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
        public int RosterID { get; set; }
        public int OrderID { get; set; }
        public string Status { get; set; }
    }
}
