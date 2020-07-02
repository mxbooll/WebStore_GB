using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebStore_GB.Domain;
using WebStore_GB.Domain.Entities.Employees;
using WebStore_GB.Interfaces.Services;

namespace WebStore_GB.ServiceHosting.Controllers
{
    /// <summary>
    /// Управление сотрудниками
    /// </summary>
    //[Route("api/[controller]")]
    //[Route("api/employees")]
    [Route(WebApi.EMPLOYEES)]
    [ApiController]
    public class EmployeesApiController : ControllerBase, IEmployeesData
    {
        private readonly IEmployeesData _employeesData;

        public EmployeesApiController(IEmployeesData employeesData) => _employeesData = employeesData;

        /// <summary>
        /// Получить всех сотрудников.
        /// </summary>
        /// <returns> Перечисление сотрудников магазина. </returns>
        [HttpGet]
        public IEnumerable<Employee> Get() => _employeesData.Get();

        /// <summary>
        /// Получить сотрудника по его идентификатору.
        /// </summary>
        /// <param name="id"> Числовой идентификатор сотрудника. </param>
        /// <returns> Сотрудник с указанным идентификатором. </returns>
        [HttpGet("{id}")]
        public Employee GetById(int id) => _employeesData.GetById(id);

        /// <summary>
        /// Добавление нового сотрудника.
        /// </summary>
        /// <param name="Employee"> Добавляемый сотрудник. </param>
        /// <returns> Идентификатор, добавленного сотрудника. </returns>
        [HttpPost]
        public int Add([FromBody] Employee Employee)
        {
            var id = _employeesData.Add(Employee);
            SaveChanges();
            return id;
        }

        /// <summary>
        /// Редактирование данных сотрудника.
        /// </summary>
        /// <param name="Employee"> Редактируемый сотрудник. </param>
        [HttpPut]
        public void Edit(Employee Employee)
        {
            _employeesData.Edit(Employee);
            SaveChanges();
        }

        /// <summary>
        /// Удаление сотрудника по его идентификатору.
        /// </summary>
        /// <param name="id"> Идентификатор, удаляемого сотрудника. </param>
        /// <returns> Истина, если сотрудник присутствовал и был удален. </returns>
        //[HttpDelete("delete/{id}")] //http://localhost:5001/api/employees/delete/15
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            var succes = _employeesData.Delete(id);
            SaveChanges();
            return succes;
        }

        //[HttpGet("Test/{Start}-{Stop}")] //http://localhost:5001/api/employees/Test/2005.05.07-2007.08.09
        //public ActionResult Test(DateTime Start, DateTime Stop) => Ok();

        [NonAction]
        public void SaveChanges() => _employeesData.SaveChanges();
    }
}
