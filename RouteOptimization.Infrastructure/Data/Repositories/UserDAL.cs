using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RouteOptimization.Infrastructure.Data.ViewModel;

namespace RouteOptimization.Infrastructure
{
    public class UserDAL
    {
        RouteOptimizationDBEntities db = new RouteOptimizationDBEntities();
       // RouteOptimizationDBEntities db = new RouteOptimizationDBEntities();
        public List<User> GetAllUsers()
        {
            var r = from w in db.Users
                    where w.UserTypeID.Equals(1)
                    select w;
            return r.ToList();
        }

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

        public void AddLogin(Login lg)
        {
            db.Logins.Add(lg);
            db.SaveChanges();

        }

        public List<UserAvailability> checkUserAvai(int uname)
        {

            var log = from p in db.UserAvailabilities
                      where p.UserID ==uname
                      select p;

            return log.ToList();
        }

        public void AddUAvail(UserAvailability lg)
        {
            db.UserAvailabilities.Add(lg);
            db.SaveChanges();

        }
        public void AddAddress(Address ad)
        {
            db.Addresses.Add(ad);
            db.SaveChanges();

        }

        public void AddUser(User ud)
        {
            db.Users.Add(ud);
            db.SaveChanges();

        }

        public List<UserType> GetUserTypes()
        {
            var cntry = from ct in db.UserTypes
                        select ct;
            return cntry.ToList();
        }

        public List<Country> GetCountry()
        {
            var cntry = from ct in db.Countries
                        select ct;
            return cntry.ToList();
        }

        public List<City> GetCity()
        {
            var cntry = from ct in db.Cities
                        select ct;
            return cntry.ToList();
        }

        public List<Province> GetProvince()
        {
            var cntry = from ct in db.Provinces
                        select ct;
            return cntry.ToList();
        }

        public List<Province> GetProvinceById(int countryId)
        {
            var cntry = from ct in db.Provinces
                        where ct.CountryID.Equals(countryId)
                        select ct;
            return cntry.ToList();
        }

        public List<City> GetCityById(int provinceId)
        {
            var cntry = from ct in db.Cities
                        where ct.ProvinceID.Equals(provinceId)
                        select ct;
            return cntry.ToList();
        }

        public IEnumerable<User> FillAll()
        {

            var list = from user in db.Users
                       join ua in db.UserAvailabilities
                       on user.UserID equals ua.UserID
                       where ua.Status == "AV" && user.UserTypeID == 2
                       orderby user.UserID
                       select user;

            if (list != null)
            {
                return list.ToList();
            }

            else
                return null;
        }



    }
}
