using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodDeliveryApi.Dtos.User;
using FoodDeliveryApi.Models;
using FoodDeliveryApi.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpGet("{id}", Name = "GetUserById")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> GetUserById(Guid id)
        {
            var response = await _userService.GetUserById(id);
            if (response.Data is null || !response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        
        [HttpPost(Name = "CreateUser")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> CreateUser(AddUserDto user)
        {
            var response = await _userService.CreateUser(user);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return CreatedAtRoute(nameof(GetUserById), new {Id = response.Data!.Id}, response);
        }
        
        [Authorize(Policy = "Customer")]
        [HttpPut("update", Name = "UpdateUser")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> UpdateUser(UpdateUserDto user)
        {
            var response = await _userService.UpdateUser(user);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [Authorize(Policy = "Customer")]
        [HttpDelete("{id}", Name = "DeleteUser")]
        public async Task<ActionResult<ServiceResponse<List<GetUserDto>>>> DeleteUser(Guid id)
        {
            var response = await _userService.DeleteUser(id);
            if (response.Data is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [Authorize(Policy = "Admin")]
        [HttpPatch("role/{id}", Name = "ChangeUserRole")]
        public async Task<ActionResult<ServiceResponse<List<GetUserDto>>>> ChangeUserRole(Guid id, string role)
        {
            var response = await _userService.ChangeUserRole(id, role);
            if (response.Data is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}
