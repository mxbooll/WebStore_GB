using WebStore_GB.Domain.Entities;

namespace WebStore_GB.Domain.ViewModels
{
    public class BreadCrumbViewModel
    {
        public Section Section { get; set; }

        public Brand Brand { get; set; }

        public string Product { get; set; }
    }
}
