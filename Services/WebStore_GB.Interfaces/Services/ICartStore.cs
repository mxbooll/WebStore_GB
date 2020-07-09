using WebStore_GB.Domain.Models;

namespace WebStore_GB.Interfaces.Services
{
    public interface ICartStore
    {
        public Cart Cart { get; set; }
    }
}
