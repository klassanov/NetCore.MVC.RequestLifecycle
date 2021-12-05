using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace NetCore.MVC.RequestLifecycle.Middleware
{
    public class FeatureFlagsMiddleware
    {
        private readonly RequestDelegate next;

        public FeatureFlagsMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context, IConfiguration config)
        {
            if (context.Request.Path.Value.Contains("/features"))
            {
                var featureFlags = config.GetSection("FeatureFlags").GetChildren().Select(x => $"{x.Key}:{x.Value}");
                await context.Response.WriteAsync(string.Join("\n", featureFlags));
            }
            else
            {
                await next.Invoke(context);
            }
        }
    }
}
