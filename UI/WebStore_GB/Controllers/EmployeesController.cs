using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using WebStore_GB.Domain.Entities.Employees;
using WebStore_GB.Domain.Entities.Identity;
using WebStore_GB.Domain.ViewModels;
using WebStore_GB.Interfaces.Services;
using WebStore_GB.Services.Mapping;

namespace WebStore_GB.Controllers
{
    //[Route("NewRoute/[controller]/123")]
    //[Route("Staff")]
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly IEmployeesData _employeesData;

        public EmployeesController(IEmployeesData employeesData) => _employeesData = employeesData;

        //[Route("List")]
        public IActionResult Index() => View(_employeesData.Get());

        //[Route("{id}")]
        //[Authorize]
        public IActionResult EmployeeDetails(int id)
        {
            Employee employee = _employeesData.GetById(id);
            if (employee is null)
                return NotFound();

            return View(employee);
        }

        #region Редактирование
        [Authorize(Roles = Role.ADMINISTRATOR)]
        public IActionResult Edit(int? Id, [FromServices] IMapper mapper)
        {
            if (Id is null) { return View(new EmployeeViewModel()); }

            if (Id < 0) { return BadRequest(); }

            var employee = _employeesData.GetById((int)Id);
            if (employee is null) { return NotFound(); }

            return View(mapper.Map<EmployeeViewModel>(employee));
            //return View(employee.ToView());
        }

        [HttpPost]
        [Authorize(Roles = Role.ADMINISTRATOR)]
        public IActionResult Edit(EmployeeViewModel model, [FromServices] IMapper mapper)
        {
            if (model is null) { throw new ArgumentNullException(nameof(model)); }

            if (model.Name == "123" && model.Surname == "QWE")
            {
                ModelState.AddModelError(string.Empty, "Странное сочетание имени и фамилии");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var employee = mapper.Map<Employee>(model);
            //var employee = Model.FromView();

            if (model.Id == 0) { _employeesData.Add(employee); }
            else { _employeesData.Edit(employee); }

            _employeesData.SaveChanges();

            return RedirectToAction("Index");
        }
        #endregion

        #region Удаление
        [Authorize(Roles = Role.ADMINISTRATOR)]
        public IActionResult Delete(int id)
        {
            if (id <= 0) { return BadRequest(); }

            var employee = _employeesData.GetById(id);
            if (employee is null) { return NotFound(); }

            return View(employee.ToView());
        }

        [HttpPost]
        [Authorize(Roles = Role.ADMINISTRATOR)]
        public IActionResult DeleteConfirmed(int id)
        {
            _employeesData.Delete(id);
            _employeesData.SaveChanges();

            return RedirectToAction("Index");
        }
        #endregion
    }
}