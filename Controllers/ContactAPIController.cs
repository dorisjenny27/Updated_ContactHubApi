using AutoMapper;
using ContactHub.Helpers;
using ContactHub.Model.DTOs;
using ContactHub.Model.Entity;
using ContactHub.Services;
using ContactHub.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ContactHub.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ContactAPIController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IPhotoManager _photoManager;
        private readonly IUserService _userService;

        public ContactAPIController(UserManager<User> userManager,IPhotoManager photoManager, IUserService userService)
        {
            _userManager = userManager;
            _photoManager = photoManager;
            _userService = userService;
        }

        [HttpGet("Id")]
        public async Task <IActionResult> UserById(string Id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(Id);
                if (user == null)
                {
                    return NotFound($"No record found for user with id: {Id}");
                }
                
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("all")]
        [Authorize(Roles = "admin")]
        public IActionResult GetAllUsers(int page, int perPage)
        {
            try
            {
                var users = _userService.GetAllUsers(page, perPage);
                return Ok(users);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update/{Id}")]
        public async Task <IActionResult> UpdateUser(string Id, [FromBody] UserUpdateDTO model)
        {
            try
            {
                var user = await _userService.UpdateUserAsync(Id, model);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteUser(string Id)
        {
            try
            {
                await _userService.DeleteUserAsync(Id);
                return Ok("Deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("Updatephoto")]
        public async Task<IActionResult> AddPhoto(string Id, [FromForm] UserToUploadPhotoDTO model)
        {
            try
            {
                var user = await _photoManager.AddPhotoAsync(Id, model.Photo);
                return Ok(user);

    }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-photo")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeletePhoto(string Id)
        {
            try
            {
                var user = _photoManager.DeletePhotoAsync(Id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Search")]
        [Authorize(Roles = "regular, admin")]
        public IActionResult SearchUsers(int page, int perPage, string searchTerm)
        {
            try
            {
                var users = _userService.SearchUsers(page, perPage, searchTerm);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
