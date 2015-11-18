using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Resources;
using System.ComponentModel.DataAnnotations;


namespace RouteOptimization.Infrastructure.Data.ViewModel
{

    public class UserViewModel
    {
        public UserViewModel()
        {

        }
        RouteOptimizationDBEntities db = new RouteOptimizationDBEntities();
      //  RouteOptimizationDBEntities db = new RouteOptimizationDBEntities();

        public virtual IEnumerable<User> UserDetails { get; set; }
        public virtual IEnumerable<Country> CountryDetails { get; set; }
        public virtual IEnumerable<Province> ProvinceDetails { get; set; }
        public virtual IEnumerable<City> CityDetails { get; set; }
        public virtual IEnumerable<Address> AddressDetails { get; set; }
        public virtual IEnumerable<UserType> UserTypeDetails { get; set; }
        public virtual IEnumerable<UserViewModel> userviewModel { get; set; }
        public IQueryable<UserViewModel> GetUserViewModel()
        {
            var result = from d in db.Users
                         join b in db.Addresses on d.AddressID equals b.AddressID
                         select new UserViewModel()
                         {
                             UserId = d.UserID,
                             FirstName = d.FirstName,
                             LastName = d.LastName,
                             EmailID = d.EmailID,
                             Phone = d.Phone,
                             City = b.City.Name,
                             Country = b.Country.Name,
                             Province = b.Province.Name,
                             Type = d.UserType.Description,
                             AddressID = d.AddressID,
                             Address1 = b.Address1,
                             Address2 = b.Address2,
                             CityId = b.CityID,
                             ProvinceId = b.ProvinceID,
                             PostalCode = b.PostalCode,
                             CountryId = b.CountryID,
                         };
            return result;
        }


        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
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
        public string Postal { get; set; }
        public string EmailID { get; set; }
        public string Phone { get; set; }
        public int AddressID { get; set; }
    }
}
