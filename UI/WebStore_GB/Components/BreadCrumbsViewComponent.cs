using Microsoft.AspNetCore.Mvc;

namespace WebStore_GB.Components
{
    public class BreadCrumbsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }
}
 