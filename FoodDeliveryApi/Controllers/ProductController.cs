using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodDeliveryApi.Dtos.Product;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryApi.Controllers
{
    [ApiController]
    [Route("api/shop")]
    public class ProductController : ControllerBase
    {
        private readonly IShopService _shopService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IShopService shopService, IProductService productService, IMapper mapper)
        {
            _shopService = shopService;
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet("{shopId}/products")]
        public async Task<ActionResult<List<GetProductDto>>> GetProducts(Guid shopId)
        {
            return Ok(await _productService.GetProducts(shopId));
        }

        [HttpGet("{shopId}/products/{productId}")]
        public async Task<ActionResult<GetProductDto>> GetProductById(Guid shopId, Guid productId)
        {
            var response = await _productService.GetProductById(shopId, productId);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost("{shopId}/products")]
        public async Task<ActionResult<GetProductDto>> AddProduct(Guid shopId, AddProductDto product)
        {
            var response = await _productService.AddProduct(shopId, product);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("{shopId}/products/{productId}")]
        public async Task<ActionResult<GetProductDto>> AddProductById(Guid shopId, Guid productId)
        {
            var response = await _productService.AddProductById(shopId, productId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("{shopId}/products/{productId}")]
        public async Task<ActionResult<List<GetProductDto>>> RemoveProduct(Guid shopId, Guid productId)
        {
            var response = await _productService.RemoveProduct(shopId, productId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut("{shopId}/products/{productId}")]
        public async Task<ActionResult<GetProductDto>> UpdateProduct(Guid shopId, Guid productId, UpdateProductDto product)
        {
            var response = await _productService.UpdateProduct(shopId, productId, product);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}