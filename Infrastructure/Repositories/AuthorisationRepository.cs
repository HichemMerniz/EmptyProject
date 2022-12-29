using Application.DTOs.Roles;
using Application.IRepositories;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories;

public class AuthorisationRepository : IAuthorisationRepository
{
    private readonly UserManager<Users> _userManager;

    private readonly RoleManager<Roles> _roleManager;
    private readonly IConfiguration _configuration;
    
    
    public AuthorisationRepository(
        UserManager<Users> userManager,
        RoleManager<Roles> roleManager,
        IConfiguration configuration
    )
    {
        _userManager = userManager;
        _configuration = configuration;
        _roleManager = roleManager;
    }
    
    public async Task<List<Roles>> GetRoles()
    {
        var roles = _roleManager.Roles.ToList();
        return roles;
    }
    
    public async Task<IdentityResult> CreateRole(CreateRoleDto createRoleDto)
    {
        var roleExist = await _roleManager.RoleExistsAsync(createRoleDto.Name);

        if (!roleExist) // checks on the role exist status
        {
            var roleResult = await _roleManager.CreateAsync(new Roles(createRoleDto.Name));
            return roleResult;
        }

        return null;
    }

   


    public async Task<IdentityResult> AddUserToRole(UserToRoleDto userToRoleDto)
    {
        // Check if the user exist
        var user = await _userManager.FindByEmailAsync(userToRoleDto.Email);

        if (user == null) // User does not exist
        {
            return null;
        }

        var roleExist = await _roleManager.RoleExistsAsync(userToRoleDto.RoleName);

        if (!roleExist) // checks on the role exist status
        {
            return null; 
        }

        var result = await _userManager.AddToRoleAsync(user, userToRoleDto.RoleName);

        return result;
    }


    public async Task<IdentityResult> UpdateUserRole(UserToRoleDto userToRoleDto, string OldRole)
    {
        // Check if the user exist
        var user = await _userManager.FindByEmailAsync(userToRoleDto.Email);

        if (user == null) // User does not exist
        {
            return null;
        }

        var roleExist = await _roleManager.RoleExistsAsync(userToRoleDto.RoleName);

        if (!roleExist) // checks on the role exist status
        {
            return null;
        }

        // Check if the user is assigned to the role successfully
        var isInRoleAsync = await _userManager.IsInRoleAsync(user, OldRole);
        if (isInRoleAsync)
        {
            await _userManager.RemoveFromRoleAsync(user, OldRole);
            var result = await _userManager.AddToRoleAsync(user, userToRoleDto.RoleName);
            // Check if the user is assigned to the role successfully
            return result;
        }
        else
        {
            return null;
        }
    }


}