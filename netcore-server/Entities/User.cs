using System;
using Microsoft.EntityFrameworkCore;
namespace netcore_server.Entities;


[Index(nameof(Email), IsUnique = true)]
public class User
{
    public int Id { get; set; }

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public string? FullName { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

    public int? CreatedBy { get; set; } = null;

    public int? UpdatedBy { get; set; } = null;

    public bool IsDeleted { get; set; } = false;
}