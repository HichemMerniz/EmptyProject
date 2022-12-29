using Application.DTOs.Users;
using Application.IRepositories;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Api;

[Authorize(AuthenticationSchemes = "Bearer")]
[Route("api/authentication")]
[ApiController]
public class AuthonticationController : ControllerBase
{
    private readonly IAuthenticationRepository _authManager;
    private readonly IMapper _mapper;

    private readonly IPasswordHasher<Users> _passwordHasher;

    private readonly IPasswordValidator<Users> _passwordValidator;
    private readonly RoleManager<Roles> _roleManager;

    private readonly UserManager<Users> _userManager;

    public AuthonticationController(
        IMapper mapper,
        UserManager<Users> userManager,
        RoleManager<Roles> roleManager,
        IAuthenticationRepository authManager
    )
    {
        _mapper = mapper;
        _userManager = userManager;
        _roleManager = roleManager;
        _authManager = authManager;
    }

    // todo: AuthonticationController || "Get Profile" ==> GET
    [HttpGet("profile")]
    public async Task<IActionResult> Profile()
    {
        var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
        // var user = await _userManager.GetUserId();
        if (user == null) return Unauthorized();

        var permissions = new List<string>();
        var userRoles = await _userManager.GetRolesAsync(user);

        var userToReturn = _mapper.Map<UserDto>(user);
        userToReturn.Role = userRoles[0];
        return Ok(userToReturn);
    }

    // todo: AuthonticationController || "Login Account" ==> POST
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginUserDto user)
    {
        if (!await _authManager.ValidateUser(user)) return Unauthorized("Token expired");

        return Ok(new { Token = await _authManager.CreateToken() });
    }

    // todo: AuthonticationController || "Create Account" ==> POST
    [HttpPost("create")]
    // [Authorize(Roles = "AdminTech")]
    [AllowAnonymous]
    public async Task<IActionResult> CreateUser(CreateUserDto createUserDto)
    {
        var user = _mapper.Map<Users>(createUserDto);
        var result =
            await _userManager
                .CreateAsync(user, createUserDto.Password);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors) ModelState.TryAddModelError(error.Code, error.Description);

            return BadRequest(ModelState);
        }

        return StatusCode(201);
    }
}