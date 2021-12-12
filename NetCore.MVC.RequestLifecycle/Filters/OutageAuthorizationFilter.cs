using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;

namespace NetCore.MVC.RequestLifecycle.Filters
{
    public class OutageAuthorizationFilter : Attribute, IAuthorizationFilter
    {
        //Reads the config flag Outage and if true, shortcircuits the request lifecycle
        //by setting the context.Result property, so the MVC pipeline does not go further.
        //It does not even instantiate a controller
        //This can also be achieved by middleware, but here we do it by MVC

        private readonly IConfiguration config;

        public OutageAuthorizationFilter(IConfiguration config)
        {
            this.config = config;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var appSwitch = this.config.GetSection("FeatureFlags").GetChildren().FirstOrDefault(x => x.Key == "Outage");
            if (appSwitch != null && bool.Parse(appSwitch.Value))
            {
                //Setting the result property makes so that the request does not proceed further in the MVC
                //When set, it is used as a response to the client directly
                context.Result = new ViewResult() { ViewName = "Outage" };
            }
        }
    }
}
