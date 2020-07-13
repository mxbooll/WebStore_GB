using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore_GB.Domain.ViewModels;
using WebStore_GB.Interfaces.Services;

namespace WebStore_GB.Components
{
    public class SectionsViewComponent : ViewComponent
    {
        private readonly IProductData _ProductData;

        public SectionsViewComponent(IProductData ProductData)
        {
            _ProductData = ProductData;
        }

        public IViewComponentResult Invoke(string sectionId)
        {
            var id = int.TryParse(sectionId, out var i) ? i : (int?)null;

            var sections = GetSections(id, out var parentSectionId);

            return View(new SelectableSectionsViewModel
            {
                Sections = sections,
                CurrentSectionId = id,
                ParentSectionId = parentSectionId
            });
        }

        //public async Task<IViewComponentResult> Invoke() => View();

        private IEnumerable<SectionViewModel> GetSections(int? sectionId, out int? parentSectionId)
        {
            parentSectionId = null;

            var sections = _ProductData.GetSections();

            var parent_sections = sections.Where(s => s.ParentId is null);

            var parent_Sections_views = parent_sections
               .Select(s => new SectionViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Order = s.Order,
                })
               .ToList();

            foreach (var parentSection in parent_Sections_views)
            {
                var childs = sections.Where(s => s.ParentId == parentSection.Id);

                foreach (var childSection in childs)
                {
                    if (childSection.Id == sectionId)
                    {
                        parentSectionId = parentSection.Id;
                    }
                    parentSection.ChildSections.Add(new SectionViewModel
                    {
                        Id = childSection.Id,
                        Name = childSection.Name,
                        Order = childSection.Order,
                        ParentSection = parentSection
                    });
                }

                parentSection.ChildSections.Sort((a, b) => Comparer<double>.Default.Compare(a.Order, b.Order));
            }

            parent_Sections_views.Sort((a, b) => Comparer<double>.Default.Compare(a.Order, b.Order));

            return parent_Sections_views;
        }
    }
}
