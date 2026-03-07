using netcore_server.Entities;

namespace netcore_server.Repositories;

public interface IUserRepository
{
    Task<User?>  GetByEmail(string email);

    Task<User?> GetById(int id);

    Task<User> Create(User user);

    Task<User> Update(User user);

    Task Delete(int id);
}