using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using netcore_server.DTOs.Request;
using netcore_server.DTOs.Response;

namespace netcore_server.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.JsonWebTokens;
using netcore_server.Services;
using netcore_server.Utils;


[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginReq request)
    {
        var result = await _authService.Login(request);
        var token = JwtUtils.GenerateToken(result.Id, result.Email, result.FullName);

        Response.Cookies.Append("token", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddDays(1)
        });

        return Ok(new ApiResponse<AuthRes>(true, 200, string.Empty, "Login successful", result));
    }

    // logout
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("token");
        return Ok(new ApiResponse<string>(true, 200, string.Empty, "Logout successful", string.Empty));
    }

    // me
    [Authorize]
    [HttpGet("me")]
    public IActionResult Me()
    {
        AuthRes authRes = new AuthRes(
            int.Parse(User.FindFirst("Id")?.Value ?? string.Empty),
            User.FindFirst("Email")?.Value ?? string.Empty,
            User.FindFirst("FullName")?.Value ?? string.Empty
        );
        return Ok(new ApiResponse<AuthRes>(true, 200, string.Empty, "Get user id successful", authRes));
    }

    [HttpGet("test")]
    public IActionResult Test()
    {
        return Ok(new ApiResponse<string>(true, 200, string.Empty, "Test successful", "Hello World!"));
    }
}

