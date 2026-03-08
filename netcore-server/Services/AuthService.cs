
using System;
using System.Threading.Tasks;
using netcore_server.DTOs.Request;
using netcore_server.DTOs.Response;
using netcore_server.Exceptions;
using netcore_server.Repositories;

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
                throw new AppException("User not found", 404);
            
            if (!BCrypt.Net.BCrypt.Verify(loginReq.Password, user.PasswordHash))
                throw new AppException("Incorrect password", 400);

            Console.WriteLine($"User ID: {user.Id}");
            Console.WriteLine($"Email: {user.Email}");
            Console.WriteLine($"Full Name: {user.FullName}");

            return new AuthRes(
                user.Id,
                user.Email,
                user.FullName ?? string.Empty
            );
            
        } catch (Exception ex) {
            throw new AppException(ex.Message, 500);
        }
    }
}