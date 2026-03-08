using System.ComponentModel.DataAnnotations;

namespace netcore_server.DTOs.Request;

public class UserReq
{
    [Required(ErrorMessage = "Full name is required")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email invalid")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Date of birth is required")]
    public string DateOfBirth { get; set; } = string.Empty;

    [Required(ErrorMessage = "Phone number is required")]
    [Phone(ErrorMessage = "Phone number invalid")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Address is required")]
    public string Address { get; set; } = string.Empty;
}