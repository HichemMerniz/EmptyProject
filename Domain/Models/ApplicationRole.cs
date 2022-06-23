using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Domain.Models;

public class ApplicationRole : IdentityRole<Guid>
{
    public ApplicationRole() :
        base()
    {
    }

    public ApplicationRole(string roleName) :
        base(roleName)
    {
    }

    public ApplicationRole(
        string roleName,
        string description,
        DateTime creationDate
    ) :
        base(roleName)
    {
        Description = description;
        CreatedDate = creationDate;
    }

    public string Description { get; set; }

    public DateTime CreatedDate { get; set; }
}

public class RoleCreateDto
{
    [Required(ErrorMessage = "Name is a required field.")]
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
}

public class RoleUpdateDto
{
    [Required(ErrorMessage = "Name is a required field.")]
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
}

public class RoleClaimsUpdateDto
{
    [Required(ErrorMessage = "Name is a required field.")]
    public IEnumerable<string> Claims { get; set; }
}