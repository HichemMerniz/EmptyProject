using Application.DTOs.Roles;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Application.IRepositories;

public interface IAuthorisationRepository
{
    Task<List<Roles>> GetRoles();
    Task<IdentityResult> CreateRole(CreateRoleDto createRoleDto);
    // Task<IdentityResult> GetRole(string roleId);
    Task<IdentityResult> AddUserToRole(UserToRoleDto userToRoleDto);
    Task<IdentityResult> UpdateUserRole(UserToRoleDto userToRoleDto, string OldRole);
}