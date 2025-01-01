using AgriToolWebApi.Application.Interfaces;
using AgriToolWebApi.Application.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.ComponentModel.DataAnnotations;

namespace AgriToolWebApi.API.Controllers
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

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] UserCreateRequest request)
        {
            try
            {
                var user = await _userService.CreateUserAsync(request);
                return Ok(user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { error = ex.Message });
            }
            catch (ValidationException ex)
            {
                return UnprocessableEntity(new { error = ex.Message });
            }
            catch (DbUpdateException ex)
            {
                return Conflict(new { error = ex.Message });
            }
            catch (MySqlException ex)
            {
                return StatusCode(500, new { error = "Database error: " + ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "予期しないエラーが発生しました。" });
            }
        }
    }
}
