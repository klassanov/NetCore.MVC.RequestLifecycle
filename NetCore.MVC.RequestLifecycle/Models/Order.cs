using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.MVC.RequestLifecycle.Models
{
    public class Order
    {
        public string ProductName { get; set; }

        public string Count { get; set; }

        public string Description { get; set; }
    }
}
