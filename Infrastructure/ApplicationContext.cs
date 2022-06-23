using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ApplicationContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ApplicationContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) :
            base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // modelBuilder.ApplyConfiguration(new RoleConfiguration());
            // modelBuilder.ApplyConfiguration(new UserConfiguration());
            // modelBuilder.ApplyConfiguration(new ConstantConfiguration());

            // TODO: complete other unique fields
            // modelBuilder.Entity<Regelement>()
            // .Property<int>("AutoIncrement")
            // .ValueGeneratedOnAdd();
            // modelBuilder.Entity<Regelement>()
            //     .HasAlternateKey("AutoIncrement");

            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
            // modelBuilder.Entity<Service>().HasIndex(u => u.Name).IsUnique();
            // modelBuilder.Entity<Card>().HasIndex(u => u.Code).IsUnique();
            // modelBuilder.Entity<Room>().HasIndex(u => u.Name).IsUnique();
            // modelBuilder.Entity<Division>().HasIndex(u => u.Name).IsUnique();
            // modelBuilder.Entity<Key>().HasIndex(u => u.Name).IsUnique();
            // modelBuilder.Entity<PricingPlan>().HasIndex(u => u.Name).IsUnique();
            // modelBuilder.Entity<Planning>().HasIndex(u => u.Name).IsUnique();
            




            // modelBuilder.Entity<BLStatus>()
            //  .HasOne(x => x.BL)
            //  .WithMany(x => x.BLStatus)
            //  .OnDelete(DeleteBehavior.Cascade);
        }

         public DbSet<Constant> Constant { get; set; }
         // public DbSet<Participant> Participants { get; set; }
         // public DbSet<Unit> Units { get; set; }
       


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is EntityBase && e.State is EntityState.Added or EntityState.Modified);
            var userName = "System";
            if (_httpContextAccessor.HttpContext.User.Identity != null)
                userName = _httpContextAccessor.HttpContext.User.Identity.Name;
            foreach (var entityEntry in entries)
            {
                ((EntityBase)entityEntry.Entity).UpdatedDate = DateTime.Now;
                if (userName != null)
                    ((EntityBase)entityEntry.Entity).UpdatedBy = userName;

                switch (entityEntry.State)
                {
                    case EntityState.Added:
                        {
                            ((EntityBase)entityEntry.Entity).CreatedDate = DateTime.Now;
                            if (userName != null)
                                ((EntityBase)entityEntry.Entity).CreatedBy = userName;
                            break;
                        }
                    case EntityState.Modified:
                        {
                            ((EntityBase)entityEntry.Entity).UpdatedDate = DateTime.Now;
                            if (userName != null)
                                ((EntityBase)entityEntry.Entity).UpdatedBy = userName;
                            break;
                        }
                    case EntityState.Detached:
                        break;
                    case EntityState.Unchanged:
                        break;
                    case EntityState.Deleted:
                        break; // TODO: implement onDelete after 48 hours
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }

    