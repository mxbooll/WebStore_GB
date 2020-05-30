using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebStore_GB.Data;
using WebStore_GB.Models;

namespace WebStore_GB.Controllers
{
    public class EmployeesController : Controller
    {
        private static readonly List<Employee> _employees = TestData.Employees;

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