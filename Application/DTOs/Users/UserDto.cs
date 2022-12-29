namespace Application.DTOs.Users;

public class UserDto
{
    public string FullName { get; set; }
    public string Mail { get; set; }
    public string? Gender { get; set; } = default!;
    public string Phone { get; set; }
    public string? Role { get; set; } = default!;
    public bool Active { get; set; }
    public string? ProfileName { get; set; } = default!;
    public string Company { get; set; }
}