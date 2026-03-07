
namespace netcore_server.DTOs.Response;

public class AuthRes
{
    public AuthRes()
    {
    }

    public AuthRes(int id, string email, string fullName)
    {
        Id = id;
        Email = email;
        FullName = fullName;
    }

    public int Id { get; set; }

    public string Email { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;
}
