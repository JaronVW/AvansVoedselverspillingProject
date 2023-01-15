using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.ContextClasses;

public class AppIdentityDBContext : IdentityDbContext
{
    public AppIdentityDBContext()
    {
    }

    public AppIdentityDBContext(DbContextOptions<AppIdentityDBContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(
            "data source=LAPTOP-60VI45O7;initial catalog=voedselverspillingtestId;trusted_connection=true;TrustServerCertificate=true;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = "2c5e174e-3b0e-446f-86af-483d56fd7210", Name = "employee",
            NormalizedName = "EMPLOYEE".ToUpper()
        });
        
        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = "4c5e174e-3b0e-446f-86af-483d56fd7210", Name = "student",
            NormalizedName = "STUDENT".ToUpper()
        });
        var hasher = new PasswordHasher<IdentityUser>();

        modelBuilder.Entity<IdentityUser>().HasData(
            new IdentityUser
            {
                Id = "8e445865-a24d-4543-a6c6-9443d048cdb9", // primary key
                UserName = "email@email.com",
                NormalizedUserName = "EMAIL@EMAIL.COM",
                PasswordHash = hasher.HashPassword(null, "1234aA!")
            },
            new IdentityUser
            {
                Id = "9e445865-a24d-4543-a6c6-9443d048cdb9", // primary key
                UserName = "student@email.com",
                NormalizedUserName = "STUDENT@EMAIL.COM",
                PasswordHash = hasher.HashPassword(null, "1234aA!")
            }
        );

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9"
            }, new IdentityUserRole<string>
            {
                RoleId = "4c5e174e-3b0e-446f-86af-483d56fd7210",
                UserId = "9e445865-a24d-4543-a6c6-9443d048cdb9"
            }
        );
    }
}