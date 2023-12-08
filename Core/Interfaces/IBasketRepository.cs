using Core.Entities;

namespace Core.Interfaces
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetBasketAsync(Guid basketId);
        Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);
        Task<CustomerBasket> DeleteCustomersBasketAsync(string email);
        Task<CustomerBasket> GetCustomersBasketAsync(string email);
        Task<CustomerBasket> CreateBasket(CustomerBasket basket);
    }
}