using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebStore_GB.Domain.Entities.Base;
using WebStore_GB.Domain.Entities.Base.Interfaces;

namespace WebStore_GB.Domain.Entities
{
    //[Table("Products")]
    public class Product : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }

        public int SectionId { get; set; }

        [ForeignKey(nameof(SectionId))]
        public virtual Section Section { get; set; }

        public int? BrandId { get; set; }

        [ForeignKey(nameof(Brand))]
        public virtual Brand Brand { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Column(/*"ProductPrice"*/TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
    }
}
