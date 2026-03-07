using Microsoft.EntityFrameworkCore;
using netcore_server.Database;
using netcore_server.Entities;

namespace netcore_server.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByEmail(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<User?> GetById(int id)
    {
        return await _context.Users
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<User> Create(User user)
    {
        _context.Users.Add(user);

        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<User> Update(User user)
    {
        _context.Users.Update(user);

        await _context.SaveChangesAsync();

        return user;
    }

    public async Task Delete(int id)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Id == id);

        if (user is null)
        {
            throw new Exception("User not found");
        }

        user.IsDeleted = true;
        user.UpdatedAt = DateTime.Now;
        _context.Users.Update(user);

        await _context.SaveChangesAsync();
    }

    public async Task<Pagination<User>> GetUsers(int page, int limit, string? search)
    {
        var query = _context.Users
            .Where(x => !x.IsDeleted &&
                (x.FullName.Contains(search ?? "") ||
                    x.Email.Contains(search ?? "")));

        var totalItems = await query.CountAsync();

        var items = await query
            .Skip((page - 1) * limit)
            .Take(limit)
            .ToListAsync();

        return new Pagination<User>
        {
            Elements = items,
            Page = page,
            PageSize = limit,
            TotalItems = totalItems,
            TotalPages = (int)Math.Ceiling((double)totalItems / limit)
        };
    }

}