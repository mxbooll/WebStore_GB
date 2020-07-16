using System.Collections.Generic;

namespace WebStore_GB.Domain.ViewModels
{
    public class SelectableSectionsViewModel
    {
        public IEnumerable<SectionViewModel> Sections { get; set; }

        public int? CurrentSectionId { get; set; }

        public int? ParentSectionId { get; set; }
    }
}
