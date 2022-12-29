using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Seeders;

public class RoleSeeder : IEntityTypeConfiguration<Roles>
{
    public void Configure(EntityTypeBuilder<Roles> builder)
    {
        builder
            .HasData(new Roles
                {
                    Id = new Guid("28ac8d6c-bfd6-4709-bb81-338856d5010d"),
                    Name = "AdminTech",
                    NormalizedName = "ADMINTECH"
                },
                new Roles
                {
                    Id = new Guid("b7204ac2-b4d9-4d17-8551-28320cb2560d"),
                    Name = "AdminMetier",
                    NormalizedName = "ADMINMETIER"
                });
    }
}