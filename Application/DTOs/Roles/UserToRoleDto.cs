using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Roles;

public class UserToRoleDto
{
    [Required(ErrorMessage = "Email is a required field.")]
    public string Email { get; set; }
    [Required(ErrorMessage = "RoleName is a required field.")]
    public string RoleName { get; set; }
}