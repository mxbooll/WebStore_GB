using System.Collections.Generic;
using WebStore_GB.Models;

namespace WebStore_GB.Infrastructure.Interfaces
{
    public interface IEmployeesData
    {
        IEnumerable<Employee> Get();

        Employee GetById(int id);

        int Add(Employee Employee);

        void Edit(Employee Employee);

        bool Delete(int id);

        void SaveChanges();
    }
}
