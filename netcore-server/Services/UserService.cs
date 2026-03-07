using System.Security.Claims;
using netcore_server.DTOs.Request;
using netcore_server.DTOs.Response;
using netcore_server.Entities;
using netcore_server.Repositories;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository) {
        _userRepository = userRepository;
    }

    public Task<UserRes> GetUserById(int id) {
        try {
            var user = _userRepository.GetById(id).Result;
            if (user == null)
                throw new ArgumentException("User not found");
            
            return Task.FromResult(new UserRes(
                user.Id,
                user.Email,
                user.FullName ?? string.Empty,
                user.DateOfBirth?.ToString("yyyy-MM-dd") ?? string.Empty,
                user.PhoneNumber ?? string.Empty,
                user.Address ?? string.Empty
            ));
        } catch (Exception ex) {
            throw ex;
        }
    }
    
    public async Task<UserRes> UpdateUser(int id, UserReq userReq, ClaimsPrincipal? userAuth = null) {
        try {
            var userId = int.Parse(userAuth?.FindFirst("Id")?.Value ?? null);

            var user = _userRepository.GetById(id).Result;
            if (user == null)
                throw new ArgumentException("User not found");
            
            user.FullName = userReq.FullName;
            user.DateOfBirth = string.IsNullOrEmpty(userReq.DateOfBirth) ? null : DateTime.Parse(userReq.DateOfBirth);
            user.PhoneNumber = userReq.PhoneNumber;
            user.Address = userReq.Address;
            user.UpdatedAt = DateTime.Now;
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
        } catch (Exception ex) {
            throw ex;
        }
    }

    public async Task<UserRes> CreateUser(UserReq userReq, ClaimsPrincipal? userAuth = null) {
        var userId = int.Parse(userAuth?.FindFirst("Id")?.Value ?? null);
        
        try {
            var user = new User {
                Email = userReq.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("12345678"),
                FullName = userReq.FullName,
                DateOfBirth = string.IsNullOrEmpty(userReq.DateOfBirth) ? null : DateTime.Parse(userReq.DateOfBirth),
                PhoneNumber = userReq.PhoneNumber,
                Address = userReq.Address,
                CreatedAt = DateTime.Now,
                CreatedBy = userId,
                // UpdatedBy = id,
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
        } catch (Exception ex) {
            throw ex;
        }
    }
    
    public async Task<bool> DeleteUser(int id, ClaimsPrincipal? userAuth = null) {
        try {
            var user = _userRepository.GetById(id).Result;
            if (user == null)
                throw new ArgumentException("User not found");
            
            await _userRepository.Delete(id);
            
            return true;
        } catch (Exception ex) {
            throw ex;
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
              
        } catch (Exception ex) {
            throw ex;
        }
    }
}