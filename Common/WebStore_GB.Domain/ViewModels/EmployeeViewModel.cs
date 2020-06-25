using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebStore_GB.Domain.ViewModels
{
    public class EmployeeViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Имя является обязательным")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Длина имени должна быть от 3 до 200 символов")]
        //[RegularExpression(@"([А-ЯЁ][а-яё]+)|([A-Z][a-z]+)", ErrorMessage = "Ошибка формата имени")]
        public string Name { get; set; }

        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Фамилия является обязательной")]
        [MinLength(3, ErrorMessage = "Длина фамилии должна быть больше 3 символов")]
        public string Surname { get; set; }

        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [Display(Name = "Возраст")]
        [Required]
        [Range(20, 80, ErrorMessage = "Возраст должен быть в пределах от 20 до 80 лет")]
        public int Age { get; set; }
    }
}
