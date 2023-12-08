using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using API.DTO;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("account")]
    public class AccountController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        private readonly UserManager<ShopUser> _userManager;
        private readonly SignInManager<ShopUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        public AccountController(UserManager<ShopUser> userManager, SignInManager<ShopUser> signInManager,
            ITokenService tokenService, IMapper mapper,IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            var user = await _userManager.FindByEmailAsync(email);

            return new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                Username = user.UserName
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login([Required][FromBody]LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) return Unauthorized();

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized();

            return new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                Username = user.UserName
            };
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register([Required][FromBody]RegisterDto registerDto)
        {
            if (CheckEmailExistsAsync(registerDto.Email).Result.Value)
            {
                return new BadRequestObjectResult("Email address is in use");
            }
            var user = new ShopUser
            {
                Email = registerDto.Email,
                UserName = registerDto.Username,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            var basket = new CustomerBasket
            {
                UserEmail = user.Email,
            };
            
            var basketId =_basketRepository.CreateBasket(basket);
            if (!result.Succeeded) return BadRequest();

            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user),
                Email = user.Email
            };
        }

        [HttpGet("check-email")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([Required][FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        [HttpGet("address")]
        [Authorize]
        public async Task<ActionResult<string>> GetUserAddress()
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            var user = await _userManager.FindByEmailAsync(email);

            return user.Address;
        }

        [HttpPut("address")]
        [Authorize]
        public async Task<ActionResult<string>> UpdateUserAddress([Required][FromQuery] string address)
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            var user = await _userManager.FindByEmailAsync(email);

            user.Address = address;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded) return Ok(user.Address);

            return BadRequest("Problem updating user's address");
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<string>> UpdateUser([Required][FromBody] UserInfoDto userInfo)
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            var user = await _userManager.FindByEmailAsync(email);
            if(!String.IsNullOrEmpty(userInfo.FirstName)){
                user.FirstName = userInfo.FirstName;
            }
            if(!String.IsNullOrEmpty(userInfo.LastName)){
                user.LastName = userInfo.LastName;
            }
            if(!String.IsNullOrEmpty(userInfo.Address)){
                user.Address = userInfo.Address;
            }

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded) return Ok();

            return BadRequest("Problem updating the user");
        }

    }
}