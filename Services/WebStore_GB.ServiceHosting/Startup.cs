using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using WebStore_GB.Domain.Entities.Identity;
using WebStore_GB.Interfaces.Services;
using WebStore_GB.Services.Data;
using WebStore_GB.Services.Products.InSQL;
using WevStore_GB.DAL.Context;

namespace WebStore_GB.ServiceHosting
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<WebStoreDB>(opt =>
               opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<WebStoreDBInitializer>();

            services.AddIdentity<User, Role>()
              .AddEntityFrameworkStores<WebStoreDB>()
              .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(opt =>
            {
#if DEBUG
                opt.Password.RequiredLength = 3;
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredUniqueChars = 3;

                //opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCD1234567890";
                opt.User.RequireUniqueEmail = false;
#endif

                opt.Lockout.AllowedForNewUsers = true;
                opt.Lockout.MaxFailedAccessAttempts = 10;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            });

            services.AddControllers();

            services
                .AddScoped<IEmployeesData, SqlEmployeesData>()
                .AddScoped<IProductData, SqlProductData>()
                .AddScoped<IOrderService, SqlOrderService>();

            #region Swagger

            services.AddSwaggerGen(opt =>
                {
                    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "WebStore_GB.API", Version = "v1" });

                    // подключить файлы откуда swagger будет получать информацию.
                    const string WEB_API_XML = "WebStore_GB.ServiceHosting.xml";
                    const string WEB_DOMAIN_XML = "WebStore_GB.Domain.xml";
                    const string DEBUG_PATH = "bin/debug/netcoreapp3.1";

                    opt.IncludeXmlComments(WEB_API_XML);
                    if (File.Exists(WEB_DOMAIN_XML))
                    {
                        opt.IncludeXmlComments(WEB_DOMAIN_XML);
                    }
                    else if (File.Exists(Path.Combine(DEBUG_PATH, WEB_DOMAIN_XML)))
                    {
                        opt.IncludeXmlComments(Path.Combine(DEBUG_PATH, WEB_DOMAIN_XML));
                    }
                }); 

            #endregion
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, WebStoreDBInitializer db)
        {
            db.Initialize();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            #region Swagger

            app.UseSwagger();
            app.UseSwaggerUI(opt => 
            {
                // путь, где будет находится документ с техническим описанием вебапи
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "WebStore_GB.API");
                // путь по которому обратиться к swagger, если string.Empty, то адрес будет "/"
                opt.RoutePrefix = string.Empty;
            });

            #endregion

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
