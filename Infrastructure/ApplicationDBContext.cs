using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ApplicationDBContext :DbContext
{
    public DbSet<MealBox> MealBoxes { get; set; }
    
    public DbSet<Canteen> Canteens { get; set; }
    
    public DbSet<Product> Products { get; set; }
    
    public DbSet<Student> Students { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("");
    }

}