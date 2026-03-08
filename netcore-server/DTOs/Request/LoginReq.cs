namespace netcore_server.DTOs.Request;
using System.ComponentModel.DataAnnotations;
public class LoginReq
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email invalid")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = string.Empty;
}