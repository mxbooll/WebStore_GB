using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using WebStore_GB.Clients.Employees;
using WebStore_GB.Clients.Identity;
using WebStore_GB.Clients.Orders;
using WebStore_GB.Clients.Products;
using WebStore_GB.Clients.Values;
using WebStore_GB.Domain.Entities.Identity;
using WebStore_GB.Infrastructure.AutoMapperProfiles;
using WebStore_GB.Interfaces.Services;
using WebStore_GB.Interfaces.TestApi;
using WebStore_GB.Services.Products.InCookies;

namespace WebStore_GB
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<ViewModelsMapping>();
            }, typeof(Startup));

            services.AddIdentity<User, Role>(/*opt => { }*/)          
              .AddDefaultTokenProviders();

            #region WebApi Identity clients stores

            services
                .AddTransient<IUserStore<User>, UsersClient>()
                .AddTransient<IUserPasswordStore<User>, UsersClient>()
                .AddTransient<IUserEmailStore<User>, UsersClient>()
                .AddTransient<IUserPhoneNumberStore<User>, UsersClient>()
                .AddTransient<IUserTwoFactorStore<User>, UsersClient>()
                .AddTransient<IUserClaimStore<User>, UsersClient>()
                .AddTransient<IUserLoginStore<User>, UsersClient>();
            services
                .AddTransient<IRoleStore<Role>, RolesClient>();

            #endregion

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

            services.ConfigureApplicationCookie(opt =>
            {
                opt.Cookie.Name = "WebStore.GeekBrains.ru";
                opt.Cookie.HttpOnly = true;
                opt.ExpireTimeSpan = TimeSpan.FromDays(10);

                opt.LoginPath = "/Account/Login";
                opt.LogoutPath = "/Account/Logout";
                opt.AccessDeniedPath = "/Account/AccessDenied";

                opt.SlidingExpiration = true;
            });

            services.AddControllersWithViews()
               .AddRazorRuntimeCompilation();

            services.AddScoped<IEmployeesData, EmployeesClient>();
            //services.AddScoped<IEmployeesData, SqlEmployeesData>();
            //services.AddSingleton<IEmployeesData, InMemoryEmployeesData>();

            //services.AddSingleton<IProductData, InMemoryProductData>();
            //services.AddScoped<IProductData, SqlProductData>();
            services.AddScoped<IProductData, ProductsClient>();
            services.AddScoped<ICartService, CookiesCartService>();
            services.AddScoped<IOrderService, OrdersClient>();
            //services.AddScoped<IOrderService, SqlOrderService>();

            services.AddTransient<IValueService, ValuesClient>();
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
            //    await next(); // ����� �������� �������� �� ������� await next()
            //    // �������������
            //});
            //app.UseMiddleware<>()

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                    );
            });
        }
    }
}
