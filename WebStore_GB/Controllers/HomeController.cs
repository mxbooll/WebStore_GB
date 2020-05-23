using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebStore_GB.Models;

namespace WebStore_GB.Controllers
{
    public class HomeController : Controller
    {
        private static readonly List<Employee> _employees = new List<Employee>
        {
            new Employee
            {
                Id = 1,
                Surname = "Иванов",
                FirstName = "Иван",
                Patronymic = "Иванович",
                Age = 50
            },
            new Employee
            {
                Id = 2,
                Surname = "Петров",
                FirstName = "Пётр",
                Patronymic = "Петрович",
                Age = 25
            },
            new Employee
            {
                Id = 3,
                Surname = "Сидоров",
                FirstName = "Сидор",
                Patronymic = "Сидорович",
                Age = 30
            },
        };

        public IActionResult Index()
        {
            ViewBag.Title = "Hello world";
            return View(_employees);
        }

        public IActionResult Another()
        {
            return Content("Hello from Another action");
        }
    }
}