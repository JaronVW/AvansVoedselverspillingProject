using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class AppIdentityDBContext : IdentityDbContext
{
    public AppIdentityDBContext()
    {
    }

    public AppIdentityDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.EnableSensitiveDataLogging();
        options.UseSqlServer(
            "Server=tcp:avansvoedselverspillingdb.database.windows.net,1433;Initial Catalog=Identity;Persist Security Info=False;User ID=avansvoedselverspillingdb;Password=yac7PJqE@X95!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
    }
}