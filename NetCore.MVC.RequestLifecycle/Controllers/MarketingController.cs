using Microsoft.AspNetCore.Mvc;
using NetCore.MVC.RequestLifecycle.Filters;

namespace NetCore.MVC.RequestLifecycle.Controllers
{
    public class MarketingController : Controller
    {
        [MobileRedirectActionFilter(Action = "NewSplash", Controller = "Marketing")]
        public IActionResult Splash()
        {
            return Content("The old splash page");
        }

        public IActionResult NewSplash()
        {
            return Content("The new splash page");
        }

    }
}
