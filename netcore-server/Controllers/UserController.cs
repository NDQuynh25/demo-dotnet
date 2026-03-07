using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using netcore_server.DTOs.Request;
using netcore_server.DTOs.Response;

namespace netcore_server.Controllers;

[Authorize]
[ApiController]
[Route("users")]
public class UserController : ControllerBase
{

    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("")]
    public async Task<IActionResult> CreateUser([FromBody] UserReq userReq) {
     
      
        try {
            var result = await _userService.CreateUser(userReq, User);

            return Ok(new ApiResponse<UserRes>(true, 200, string.Empty, "Login successful", result));
        } catch (Exception ex) {
            return BadRequest(new ApiResponse<string>(false, 400, string.Empty, "Login failed", ex.Message));
        }
    }

    [HttpGet("test")]
    public IActionResult Test()
    {
        return Ok(new ApiResponse<string>(true, 200, string.Empty, "Test successful", "Hello World!"));
    }
}

