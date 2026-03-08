using Microsoft.AspNetCore.Mvc;
using PhoneCompany.API.Extensions;
using PhoneCompany.Application.Interfaces.Public;
using PhoneCompany.Application.Requests.User;
using System.Security.Claims;

namespace PhoneCompany.API.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(
            [FromBody] LoginUserRequest loginUserRequest,
            CancellationToken cancellationToken)
        {
            var result = await _authService.LoginUserAsync(
                loginUserRequest, 
                cancellationToken);

            HttpContext.SetAuthTokens(
                result.RefreshToken, 
                result.AccessToken);

            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(
            [FromBody] RegisterUserRequest registerUserRequest,
            CancellationToken cancellationToken)
        {
            await _authService.RegisterUserAsync(
                registerUserRequest, 
                cancellationToken);

            return Ok();
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshUserTokens(CancellationToken cancellationToken)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _authService.RefreshUserTokens(
                Guid.Parse(userId),
                cancellationToken);

            HttpContext.SetAuthTokens(
                result.RefreshToken, 
                result.AccessToken);

            return Ok(result);
        }
    }
}
