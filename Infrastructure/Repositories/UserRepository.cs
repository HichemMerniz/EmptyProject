using Application.DTOs.Users;
using Application.IRepositories;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _AppDbContext;
    private readonly IConfiguration _configuration;
    private readonly UserManager<Users> _userManager;


    public UserRepository(AppDbContext AppDbContext, UserManager<Users> userManager, IConfiguration configuration)
    {
        _AppDbContext = AppDbContext;
        _userManager = userManager;
        _configuration = configuration;
    }

    // public async Task<List<Users>> GetAllUsers(int page, int perPage, string? name = null)
    // {
    //     if (name != null)
    //         return await _userManager.Users
    //             .Where(o => o.UserName.Contains(name))
    //             .Skip((page - 1) * perPage)
    //             .Take(perPage)
    //             .ToListAsync();
    //     return await _userManager.Users
    //         //.Skip((page - 1) * perPage)
    //         //.Take(perPage)
    //         .ToListAsync();
    // }

    public async Task<List<Users>> GetAllUsers()
    {
        return await _userManager.Users
            .Where(o => o.Active == true)
            //.Skip((page - 1) * perPage)
            //.Take(perPage)
            .ToListAsync();
    }

    public async Task<List<Users>> GetUsersByCompany(string company)
    {
        return await _userManager.Users.Where(e => e.Company == company).ToListAsync();
    }

    public Users Update(Guid id, UserUpdateDto newUser)
    {
        var user = _AppDbContext.Set<Users>().FirstOrDefault(b => b.Id == id);
        PasswordHasher<Users> ph = new();
        if (newUser.Password != null)
        {
            var newPassword = ph.HashPassword(user, newUser.Password);
            user.PasswordHash = newUser.Password != null ? newPassword : user.PasswordHash;
        }

        if (user == null) return user;

        var roleString = user.ProfileName;
        var newRoleString = newUser.ProfileName;
        if (user != null)
        {
            user.UserName = newUser.FullName != null ? newUser.FullName : user.UserName;
            user.Email = newUser.Mail != null ? newUser.Mail : user.Email;
            user.PhoneNumber = newUser.Phone != null ? newUser.Phone : user.PhoneNumber;
            user.Company = newUser.Company != null ? newUser.Company : user.Company;
            user.Active = newUser.Active ? user.Active : newUser.Active;
            user.ProfileName = newUser.ProfileName != null ? newUser.ProfileName : user.ProfileName;
            // user.PasswordHash = newUser.Password != null ? newPassword : user.PasswordHash;
        }

        // if (roleString != newUser.ProfileName)
        // {
        //      _userManager.RemoveFromRoleAsync(user, roleString);
        //      _userManager.AddToRoleAsync(user, newUser.ProfileName);
        // }
        // updateRole(roleString, newRoleString, user);
        // AuthorisationRepository.UpdateUserRole(user, newUser.Role, _userManager); 
        _AppDbContext.Set<Users>().Update(user);
        _AppDbContext.SaveChanges();

        return user;
    }

  

    public Users Delete(Guid id)
    {
        var user = _AppDbContext.Set<Users>().FirstOrDefault(b => b.Id == id);
        if (user == null) return user;
        _AppDbContext.Set<Users>().Remove(user);
        _AppDbContext.SaveChanges();
        return user;
    }
}