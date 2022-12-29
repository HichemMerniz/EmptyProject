using Microsoft.AspNetCore.Identity;

namespace Domain.Models;

public class Roles : IdentityRole<Guid>
{
    public Roles()
    {
    }

    public Roles(string roleName) : base(roleName)
    {
    }


    // public ICollection<ApplicationUserRole> UserRoles { get; set; }
}