using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WebStore_GB.Domain.Entities.Base;
using WebStore_GB.Domain.Entities.Base.Interfaces;

namespace WebStore_GB.Domain.Entities
{
    [Table("ProductBrand")]
    public class Brand : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
