using AutoMapper;
using FoodDeliveryApi.Dtos.Shop;
using FoodDeliveryApi.Services.ShopService;
using FoodDeliveryApi.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShopController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IShopService _shopService;
        private readonly IUserService _userService;

        public ShopController(IMapper mapper, IShopService shopService, IUserService userService)
        {
            _mapper = mapper;
            _shopService = shopService;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<GetShopDto>>> GetShops()
        {
            var shops = await _shopService.GetAllShops();
            return Ok(shops);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<GetShopDto>> GetShopById(Guid id)
        {
            var shop = await _shopService.GetShopById(id);
            if (shop.Data is null)
                return NotFound(shop);
            return Ok(shop);
        }

        [Authorize(Policy = "Manager")]
        [HttpPost]
        public async Task<ActionResult<GetShopDto>> CreateShop(AddShopDto shop)
        {
            var createdShop = await _shopService.CreateShop(shop);
            return CreatedAtAction(nameof(GetShopById), new {id = createdShop.Data!.Id}, createdShop);
        }

        [Authorize(Policy = "Manager")]
        [HttpPut("{shopId}")]
        public async Task<ActionResult<GetShopDto>> UpdateShop(Guid shopId, UpdateShopDto shop)
        {
            var updatedShop = await _shopService.UpdateShop(shopId, shop);
            if (updatedShop.Data is null)
                return NotFound(updatedShop);
            return Ok(updatedShop);
        }

        [Authorize(Policy = "Manager")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<GetShopDto>> DeleteShop(Guid id)
        {
            var deletedShop = await _shopService.DeleteShop(id);
            if (deletedShop.Data is null)
                return NotFound(deletedShop);
            return Ok(deletedShop);
        }

        [Authorize(Policy = "Manager")]
        [HttpPatch("{shopId}/managers/{userId}")]
        public async Task<ActionResult<GetShopDto>> AddManager(Guid shopId, Guid userId)
        {
            var shop = await _shopService.AddManager(shopId, userId);
            if (shop.Data is null)
                return BadRequest(shop);
            return Ok(shop);
        }

        [Authorize(Policy = "Manager")]
        [HttpDelete("{shopId}/managers/{userId}")]
        public async Task<ActionResult<GetShopDto>> RemoveManager(Guid shopId, Guid userId)
        {
            var shop = await _shopService.RemoveManager(shopId, userId);
            if (shop.Data is null)
                return BadRequest(shop);
            return Ok(shop);
        }

    }
}