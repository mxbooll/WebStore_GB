using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebStore_GB.Domain.Entities;
using WebStore_GB.Domain.Entities.Identity;
using WebStore_GB.Interfaces.Services;
using WebStore_GB.Services.Mapping;

namespace WebStore_GB.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = Role.ADMINISTRATOR)]
    public class CatalogController : Controller
    {
        private readonly IProductData _ProductData;

        public CatalogController(IProductData ProductData) => _ProductData = ProductData;

        public IActionResult Index() => View(_ProductData.GetProducts().Select(p => p.FromDTO()));

        public IActionResult Edit(int? id)
        {
            var product = id is null
                ? new Product()
                : _ProductData.GetProductById((int)id).FromDTO();

            if (product is null)
                return NotFound();

            return View(product);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(Product product) => RedirectToAction(nameof(Index));

        public IActionResult Delete(int id)
        {
            var product = _ProductData.GetProductById(id);

            if (product is null)
                return NotFound();

            return View(product.FromDTO());
        }

        [HttpPost, ValidateAntiForgeryToken, ActionName(nameof(Delete))]
        public IActionResult DeleteConfirm(int id) => RedirectToAction(nameof(Index));
    }
}