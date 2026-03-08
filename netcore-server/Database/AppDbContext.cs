using Microsoft.EntityFrameworkCore;
using netcore_server.Entities;

namespace netcore_server.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Email = "admin@gmail.com",
                FullName = "Admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                IsDeleted = false
            }
        );
    }
}