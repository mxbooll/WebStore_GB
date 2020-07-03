using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebStore_GB.Domain.Entities.Base.Interfaces;

namespace WebStore_GB.Domain.Entities.Base
{
    /// <summary> Базовая сущность. </summary>
    public abstract class BaseEntity : IBaseEntity
    {
        /// <summary> Идентификатор </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}
