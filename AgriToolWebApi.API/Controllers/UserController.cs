using AgriToolWebApi.Application.Exceptions;
using AgriToolWebApi.Application.Interfaces;
using AgriToolWebApi.Application.Requests.Users;
using AgriToolWebApi.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.ComponentModel.DataAnnotations;

namespace AgriToolWebApi.API.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("ユーザ登録のリクエストを受付ました。");
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
            catch (UserCreationException ex)
            {
                return Conflict(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "予期しないエラーが発生しました。" });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateRequest request)
        {
            _logger.LogInformation($"ユーザ更新リクエストを受け付けました。UserId: {request.Id}");

            try
            {
                var updatedUser = await _userService.UpdateUserAsync(request);
                return Ok(updatedUser); // 200 OK と更新後のユーザ情報を返す
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning($"ユーザ更新失敗: {ex.Message}");
                return NotFound(new { Message = ex.Message }); // 404 Not Found を返す
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser([FromQuery] UserDeleteRequest request)
        {
            _logger.LogInformation($"ユーザ削除リクエストを受け付けました。UserId: {request.Id}");

            try
            {
                await _userService.DeleteUserAsync(request);
                return NoContent(); // 204 No Content を返す
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning($"ユーザ削除失敗: {ex.Message}");
                return NotFound(new { Message = ex.Message }); // 404 Not Found を返す
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUserById([FromQuery] UserDetailRequest request)
        {
            _logger.LogInformation($"ユーザ詳細取得リクエストを受け付けました。UserId: {request.Id}");

            try
            {
                var user = await _userService.GetUserByIdAsync(request);
                return Ok(user); // 200 OK とユーザ詳細を返す
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning($"ユーザ詳細取得失敗: {ex.Message}");
                return NotFound(new { Message = ex.Message }); // 404 Not Found を返す
            }
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetUserList([FromQuery] UserSearchRequest request)
        {
            _logger.LogInformation("ユーザ一覧取得リクエストを受け付けました。");

            var users = await _userService.GetUserListAsync(request);
            return Ok(users); // 200 OK とユーザ一覧を返す
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginRequest request)
        {
            _logger.LogInformation("ログインリクエストを受け付けました。");

            try
            {
                var user = await _userService.AuthenticateAsync(request);
                return Ok(user); // 認証成功時は 200 OK とユーザ情報を返す
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning($"認証失敗: {ex.Message}");
                return Unauthorized(new { Message = ex.Message }); // 認証失敗時は 401 Unauthorized を返す
            }
            catch (Exception ex)
            {
                _logger.LogError($"ログイン処理中にエラーが発生しました: {ex.Message}");
                return StatusCode(500, new { Message = "内部エラーが発生しました。" }); // サーバーエラー時は 500 Internal Server Error を返す
            }
        }
    }
}
