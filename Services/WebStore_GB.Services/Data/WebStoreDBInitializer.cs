using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebStore_GB.Domain.Entities.Identity;
using WevStore_GB.DAL.Context;

namespace WebStore_GB.Services.Data
{
    public class WebStoreDBInitializer
    {
        private readonly WebStoreDB _db;
        private readonly UserManager<User> _UserManager;
        private readonly RoleManager<Role> _RoleManager;
        private readonly ILogger<WebStoreDBInitializer> _logger;

        public WebStoreDBInitializer(WebStoreDB db, UserManager<User> UserManager, RoleManager<Role> RoleManager, ILogger<WebStoreDBInitializer> logger)
        {
            _db = db;
            _UserManager = UserManager;
            _RoleManager = RoleManager;
            _logger = logger;
        }

        public void Initialize()
        {
            var db = _db.Database;

            //if(db.EnsureDeleted())
            //    if(!db.EnsureCreated())
            //        throw new InvalidOperationException("Ошибка при создании базы данных товаров");

            if (db.GetPendingMigrations().Any())
            {
                _logger.LogInformation("Подготовка к выполнению миграции БД");
                db.Migrate();
                _logger.LogInformation("Миграция БД выполнена успешно");
            }


            InitializeEmployees();

            InitializeProducts();

            InitializeIdentityAsync().Wait();
        }

        private void InitializeEmployees()
        {
            var db = _db.Database;

            if (_db.Employees.Any()) return;

            using (db.BeginTransaction())
            {
                var employees = TestData.Employees.ToList();
                //foreach (var employee in employees)
                //    employee.Id = 0;
                employees.ForEach(e => e.Id = 0);

                _db.Employees.AddRange(employees);
            }
        }

        private void InitializeProducts()
        {
            var db = _db.Database;

            if (_db.Products.Any()) return;

            using (db.BeginTransaction())
            {
                _db.Sections.AddRange(TestData.Sections);

                db.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[ProductSection] ON");
                _db.SaveChanges();
                db.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[ProductSection] OFF");

                db.CommitTransaction();
            }

            using (var transaction = db.BeginTransaction())
            {
                _db.Brands.AddRange(TestData.Brands);

                db.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[ProductBrand] ON");
                _db.SaveChanges();
                db.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[ProductBrand] OFF");

                transaction.Commit();
            }

            using (var transaction = db.BeginTransaction())
            {
                _db.Products.AddRange(TestData.Products);


                db.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Products] ON");
                _db.SaveChanges();
                db.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Products] OFF");

                transaction.Commit();
            }
        }

        private async Task InitializeIdentityAsync()
        {
            if (!await _RoleManager.RoleExistsAsync(Role.ADMINISTRATOR))
                await _RoleManager.CreateAsync(new Role { Name = Role.ADMINISTRATOR });

            if (!await _RoleManager.RoleExistsAsync(Role.USER))
                await _RoleManager.CreateAsync(new Role { Name = Role.USER });

            if (await _UserManager.FindByNameAsync(User.ADMINISTRATOR) is null)
            {
                var admin = new User { UserName = User.ADMINISTRATOR };

                var create_result = await _UserManager.CreateAsync(admin, User.DEFAULTADMINPASSWORD);
                if (create_result.Succeeded)
                    await _UserManager.AddToRoleAsync(admin, Role.ADMINISTRATOR);
                else
                {
                    var errors = create_result.Errors.Select(e => e.Description);
                    throw new InvalidOperationException($"Ошибка при создании пользователя Администратора: {string.Join(",", errors)}");
                }
            }
        }
    }
}
