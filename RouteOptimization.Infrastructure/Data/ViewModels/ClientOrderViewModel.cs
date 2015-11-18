using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Infrastructure
{
     public class ClientOrderViewModel
    {
        public Order Order { get; set; }
        public IEnumerable<Product> AllProducts { get; set; }
        public IEnumerable<string> SelectedProducts { get; set; }
        public int Quantity { get; set; }
        public IEnumerable<OrderProduct> OrderedProducts { get; set; }
        public IDictionary<string, int> prodDict{ get; set; }
        public int RetailerID { get; set; }
        public int StoreID { get; set; }
         public DateTime datepicker { get; set;}
        public int prod { get; set; }
    }
}
