using Business.Repositories.UserRepository;
using Domain.Dtos;
using Domain.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("getList")]
        public IActionResult GetList()
        {
            var result = _userService.GetList();
            if (result.Success) 
                return Ok(result);
            
            return BadRequest(result);
        }

        [HttpGet("getByEmail")]
        public IActionResult GetByEmail(string email)
        {
            var result = _userService.GetByEmail(email);
            if (result.Success) 
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("getById")]
        public IActionResult GetByUserId(int userId)
        {
            var result = _userService.GetByUserId(userId);
            if (result.Success) 
                return Ok(result);

            return BadRequest(result);  
        }

        [HttpPut("update")]
        public IActionResult Update(User user)
        {
            var result = _userService.Update(user);
            if (result.Success) 
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(User user)
        {
            var result = _userService.Delete(user);
            if (result.Success) 
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("deleteById")]
        public IActionResult DeleteById(int userId)
        {
            var result = _userService.DeleteById(userId);
            if (result.Success) 
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("changePassword")]
        public IActionResult ChangePassword(UserChangePasswordDto userChangePasswordDto)
        {
            var result = _userService.ChangePassword(userChangePasswordDto);
            if (result.Success) 
                return Ok(result);

            return BadRequest(result);
        }
    }
}
