namespace netcore_server.DTOs.Request;

public class LoginReq
{
    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}