using System.Collections.Generic;
using System.Threading.Tasks;
using BPDotNet.Application.DTO.Request;
using BPDotNet.Application.DTO.Response;
using BPDotNet.Application.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace BPDotNet.API.Controllers
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

        /// <summary>
        /// Returns all users paginated
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetAllAsync()
        {
            return Ok(await _userService.GetAllAsync());
        }

        /// <summary>
        /// Returns on user per id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<UserResponseDto>> GetOneAsync(int id)
        {
            return Ok(await _userService.GetOneAsync(id));
        }

        /// <summary>
        /// Insert on user on database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<UserResponseDto>> Create(CreateUserRequestDto user)
        {
            var createdUser = await _userService.CreateAsync(user);
            return CreatedAtAction(nameof(GetOneAsync),new { id = createdUser.Id }, createdUser);
        }
    }
}