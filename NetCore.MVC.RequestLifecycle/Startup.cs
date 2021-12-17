using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetCore.MVC.RequestLifecycle.Middleware;
using NetCore.MVC.RequestLifecycle.ModelBinding;

namespace NetCore.MVC.RequestLifecycle
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(options => 
            {
                //Register the model binder provider at index 0 so that it can be the first
                options.ModelBinderProviders.Insert(0, new CSVModelBinderProvider());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            /*
            //Custom middleware 0
            app.Map("/hello-world", SayHello);

            //Custom middleware 1
            app.Use(async (context, next) =>
            {
                //Logic before next middleware here
                Debug.WriteLine("Before");

                context.Items["greeting"] = "Hi there";

                await next.Invoke();

                ///Logic when we go back -> calls are stacked
                Debug.WriteLine("After");
            });

            //Custom middleware 2
            app.Run(async context =>
            {
                await context.Response.WriteAsync($"Hello world, from middleware: {context.Items["greeting"]}");
            });

            */

            //Custom middleware
            app.UseFeatureFlagsMiddleware();


            //Default configurations
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //Custom 404 middleware feature switch
            app.UseMiddleware<FeatureSwitchAuthMiddleware>();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private static void SayHello(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Hi, the request was mapped here!");
            });
        }
    }
}
