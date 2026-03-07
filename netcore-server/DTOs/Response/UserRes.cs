
namespace netcore_server.DTOs.Response;

public class UserRes
{
    public UserRes()
    {
    }

    public UserRes(int id, string email, string fullName, string? dateOfBirth, string? phoneNumber, string? address)
    {
        Id = id;
        Email = email;
        FullName = fullName;
        DateOfBirth = dateOfBirth;
        PhoneNumber = phoneNumber;
        Address = address;
    }

    public int Id { get; set; }

    public string Email { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public string? DateOfBirth { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }
}
