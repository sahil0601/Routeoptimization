using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Infrastructure
{
    public class OrderViewModel
    {
        public Order Order { get; set; }
        public IEnumerable<Order> AllOrders { get; set; }
        public IEnumerable<OrderProduct> OrderedProducts { get; set; }
    }
}
