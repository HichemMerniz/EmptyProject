using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Roles;

public class CreateRoleDto
{
    [Required(ErrorMessage = "Name is a required field.")]
    public string Name { get; set; }
    
    public string Description { get; set; }
}