using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NetCore.MVC.RequestLifecycle.Filters
{
    public class MobileRedirectActionFilter : Attribute, IActionFilter
    {
        //Used to pass values to the class from outside [MobileRedirectActionFilter(Action="...", Controller="...")]
        public string Action { get; set; }

        public string Controller { get; set; }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Debug.WriteLine("Action executed");

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Headers.ContainsKey("x-mobile"))
            {
                //Take a look also to other filter OutageAuthorizationFilter
                context.Result = new RedirectToActionResult(Action, Controller, null);
            }
        }
    }
}
