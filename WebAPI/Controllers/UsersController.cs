using Business.Abstract;
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

        [HttpPost("add")]
        public IActionResult Add(AuthDto authDto)
        {
            _userService.Add(authDto);
            return Ok();
        }

        [HttpGet("getList")]
        public IActionResult GetList()
        {
            return Ok(_userService.GetList());
        }
    }
}
