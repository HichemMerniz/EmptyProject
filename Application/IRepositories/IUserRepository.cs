using Application.DTOs.Users;
using Domain.Models;

namespace Application.IRepositories;

public interface IUserRepository
{
    // Task<List<Users>> GetAllUsers(int page, int perPage, string? name = null);
    Task<List<Users>> GetAllUsers();
    Task<List<Users>> GetUsersByCompany(string company);
    Users Update(Guid id, UserUpdateDto user);
    Users Delete(Guid id);
}