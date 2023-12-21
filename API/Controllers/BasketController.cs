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
        public async Task<ActionResult<string>> GetCustomersBasket(string email)
        {
            var basket = await _basketRepository.GetCustomersBasketAsync(email);

            if (basket == null)
            {
                return NotFound();
            }
            return Ok(basket);;
        }

        [HttpPut]
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

        [HttpDelete("{email}/empty")]
        public async Task<ActionResult<CustomerBasket>> EmptyBasket([FromRoute] string email)
        {
            try
            {
                var updatedBasket = await _basketRepository.EmptyBasket(email);
                return Ok(updatedBasket);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("{email}/delete/{productId}")]
        public async Task<ActionResult<CustomerBasket>> DeleteBasketItem([FromRoute]string email, [FromRoute]int productId)
        {
            try
            {
                var updatedBasket = await _basketRepository.DeleteBasketItem(email, productId);
                return Ok(updatedBasket);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}