using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using WebStore_GB.Clients.Base;
using WebStore_GB.Domain;
using WebStore_GB.Domain.Entities.Employees;
using WebStore_GB.Interfaces.Services;

namespace WebStore_GB.Clients.Employees
{
    public class EmployeesClient : BaseClient, IEmployeesData
    {
        private readonly ILogger<EmployeesClient> _logger;

        public EmployeesClient(IConfiguration configuration, ILogger<EmployeesClient> logger) : base(configuration, WebApi.EMPLOYEES)
        {
            _logger = logger;
        }

        public IEnumerable<Employee> Get() => Get<IEnumerable<Employee>>(_serviceAddress);

        public Employee GetById(int id) => Get<Employee>($"{_serviceAddress}/{id}");

        public int Add(Employee employee)
        {
            try
            {
                _logger.LogInformation("Запрос к {0} на добавление сотрудника: {1}", _serviceAddress, employee.FirstName);
                return Post(_serviceAddress, employee).Content.ReadAsAsync<int>().Result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Ошибка при выполнении запроса к {0} на добавление сотрудника {1}: {2}", _serviceAddress, employee.Id, ex);
                throw new InvalidOperationException( $"Ошибка при выполнении запроса к {_serviceAddress} на добавление сотрудника {employee.FirstName}", ex);
            }
        }

        public void Edit(Employee employee)
        {
            try
            {
                _logger.LogInformation("Запрос к {0} на редактирование сотрудника id: {1}", _serviceAddress, employee.Id);
                Put(_serviceAddress, employee);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ошибка при выполнении запроса к {0} на редактирование сотрудника {1}: {2}", _serviceAddress, employee.Id, ex);
                throw new InvalidOperationException($"Ошибка при выполнении запроса к {_serviceAddress} на редактирование сотрудника {employee.FirstName}", ex);
            }
        }

        public bool Delete(int id) => Delete($"{_serviceAddress}/{id}").IsSuccessStatusCode;

        public void SaveChanges() { }
    }
}
