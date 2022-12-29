using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Users;

public class CreateUserDto
{
    [Required(ErrorMessage = "Username is required")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Company is required")]
    public string Company { get; set; }

    public bool Active { get; set; } 

    public string Phone { get; set; }

    public string ProfileName { get; set; }
}