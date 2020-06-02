using Microsoft.AspNetCore.Mvc;
using System;
using WebStore_GB.Infrastructure.Interfaces;
using WebStore_GB.Models;
using WebStore_GB.ViewModels;

namespace WebStore_GB.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeesData _employeesData;

        public EmployeesController(IEmployeesData employeesData)
        {
            _employeesData = employeesData ?? throw new System.ArgumentNullException(nameof(employeesData));
        }

        public IActionResult Index() => View(_employeesData.Get());

        public IActionResult EmployeeDetails(int id)
        {
            var employee = _employeesData.GetById(id);
            if (employee is null)
                return NotFound();

            return View(employee);
        }

        #region Редактирование
        public IActionResult Edit(int? Id)
        {
            if (Id is null) { return View(new EmployeeViewModel()); }

            if (Id < 0) { return BadRequest(); }

            var employee = _employeesData.GetById((int)Id);
            if (employee is null) { return NotFound(); }

            return View(new EmployeeViewModel
            {
                Id = employee.Id,
                Surname = employee.Surname,
                Name = employee.FirstName,
                Patronymic = employee.Patronymic,
                Age = employee.Age
            });
        }

        [HttpPost]
        public IActionResult Edit(EmployeeViewModel model)
        {
            if (model is null) { throw new ArgumentNullException(nameof(model)); }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var employee = new Employee
            {
                Id = model.Id,
                FirstName = model.Name,
                Surname = model.Surname,
                Patronymic = model.Patronymic,
                Age = model.Age
            };

            if (model.Id == 0) { _employeesData.Add(employee); }
            else { _employeesData.Edit(employee); }

            _employeesData.SaveChanges();

            return RedirectToAction("Index");
        }
        #endregion

        #region Удаление
        public IActionResult Delete(int id)
        {
            if (id <= 0) { return BadRequest(); }

            var employee = _employeesData.GetById(id);
            if (employee is null) { return NotFound(); }

            return View(new EmployeeViewModel
            {
                Id = employee.Id,
                Surname = employee.Surname,
                Name = employee.FirstName,
                Patronymic = employee.Patronymic,
                Age = employee.Age
            });
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            _employeesData.Delete(id);
            _employeesData.SaveChanges();

            return RedirectToAction("Index");
        }
        #endregion
    }
}