using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class AppIdentityDBContext : IdentityDbContext
{
    public AppIdentityDBContext()
    {
    }

    public AppIdentityDBContext( DbContextOptions<AppIdentityDBContext> options) : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.EnableSensitiveDataLogging();
        options.UseSqlServer(
            "data source=LAPTOP-60VI45O7;initial catalog=voedselverspillingtestId;trusted_connection=true;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       base.OnModelCreating(modelBuilder);
       
    }
}