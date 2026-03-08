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

        // Configure all DateTime properties to use UTC when interacting with the database
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                {
                    property.SetValueConverter(new Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<DateTime, DateTime>(
                        v => v.Kind == DateTimeKind.Unspecified ? DateTime.SpecifyKind(v, DateTimeKind.Utc) : v.ToUniversalTime(),
                        v => DateTime.SpecifyKind(v, DateTimeKind.Utc)));
                }
            }
        }

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