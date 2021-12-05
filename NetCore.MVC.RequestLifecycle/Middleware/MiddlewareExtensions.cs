using Microsoft.AspNetCore.Builder;

namespace NetCore.MVC.RequestLifecycle.Middleware
{
    public static class MiddlewareExtensions
    {
        public static void UseFeatureFlagsMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<FeatureFlagsMiddleware>();
        }
    }
}
