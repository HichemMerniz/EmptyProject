namespace Application.DTOs.Users;

public class UserUpdateDto
{
    public string FullName { get; set; }
    public string Mail { get; set; }
    public string Phone { get; set; }
    public string Company { get; set; }
    public bool Active { get; set; }
    public string? ProfileName { get; set; }
    public string? Password { get; set; }
}