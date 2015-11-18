using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RouteOptimization.Infrastructure
{
    public class StoreViewModel
    {
        public Store Store { get; set; }
        public IEnumerable<Store> AllStores { get; set; }

        public virtual IEnumerable<Store> UserDetails { get; set; }
        public virtual IEnumerable<Country> CountryDetails { get; set; }
        public virtual IEnumerable<Province> ProvinceDetails { get; set; }
        public virtual IEnumerable<City> CityDetails { get; set; }
        public virtual IEnumerable<Address> AddressDetails { get; set; }

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
