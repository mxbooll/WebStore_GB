using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebStore_GB.Domain;
using WebStore_GB.Domain.Entities.Employees;
using WebStore_GB.Interfaces.Services;

namespace WebStore_GB.ServiceHosting.Controllers
{
    //[Route("api/[controller]")]
    //[Route("api/employees")]
    [Route(WebApi.EMPLOYEES)]
    [ApiController]
    public class EmployeesApiController1 : ControllerBase, IEmployeesData
    {
        private readonly IEmployeesData _employeesData;

        public EmployeesApiController1(IEmployeesData employeesData) => _employeesData = employeesData;

        [HttpGet]
        public IEnumerable<Employee> Get() => _employeesData.Get();

        [HttpGet("{id}")]
        public Employee GetById(int id) => _employeesData.GetById(id);

        [HttpPost]
        public int Add([FromBody] Employee Employee) => _employeesData.Add(Employee);

        [HttpPut]
        public void Edit(Employee Employee) => _employeesData.Edit(Employee);

        //[HttpDelete("delete/{id}")] //http://localhost:5001/api/employees/delete/15
        [HttpDelete("{id}")]
        public bool Delete(int id) => _employeesData.Delete(id);

        //[HttpGet("Test/{Start}-{Stop}")] //http://localhost:5001/api/employees/Test/2005.05.07-2007.08.09
        //public ActionResult Test(DateTime Start, DateTime Stop) => Ok();

        public void SaveChanges() => _employeesData.SaveChanges();
    }
}
