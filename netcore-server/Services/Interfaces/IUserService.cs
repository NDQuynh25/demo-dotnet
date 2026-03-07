
using System.Security.Claims;
using System.Threading.Tasks;
using netcore_server.DTOs.Request;
using netcore_server.DTOs.Response;

public interface IUserService
{
    Task<UserRes> GetUserById(int id);
    Task<UserRes> UpdateUser(int id, UserReq userReq, ClaimsPrincipal? userAuth = null);
    Task<UserRes> CreateUser(UserReq userReq, ClaimsPrincipal? userAuth = null);
    Task<bool> DeleteUser(int id, ClaimsPrincipal? userAuth = null);
    Task<Pagination<UserRes>> GetUsers(PageReq pageReq);
}