
using System;
using System.Threading.Tasks;
using netcore_server.DTOs.Request;
using netcore_server.DTOs.Response;
using netcore_server.Exceptions;
using netcore_server.Repositories;
using netcore_server.Utils.Constants;

namespace netcore_server.Services;


public class AuthService : IAuthService {
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<AuthRes> Login(LoginReq loginReq) {
        try {
            if (string.IsNullOrEmpty(loginReq.Email) || string.IsNullOrEmpty(loginReq.Password))
                throw new AppException("Email and password are required", 400);

            var user = await _userRepository.GetByEmail(loginReq.Email);
            if (user == null)
                throw new AppException("Email or password is incorrect", 401);
            
            if (!BCrypt.Net.BCrypt.Verify(loginReq.Password, user.PasswordHash))
                throw new AppException("Email or password is incorrect", 401);
            
            if (user.Role != RoleConstants.ADMIN)
                throw new AppException("You are not authorized to access this resource", 403);

            return new AuthRes(
                user.Id,
                user.Email,
                user.FullName ?? string.Empty
            );
            
        } catch (Exception ex) {
            throw ex;
        }
    }
}