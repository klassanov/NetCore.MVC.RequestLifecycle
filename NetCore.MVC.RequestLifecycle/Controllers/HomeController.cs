using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetCore.MVC.RequestLifecycle.Filters;
using NetCore.MVC.RequestLifecycle.Models;

namespace NetCore.MVC.RequestLifecycle.Controllers
{
    //[OutageAuthorizationFilter] will not work since it has a custom controller
    [TypeFilter(typeof(OutageAuthorizationFilter))] //This way the dependencies are solved
    //Slight difference with ResourceFilter -> to explore
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("/contact-us", Name = "Contact")]
        public IActionResult Contact() => View();

        [HttpPost]
        [Route("/contact-us", Name ="Contact")]
        public IActionResult Contact(ContactViewModel contactInfo)
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
