using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infra = RouteOptimization.Infrastructure;

namespace RouteOptimization.Core
{
    public class ProductService : IProduct
    {
        Infra.ProductDAL productDAL = new Infra.ProductDAL();

        public void GetAllProducts()
        {
           
        }
    }
}
