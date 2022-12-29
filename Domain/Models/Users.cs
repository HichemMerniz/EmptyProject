using Microsoft.AspNetCore.Identity;

namespace Domain.Models;

public class Users : IdentityUser<Guid>
{
    public string? Company { get; set; }
    public string? ProfileName { get; set; } = default!;
    public bool Active { get; set; } = true;

    //Todo: rolationship
    // public List<Incidents> Incidents { get; set; }
}