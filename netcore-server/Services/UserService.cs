using System.Security.Claims;
using netcore_server.DTOs.Request;
using netcore_server.DTOs.Response;
using netcore_server.Entities;
using netcore_server.Exceptions;
using netcore_server.Repositories;
using netcore_server.Utils.Constants;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository) {
        _userRepository = userRepository;
    }

    public async Task<UserRes> GetUserById(int id) {
        try {
            var user = await _userRepository.GetById(id);
            if (user == null)
                throw new AppException("User not found", 404);
            
            return new UserRes(
                user.Id,
                user.Email,
                user.FullName ?? string.Empty,
                user.DateOfBirth?.ToString("yyyy-MM-dd") ?? string.Empty,
                user.PhoneNumber ?? string.Empty,
                user.Address ?? string.Empty
            );
        } catch (Exception) {
            throw;
        }
    }
    
    public async Task<UserRes> UpdateUser(int id, UserReq userReq, ClaimsPrincipal? userAuth = null) {
        try {
            var userIdStr = userAuth?.FindFirst("id")?.Value;
            var userId = string.IsNullOrEmpty(userIdStr) ? 0 : int.Parse(userIdStr);

            var user = await _userRepository.GetById(id);
            if (user == null)
                throw new AppException("User not found", 404);
            
            user.FullName = userReq.FullName;
            user.DateOfBirth = string.IsNullOrEmpty(userReq.DateOfBirth) ? null : DateTime.SpecifyKind(DateTime.Parse(userReq.DateOfBirth), DateTimeKind.Utc);
            user.PhoneNumber = userReq.PhoneNumber;
            user.Address = userReq.Address;
            user.UpdatedAt = DateTime.UtcNow;
            user.UpdatedBy = userId;
            
            await _userRepository.Update(user);
            
            return new UserRes(
                user.Id,
                user.Email,
                user.FullName ?? string.Empty,
                user.DateOfBirth?.ToString("yyyy-MM-dd") ?? string.Empty,
                user.PhoneNumber ?? string.Empty,
                user.Address ?? string.Empty
            );
        } catch (Exception) {
            throw;
        }
    }

    public async Task<UserRes> CreateUser(UserReq userReq, ClaimsPrincipal? userAuth = null) {
        var userIdStr = userAuth?.FindFirst("id")?.Value;
        var userId = string.IsNullOrEmpty(userIdStr) ? 0 : int.Parse(userIdStr);
        
        try {
            var existingUser = await _userRepository.GetByEmail(userReq.Email);
            if (existingUser != null)
                throw new AppException("Email already exists", 400);

            var user = new User {
                Email = userReq.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("12345678"),
                FullName = userReq.FullName,
                DateOfBirth = string.IsNullOrEmpty(userReq.DateOfBirth) ? null : DateTime.SpecifyKind(DateTime.Parse(userReq.DateOfBirth), DateTimeKind.Utc),
                PhoneNumber = userReq.PhoneNumber,
                Address = userReq.Address,
                Role = RoleConstants.USER,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = userId,
            };
           
            
            await _userRepository.Create(user);
           
            
            return new UserRes(
                user.Id,
                user.Email,
                user.FullName ?? string.Empty,
                user.DateOfBirth?.ToString("yyyy-MM-dd") ?? string.Empty,
                user.PhoneNumber ?? string.Empty,
                user.Address ?? string.Empty
            );
        } catch (Exception) {
            throw;
        }
    }
    
    public async Task<bool> DeleteUser(int id, ClaimsPrincipal? userAuth = null) {
        try {
            var user = await _userRepository.GetById(id);
            if (user == null)
                throw new AppException("User not found", 404);
            
            await _userRepository.Delete(id);
            
            return true;
        } catch (Exception) {
            throw;
        }
    }

    public async Task<Pagination<UserRes>> GetUsers(PageReq pageReq) {
        try {
            var users = await _userRepository.GetUsers(
                pageReq.Page,
                pageReq.Limit,
                pageReq.Search
            );
            return new Pagination<UserRes>
            {
                Elements = users.Elements.Select(u => new UserRes(
                    u.Id,
                    u.Email,
                    u.FullName ?? string.Empty,
                    u.DateOfBirth?.ToString("yyyy-MM-dd") ?? string.Empty,
                    u.PhoneNumber ?? string.Empty,
                    u.Address ?? string.Empty
                )).ToList(),
                Page = pageReq.Page,
                PageSize = pageReq.Limit,
                TotalItems = users.TotalItems,
                TotalPages = users.TotalPages
            };
              
        } catch (Exception) {
            throw;
        }
    }
}