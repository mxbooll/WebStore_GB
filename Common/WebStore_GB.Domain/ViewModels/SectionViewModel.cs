using System.Collections.Generic;
using WebStore_GB.Domain.Entities.Base.Interfaces;

namespace WebStore_GB.Domain.ViewModels
{
    public class SectionViewModel : INamedEntity, IOrderedEntity
    {
        public int Id { get; set; }

        public int Order { get; set; }

        public string Name { get; set; }

        public List<SectionViewModel> ChildSections { get; set; } = new List<SectionViewModel>();

        public SectionViewModel ParentSection { get; set; }
    }
}
