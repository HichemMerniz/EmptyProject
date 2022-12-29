using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Seeders;

public class UserSeeder : IEntityTypeConfiguration<Users>
{
    public void Configure(EntityTypeBuilder<Users> builder)
    {
        Users appUser =
            new()
            {
                Id = new Guid("0b13b705-3d44-4586-b513-04065b351dad"),
                UserName = "admin",
                Email = "admintech@gmail.com",
                ProfileName = "Admin",
                Company = "condor",
                EmailConfirmed = true,
                NormalizedEmail = "AdminTech@gmail.com".ToUpper(),
                NormalizedUserName = "admin".ToUpper(),
                TwoFactorEnabled = false,
                Active = true,
                PhoneNumber = "+213123456789",
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };
        PasswordHasher<Users> ph = new();
        appUser.PasswordHash = ph.HashPassword(appUser, "Pa$$word2022");
        builder.HasData(appUser);
    }
}