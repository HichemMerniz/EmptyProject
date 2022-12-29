﻿using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Users;

public class LoginUserDto
{
    [Required(ErrorMessage = "User name is required")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Password name is required")]
    public string Password { get; set; }
}