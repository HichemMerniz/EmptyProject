using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Roles;

public class UserUpdateRoleDto
{
    [Required(ErrorMessage = "Email is a required field.")]
    public string Email { get; set; }
    [Required(ErrorMessage = "RoleName is a required field.")]
    public string OldRoleName { get; set; }
    [Required(ErrorMessage = "RoleName is a required field.")]
    public string NewRoleName { get; set; }
}