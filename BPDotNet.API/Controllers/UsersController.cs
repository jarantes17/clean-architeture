using BPDotNet.Application.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace BPDotNet.API.Controllers
{
    [Route("api/{controller}")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(this._userService.GetAll());
        }
    }
}