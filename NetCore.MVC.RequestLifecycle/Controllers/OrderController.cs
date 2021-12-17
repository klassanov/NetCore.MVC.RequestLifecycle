using Microsoft.AspNetCore.Mvc;
using NetCore.MVC.RequestLifecycle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.MVC.RequestLifecycle.Controllers
{
    public class OrderController : Controller
    {
        [HttpPost]
        public IActionResult Create(List<Order> orderList)
        {
            return Content($"Binding successful: {orderList.Count} products model binded!");
        }
    }
}
