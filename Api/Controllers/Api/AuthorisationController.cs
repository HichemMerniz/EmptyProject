using Application.DTOs.Roles;
using Application.IRepositories;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Api;

[Authorize(AuthenticationSchemes = "Bearer")]
[Route("api/authorisation")]
[ApiController]
public class AuthorisationController : ControllerBase
{
    private readonly IMapper _mapper;

    private readonly UserManager<Users> _userManager;

    private readonly RoleManager<Roles> _roleManager;

    private readonly IPasswordValidator<Users> _passwordValidator;

    private readonly IPasswordHasher<Users> _passwordHasher;

    private readonly IAuthenticationRepository _authManager;

    // private readonly IRepositoryWrapper _repositoryWrapper;

    public AuthorisationController(
        IMapper mapper,
        UserManager<Users> userManager,
        RoleManager<Roles> roleManager,
        IPasswordValidator<Users> passwordValidator,
        IPasswordHasher<Users> passwordHasher,
        IAuthenticationRepository authManager
        // IRepositoryWrapper repositoryWrapper
    )
    {
        _mapper = mapper;
        _userManager = userManager;
        _roleManager = roleManager;
        _passwordValidator = passwordValidator;
        _passwordHasher = passwordHasher;
        _authManager = authManager;
        // _repositoryWrapper = repositoryWrapper;
    }


    [HttpGet("roles")]
    // [Authorize(Roles = "AdminTech")]
    public async Task<IActionResult> GetRoles()
    {
        var roles = _roleManager.Roles.ToList();
        return Ok(roles);
    }


    //[Authorize(Role = "admin-tech,admin-metier ")]
    [HttpPost("roles")]
    public async Task<IActionResult> CreateRole(CreateRoleDto createRoleDto)
    {
        var roleExist = await _roleManager.RoleExistsAsync(createRoleDto.Name);

        if (!roleExist) // checks on the role exist status
        {
            var roleResult = await _roleManager.CreateAsync(new Roles(createRoleDto.Name));

            // We need to check if the role has been added successfully
            if (roleResult.Succeeded)
            {
                return Ok(new
                {
                    result = $"The role {createRoleDto.Name} has been added successfully"
                });
            }
            else
            {
                return BadRequest(new
                {
                    error = $"The role {createRoleDto.Name} has not been added"
                });
            }
        }

        return BadRequest(new { error = "Role already exist" });
    }

    // [Authorize(Role = "admin-tech,admin-metier ")]
    [HttpGet("roles/{roleId}")]
    public async Task<IActionResult> GetRole(string roleId)
    {
        var role = await _roleManager.FindByIdAsync(roleId);
        var claims = await _roleManager.GetClaimsAsync(role);
        if (role != null)
        {
            return Ok(new { role = role, claims = claims });
        }
        else
        {
            return BadRequest("Error");
        }
    }

    // [HttpGet("claims")]
    // public IActionResult GetAllClaims()
    // {
    //     var claims = Enum.GetNames(typeof(Permissions));
    //     return Ok(claims);
    // }

    [HttpPost]
    [Route("AddUserToRole")]
    public async Task<IActionResult> AddUserToRole(UserToRoleDto userToRoleDto)
    {
        // Check if the user exist
        var user = await _userManager.FindByEmailAsync(userToRoleDto.Email);

        if (user == null) // User does not exist
        {
            return BadRequest(new
            {
                error = "User does not exist"
            });
        }

        var roleExist = await _roleManager.RoleExistsAsync(userToRoleDto.RoleName);

        if (!roleExist) // checks on the role exist status
        {
            return BadRequest(new
            {
                error = "Role does not exist"
            });
        }

        var result = await _userManager.AddToRoleAsync(user, userToRoleDto.RoleName);

        // Check if the user is assigned to the role successfully
        if (result.Succeeded)
        {
            return Ok(new
            {
                result = "Success, user has been added to the role"
            });
        }
        else
        {
            return BadRequest(new
            {
                error = "The user was not abel to be added to the role"
            });
        }
    }


    //todo:update user role ==>PUT
    [HttpPut("roles/{roleId}")]
    // [Route("UpdateUserRole")]
    public async Task<IActionResult> UpdateUserRole(UserToRoleDto userToRoleDto, string OldRole)
    {
        // Check if the user exist
        var user = await _userManager.FindByEmailAsync(userToRoleDto.Email);

        if (user == null) // User does not exist
        {
            return BadRequest(new
            {
                error = "User does not exist"
            });
        }

        var roleExist = await _roleManager.RoleExistsAsync(userToRoleDto.RoleName);

        if (!roleExist) // checks on the role exist status
        {
            return BadRequest(new
            {
                error = "Role does not exist"
            });
        }

        // Check if the user is assigned to the role successfully
        var isInRoleAsync = await _userManager.IsInRoleAsync(user, OldRole);
        if (isInRoleAsync)
        {
            await _userManager.RemoveFromRoleAsync(user, OldRole);
            var result = await _userManager.AddToRoleAsync(user, userToRoleDto.RoleName);
            // Check if the user is assigned to the role successfully
            if (result.Succeeded)
            {
                return Ok(new
                {
                    result = "Success, user has been added to the role"
                });
            }
            else
            {
                return BadRequest(new
                {
                    error = "The user was not abel to be added to the role"
                });
            }
        }
        else
        {
            return BadRequest(new
            {
                error = "The user was not abel to be added to the role"
            });
        }
    }
    
}