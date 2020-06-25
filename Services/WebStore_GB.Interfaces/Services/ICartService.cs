using WebStore_GB.Domain.ViewModels;

namespace WebStore_GB.Interfaces.Services
{
    public interface ICartService
    {
        void AddToCart(int id);

        void DecrementFromCart(int id);

        void RemoveFromCart(int id);

        void RemoveAll();

        CartViewModel TransformFromCart();
    }
}
