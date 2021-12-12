using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace NetCore.MVC.RequestLifecycle.Middleware
{
    public class FeatureSwitchAuthMiddleware
    {
        //Idea: If a feature is not implemented, but it is requested, we return a 404
        //This middleware should sit after EndpointRoutingMiddleware (Endpoint selection) and before EndpointMiddleware (Endpoint execution)
        //It relies on the selected endpoint info
        //It should be registered (and executed) after  app.UseRouting(); and before  app.UseEndpoints

        private readonly RequestDelegate _next;

        public FeatureSwitchAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IConfiguration config)
        {
            //Get the selected endpoint and its route attribute
            var endpoint = context.GetEndpoint();
            var endpointMetadata = endpoint?.Metadata;
            var endpontRouteAttributeMetadata = endpointMetadata?.GetMetadata<Microsoft.AspNetCore.Mvc.RouteAttribute>();


            if (endpontRouteAttributeMetadata != null)
            {
                //Feature flag is disabled
                var featureFlag = config.GetSection("FeatureFlags").GetChildren().FirstOrDefault(x => x.Key == endpontRouteAttributeMetadata.Name);
                if (featureFlag != null && !bool.Parse(featureFlag.Value))
                {

                    //ReSet a new endpoint to custom 404 middleware
                    //All other middleware is cut off as we generate a response directly
                    //We substitute the previously selected endpoint
                    //Actually, here, we both get and set an endpoint
                    context.SetEndpoint(new Endpoint((context) =>
                    {
                        context.Response.StatusCode = StatusCodes.Status404NotFound;
                        return Task.CompletedTask;
                    },
                    EndpointMetadataCollection.Empty,
                    "FeatureNotFound"));


                    //Alternatively, just return a response, instead of setting up an endpoint that returns a 404
                    //context.Response.StatusCode = StatusCodes.Status404NotFound;
                    //await Task.CompletedTask;
                    //return;
                }
            }

            await _next(context);
        }
    }
}
