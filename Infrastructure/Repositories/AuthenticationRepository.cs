using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.DTOs.Users;
using Application.IRepositories;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Repositories;

public class AuthenticationRepository : IAuthenticationRepository
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<Users> _userManager;
    private Users _user;

    public AuthenticationRepository(
        UserManager<Users> userManager,
        IConfiguration configuration
    )
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public Users AuthenticatedUser()
    {
        return _user;
    }

    public async Task<bool> ValidateUser(LoginUserDto userForAuth)
    {
        _user = await _userManager.FindByNameAsync(userForAuth.UserName);
        if (_user == null) // Try to find User By Email if null
            _user = await _userManager.FindByEmailAsync(userForAuth.UserName);

        return _user != null &&
               await _userManager.CheckPasswordAsync(_user, userForAuth.Password);
    }

    public async Task<string> CreateToken()
    {
        var signingCredentials = GetSigningCredentials();
        var claims = await GetClaims();
        var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }

    private SigningCredentials GetSigningCredentials()
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
        var secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(secret,
            SecurityAlgorithms.HmacSha256);
    }

    private async Task<List<Claim>> GetClaims()
    {
        var claims =
            new List<Claim> { new(ClaimTypes.Name, _user.UserName) };
        var roles = await _userManager.GetRolesAsync(_user);
        foreach (var role in roles) claims.Add(new Claim(ClaimTypes.Role, role));
        return claims;
    }

    private JwtSecurityToken
        GenerateTokenOptions(
            SigningCredentials signingCredentials,
            List<Claim> claims
        )
    {
        var jwtSettings = _configuration.GetSection("Jwt");
        var tokenOptions =
            new JwtSecurityToken(jwtSettings
                    .GetSection("Issuer")
                    .Value,
                jwtSettings.GetSection("Audience").Value,
                claims,
                // expires: DateTime
                //     .Now
                //     .AddMinutes(Convert
                //         .ToDouble(jwtSettings.GetSection("expires").Value)),   todo:changed when configure jwt settings
                signingCredentials: signingCredentials);
        return tokenOptions;
    }
}