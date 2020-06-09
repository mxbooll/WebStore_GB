using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebStore_GB.Data;
using WebStore_GB.Infrastructure.Interfaces;
using WebStore_GB.Infrastructure.Services;
using WebStore_GB.Infrastructure.Services.InMemory;
using WebStore_GB.Infrastructure.Services.InSQL;
using WevStore_GB.DAL.Context;
using static WebStore_GB.Infrastructure.Services.InSQL.SqlProductData;

namespace WebStore_GB
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<WebStoreDB>(opt =>
               opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();

            services.AddSingleton<IEmployeesData, InMemoryEmployeesData>();
            //services.AddSingleton<IProductData, InMemoryProductData>();
            services.AddScoped<IProductData, SqlProductData>();
            services.AddTransient<WebStoreDBInitializer>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            app.UseStaticFiles();
            app.UseDefaultFiles();

            app.UseWelcomePage("/MVC");

            //app.Use(async (context, next) =>
            //{
            //    Debug.WriteLine($"Request to {context.Request.Path}");
            //    await next(); // Прерывание конвейера не вызывая await next()
            //    // постобработка
            //});
            //app.UseMiddleware<>()

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                    );
            });
        }
    }
}
