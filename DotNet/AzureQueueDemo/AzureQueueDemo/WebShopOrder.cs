using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureQueueDemo
{
    public class WebShopOrder
    {
        public int OrderId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double OrderPrice { get; set; }
    }
}
