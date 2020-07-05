using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<EmployeesApiController> _logger;

        public EmployeesApiController(IEmployeesData employeesData, ILogger<EmployeesApiController> logger)
        {
            _employeesData = employeesData;
            _logger = logger;
        }

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
        /// <param name="employee"> Добавляемый сотрудник. </param>
        /// <returns> Идентификатор, добавленного сотрудника. </returns>
        [HttpPost]
        public int Add([FromBody] Employee employee)
        {
            _logger.LogInformation("Добавление нового сотрудника: [{0}]{1} {2} {3}", employee.Id, employee.Surname, employee.FirstName, employee.Patronymic);
            var id = _employeesData.Add(employee);
            SaveChanges();
            return id;
        }

        /// <summary>
        /// Редактирование данных сотрудника.
        /// </summary>
        /// <param name="employee"> Редактируемый сотрудник. </param>
        [HttpPut]
        public void Edit(Employee employee)
        {
            _logger.LogInformation("Редактирование сотрудника: [{0}]{1} {2} {3}", employee.Id, employee.Surname, employee.FirstName, employee.Patronymic);
            _employeesData.Edit(employee);
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
            _logger.LogInformation("Удаление сотрудника id:{0}", id);
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
