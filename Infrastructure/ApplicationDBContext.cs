using System.Collections.Immutable;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure;

public class ApplicationDBContext :DbContext
{
    public DbSet<MealBox> MealBoxes { get; set; }
    
    public DbSet<Canteen> Canteens { get; set; }
    
    public DbSet<Product> Products { get; set; }
    
    public DbSet<Student> Students { get; set; }

    public ApplicationDBContext()
    {
        
    }

    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
    {
        
    }


}

// data source=LAPTOP-60VI45O7;initial catalog=voedselverspillingtest;trusted_connection=true
