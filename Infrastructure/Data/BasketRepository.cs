

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
            return await _context.CustomerBaskets.Include(p => p.Items).FirstOrDefaultAsync(x => x.Id == basketId);
        }

        public async Task<CustomerBasket> GetCustomersBasketAsync(string email)
        {
            return await _context.CustomerBaskets.Include(p => p.Items).FirstOrDefaultAsync(x => x.UserEmail == email);
        }

        public async Task<CustomerBasket> DeleteCustomersBasketAsync(string email)
        {
            var basket = await _context.CustomerBaskets.FirstOrDefaultAsync(x => x.UserEmail == email);
            var removed =_context.CustomerBaskets.Remove(basket);
            _context.SaveChanges();
            if (removed == null) return null;

            return await GetBasketAsync(basket.Id);
        }

        public async Task<CustomerBasket> DeleteBasketItem(string userEmail, int itemId)
        {
            var basket = _context.CustomerBaskets
                .Include(b => b.Items)
                .FirstOrDefault(b => b.UserEmail == userEmail);

            if (basket != null)
            {
                var itemToRemove = basket.Items.FirstOrDefault(i => i.ProductId == itemId);

                if (itemToRemove != null)
                {
                    basket.Items.Remove(itemToRemove);
                    var itemEntity = _context.BasketItem.FirstOrDefault(i => i.ProductId == itemId && i.BasketId == basket.Id);
                    if (itemEntity != null)
                    {
                        _context.BasketItem.Remove(itemEntity);
                    }

                    _context.SaveChanges();

                    return await GetBasketAsync(basket.Id);
                }
            }
            return null;
        }

        public async Task<CustomerBasket> EmptyBasket(string userEmail)
        {
            var basket = _context.CustomerBaskets
                .Include(b => b.Items)
                .FirstOrDefault(b => b.UserEmail == userEmail);

            if (basket != null)
            {
                basket.Items.Clear();

                var itemsToRemove = _context.BasketItem
                    .Where(item => item.BasketId == basket.Id)
                    .ToList();

                _context.BasketItem.RemoveRange(itemsToRemove);

                _context.SaveChanges();
                return await GetBasketAsync(basket.Id);
            }
            return null;
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            _context.CustomerBaskets.Update(basket);
            await _context.SaveChangesAsync();
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