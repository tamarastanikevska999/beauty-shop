using API.DTO;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly AutoMapper.IMapper _mapper;

        public ProductsController ( IProductRepository repo, AutoMapper.IMapper mapper)
        {
            _productRepository = repo;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(PagedProductsDto), 200)]
        public async Task<ActionResult<PagedProductsDto>> GetProducts(
            [FromQuery(Name = "id")] int id,
            [FromQuery(Name = "from-amount")] int fromAmount,
            [FromQuery(Name = "to-amount")] int toAmount,
            [FromQuery(Name = "name")] string name,
            [FromQuery(Name = "type")] string type,
            [FromQuery(Name = "brand")] string brand,
            [FromQuery(Name = "page-size")] int pageSize,
            [FromQuery(Name = "page")] int page, 
            [FromQuery(Name = "sort-by")] string sortBy,
            [FromQuery(Name = "sort-order")] string sortOrder
        )
        {
            var sorting = new Sorting(sortOrder, sortBy);
            var paging = new Paging(pageSize, page);
            var response = await _productRepository.GetProductsAsync(id, fromAmount, toAmount, name, type, brand, paging, sorting);
            PagedProductsDto products = _mapper.Map<PagedProductsDto>(response);
            return StatusCode(200, products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            return Ok(_mapper.Map<Product, ProductDto>(product));
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands([FromQuery(Name = "name")] string name)
        {
            return Ok(await _productRepository.GetProductBrandsAsync(name));
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductTypes([FromQuery(Name = "name")] string name)
        {
            return Ok(await _productRepository.GetProductTypesAsync(name));
        }
        
    }
}