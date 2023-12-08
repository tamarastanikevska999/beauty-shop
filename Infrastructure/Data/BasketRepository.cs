

using System.Text.Json;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace Infrastructure.Data
{
    public class BasketRepository : IBasketRepository
    {
        private readonly StoreContext _context;
        public BasketRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<CustomerBasket> GetBasketAsync(Guid basketId)
        {
            return await _context.CustomerBaskets.FirstOrDefaultAsync(x => x.Id == basketId);
        }

        public async Task<CustomerBasket> GetCustomersBasketAsync(string email)
        {
            return await _context.CustomerBaskets.FirstOrDefaultAsync(x => x.UserEmail == email);
        }

        public async Task<CustomerBasket> DeleteCustomersBasketAsync(string email)
        {
            var basket = await _context.CustomerBaskets.FirstOrDefaultAsync(x => x.UserEmail == email);
            var removed =_context.CustomerBaskets.Remove(basket);
            _context.SaveChanges();
            if (removed == null) return null;

            return await GetBasketAsync(basket.Id);
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            var updated = _context.CustomerBaskets.Update(basket);
            _context.SaveChanges();

            if (updated == null) return null;

            return await GetBasketAsync(basket.Id);
        }

        public async Task<CustomerBasket> CreateBasket(CustomerBasket basket)
        {
            var created =_context.CustomerBaskets.Add(basket);
            _context.SaveChanges();
            if (created == null) return null;

            return await GetBasketAsync(basket.Id);
        }
    }
}