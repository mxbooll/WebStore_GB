using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebStore_GB.Domain.Entities;
using WebStore_GB.Domain.Entities.Employees;
using WebStore_GB.Domain.Entities.Identity;

namespace WevStore_GB.DAL.Context
{
    public class WebStoreDB : IdentityDbContext<User, Role, string>
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Section> Sections { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public WebStoreDB(DbContextOptions<WebStoreDB> Options) : base(Options) { }
    }
}
