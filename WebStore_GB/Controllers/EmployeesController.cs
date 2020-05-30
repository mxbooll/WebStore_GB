using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebStore_GB.Models;

namespace WebStore_GB.Controllers
{
    public class EmployeesController : Controller
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

        public IActionResult Index() => View(_employees);

        public IActionResult EmployeeDetails(int id)
        {
            var employee = _employees.FirstOrDefault(e => e.Id == id);
            if (employee is null)
                return NotFound();

            return View(employee);
        }
    }
}