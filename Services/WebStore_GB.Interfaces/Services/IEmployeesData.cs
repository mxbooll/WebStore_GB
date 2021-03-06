﻿using System.Collections.Generic;
using WebStore_GB.Domain.Entities.Employees;

namespace WebStore_GB.Interfaces.Services
{
    public interface IEmployeesData
    {
        IEnumerable<Employee> Get();

        Employee GetById(int id);

        int Add(Employee employee);

        void Edit(Employee employee);

        bool Delete(int id);

        void SaveChanges();
    }
}
