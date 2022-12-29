using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Roles;

public class RoleClaimsDto
{
    [Required(ErrorMessage = "Name is a required field.")]
    public IEnumerable<string> Claims { get; set; }
}