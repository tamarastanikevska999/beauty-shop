using System.Security.Claims;
using API.DTO;
using Core.Entities;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("basket")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        private readonly UserManager<ShopUser> _userManager;
        public BasketController(UserManager<ShopUser> userManager,IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
            _userManager = userManager;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerBasket>> GetBasketById(Guid id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);

            return Ok(basket);
        }

        [HttpGet("user/{email}")]
        [Authorize]
        public async Task<ActionResult<string>> GetCustomersBasket(string email)
        {
            var user =await _userManager.FindByEmailAsync(email);
            var basket = await _basketRepository.GetCustomersBasketAsync(user.Email);

            if (basket == null)
            {
                return NotFound();
            }
            return Ok(basket);;
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
        {
            var updatedBasket = await _basketRepository.UpdateBasketAsync(basket);

            return Ok(updatedBasket);
        }

        [HttpDelete("{email}")]
        public async Task DeleteBasketAsync(string email)
        {
            await _basketRepository.DeleteCustomersBasketAsync(email);
        }
    }
}