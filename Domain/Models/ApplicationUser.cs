using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Domain.Models;

public class ApplicationUser : IdentityUser<Guid>
  {
    [NotMapped]
    public string ProfileName { get; set; }
    public string FullName { get; set; }
    public string Gender { get; set; }
    public string Adresse { get; set; }
    public DateTime LastConnected { get; set; }

#nullable enable
    public DateTime? UpdatedDate { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string? GeneratedCodeRememberPW { get; set; }
    public DateTime? ExpireDateRememberPW { get; set; }
#nullable disable
  }

  public class UserDto
  {
    public string Id { get; set; }
    public string FullName { get; set; }
    public string UserName { get; set; }
    public string Gender { get; set; }
    public string Email { get; set; }
    public string ProfileName { get; set; }
    public List<string> Permissions { get; set; }
  }

  public class UserRegisterDto
  {
    [Required(ErrorMessage = "RoleId is required")]
    public string RoleId { get; set; }
    public string FullName { get; set; }

    [Required(ErrorMessage = "Username is required")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string Adresse { get; set; }
  }

  public class UserAuthenticationDto
  {
    [Required(ErrorMessage = "User name is required")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Password name is required")]
    public string Password { get; set; }
  }

  public class UserUpdateDto
  {
    public string RoleId { get; set; }
    public string FullName { get; set; }
    [Required(ErrorMessage = "Username is required")]
    // public string UserName { get; set; }
    // [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Adresse { get; set; }
  }

  public class UserChangePasswordDto
  {
    [Required(ErrorMessage = "Email is required")]
    public string OldPassword { get; set; }
    [Required(ErrorMessage = "Email is required")]
    public string NewPassword { get; set; }
  }

  public class UsersListDto
  {
    public ICollection<ApplicationUser> Users { get; set; }
    public int Count { get; set; }
  }