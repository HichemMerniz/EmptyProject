using Application.Seeders;
using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class AppDbContext : IdentityDbContext<Users, Roles, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> opt) :
        base(opt)
    {
    }

    public DbSet<Users> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new RoleSeeder());
        modelBuilder.ApplyConfiguration(new UserSeeder());

        modelBuilder.Entity<Users>(builder =>
        {
            builder.Metadata.RemoveIndex(new[] { builder.Property(u => u.NormalizedUserName).Metadata });

            builder.HasIndex(u => new { u.NormalizedUserName }).HasName("UserNameIndex").IsUnique();
        });

        modelBuilder.Entity<Roles>(builder =>
        {
            builder.Metadata.RemoveIndex(new[] { builder.Property(r => r.NormalizedName).Metadata });

            builder.HasIndex(r => new { r.NormalizedName }).HasName("RoleNameIndex").IsUnique();
        });
    }
}