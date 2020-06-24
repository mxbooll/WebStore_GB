using WebStore_GB.Domain.Entities.Employees;
using WebStore_GB.Domain.ViewModels;

namespace WebStore_GB.Infrastructure.Mapping
{
    public static class EmployeeMapper
    {
        public static EmployeeViewModel ToView(this Employee e) => new EmployeeViewModel
        {
            Id = e.Id,
            Name = e.FirstName,
            Surname = e.Surname,
            Patronymic = e.Patronymic,
            Age = e.Age
        };

        public static Employee FromView(this EmployeeViewModel e) => new Employee
        {
            Id = e.Id,
            FirstName = e.Name,
            Surname = e.Surname,
            Patronymic = e.Patronymic,
            Age = e.Age
        };
    }
}
