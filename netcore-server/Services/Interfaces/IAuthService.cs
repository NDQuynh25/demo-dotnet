
using System.Threading.Tasks;
using netcore_server.DTOs.Request;
using netcore_server.DTOs.Response;

public interface IAuthService
{
    Task<AuthRes> Login(LoginReq request);
}