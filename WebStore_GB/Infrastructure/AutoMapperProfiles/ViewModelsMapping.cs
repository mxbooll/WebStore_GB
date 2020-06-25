using AutoMapper;
using WebStore_GB.Domain.Entities;
using WebStore_GB.Domain.Entities.Employees;
using WebStore_GB.ViewModels;

namespace WebStore_GB.Infrastructure.AutoMapperProfiles
{
    public class ViewModelsMapping : Profile
    {
        public ViewModelsMapping()
        {
            CreateMap<Product, ProductViewModel>()
               .ForMember(view_model => view_model.Brand, opt => opt.MapFrom(product => product.Brand.Name))
               .ReverseMap();

            CreateMap<Employee, EmployeeViewModel>()
               .ForMember(view_model => view_model.Name, opt => opt.MapFrom(employee => employee.FirstName))
               .ReverseMap();
        }
    }
}
