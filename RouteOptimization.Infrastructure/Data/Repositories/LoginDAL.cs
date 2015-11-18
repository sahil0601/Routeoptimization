using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Infrastructure.Data.Repositories
{
    public class LoginDAL
    {
        RouteOptimizationDBEntities db = new RouteOptimizationDBEntities();
        public List<Login> checkLog(string uname, string pass)
        {

            var log = from p in db.Logins
                      where p.UserName.Equals(uname) && p.Password.Equals(pass)
                      select p;

            return log.ToList();
        }

        public List<Login> checkLogdet(string uname)
        {

            var log = from p in db.Logins
                      where p.UserName.Equals(uname)
                      select p;

            return log.ToList();
        }
    }
}
