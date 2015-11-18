using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Infrastructure
{
    public class RetailerViewModel
    {
        public Retailer retailer { get; set; }

        public IEnumerable<Retailer> allRetailers { get; set; }
    }
}
