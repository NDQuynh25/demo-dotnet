using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

            return Ok(new ApiResponse<UserRes>(true, 200, string.Empty, "Create user successful", result));
        } catch (Exception ex) {
            return BadRequest(new ApiResponse<string>(false, 400, string.Empty, "Create user failed", ex.Message));
        }
    }

    [HttpGet("")]
    public async Task<IActionResult> GetUsers([FromQuery] PageReq pageReq) {
        try {
            var result = await _userService.GetUsers(pageReq);

            return Ok(new ApiResponse<Pagination<UserRes>>(true, 200, string.Empty, "Get users successful", result));
        } catch (Exception ex) {
            return BadRequest(new ApiResponse<string>(false, 400, string.Empty, "Get users failed", ex.Message));
        }
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] UserReq userReq) {
        try {
            var result = await _userService.UpdateUser(id, userReq, User);

            return Ok(new ApiResponse<UserRes>(true, 200, string.Empty, "Update user successful", result));
        } catch (Exception ex) {
            return BadRequest(new ApiResponse<string>(false, 400, string.Empty, "Update user failed", ex.Message));
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser([FromRoute] int id) {
        try {
            await _userService.DeleteUser(id, User);

            return Ok(new ApiResponse<string>(true, 200, string.Empty, "Delete user successful", string.Empty));
        } catch (Exception ex) {
            return BadRequest(new ApiResponse<string>(false, 400, string.Empty, "Delete user failed", ex.Message));
        }
    }
}

