using Application.DTOs.Users;
using Domain.Models;

namespace Application.IRepositories;

public interface IAuthenticationRepository
{
    Users AuthenticatedUser();
    Task<bool> ValidateUser(LoginUserDto userForAuth);
    Task<string> CreateToken();
}