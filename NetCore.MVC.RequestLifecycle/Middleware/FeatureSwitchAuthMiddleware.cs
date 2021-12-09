using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.MVC.RequestLifecycle.Middleware
{
    public class FeatureSwitchAuthMiddleware
    {
        //Idea: If a feature is not implemented, but it is requested, we return a 404
        //This middleware should sit after EndpointRoutingMiddleware (Endpoint selection) and before EndpointMiddleware (Endpoint execution)
        //It relies on the selected endpoint info

        private readonly RequestDelegate _next;

        public FeatureSwitchAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IConfiguration config)
        {
            await _next(context);
        }
    }
}
