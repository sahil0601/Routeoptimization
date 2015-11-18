﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RouteOptimization.Infrastructure.Data.ViewModel;

namespace RouteOptimization.Infrastructure
{
    public class RosterViewModel
    {
        public Order order { get; set; }

        public IEnumerable<Order> allOrders { get; set; }


        public User user { get; set; }

        public IEnumerable<User> allUsers { get; set; }

                RouteOptimizationDBEntities db = new RouteOptimizationDBEntities();


          public virtual IEnumerable<RosterViewModel> userviewModel { get; set; }
        public IQueryable<RosterViewModel> GetAdreessForMap()
        {
            var datef = Convert.ToDateTime("" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + " 00:00:00.000");
            var res = from r in db.Rosters
                      join rr in db.RosterOrders on r.RosterID equals rr.RosterID
                      join o in db.Orders on rr.OrderID equals o.OrderID
                      join s in db.Stores on o.StoreID equals s.StoreID
                      join a in db.Addresses on s.AddressID equals a.AddressID
                      where r.UserID == 2 && r.ActualDeliveryDate == datef 
                      select new RosterViewModel()
                      {
                          Address1 = a.Address1,
                          Address2 = a.Address2,
                          CityId = a.CityID,
                          CountryId = a.CountryID,
                          ProvinceId = a.ProvinceID,
                          PostalCode = a.PostalCode,
                          City = a.City.Name,
                          Country = a.Country.Name,
                          Province = a.Province.Name
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
    
    }
}