using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ApplicationDBContext : DbContext
{
    public DbSet<MealBox> MealBoxes { get; set; }

    public DbSet<Canteen> Canteens { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<Employee> Employees { get; set; }

    public DbSet<Student> Students { get; set; }


    public ApplicationDBContext()
    {
    }

    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
    {
    }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var student1 = new Student()
        {
            Id = 1,
            FirstName = "Jaron",
            LastName = "lastname",
            BirthDate = new DateTime(),
            StudentNumber = 12345,
            email = "mai@mail.com",
            StudyCity = City.Breda,
            PhoneNumber = "12345"
        };
        var student2 = new Student()
        {
            Id = 2,
            FirstName = "henk",
            LastName = "vries",
            BirthDate = new DateTime(),
            StudentNumber = 12345,
            email = "mai@mail.com",
            StudyCity = City.Tilburg,
            PhoneNumber = "54321"
        };
    
    
        var broodje = new Product() { Id = 1, Name = "Broodje", ContainsAlcohol = true, Photo = "test" };
        var heineken = new Product() { Id = 2, Name = "Heiniken", ContainsAlcohol = true, Photo = "BIER" };
    
    
        var LD = new Canteen()
            { Id = 1, City = City.Breda, Address = "straat 2", PostalCode = "12345", WarmMealsprovided = true };
        var KantineTilburg = new Canteen()
            { Id = 2, City = City.Tilburg, Address = "straat 5", PostalCode = "54321", WarmMealsprovided = false };
        
       
    
        var box1 = new MealBox()
        {
            Id = 1,
            MealBoxName = "box1",
            City = City.Breda,
            PickupDateTime = new DateTime(),
            ExpireTime = new DateTime(),
            EighteenPlus = true,
            Price = 5.45m,
            Type = MealType.Box,
            StudentId = 1,
            Products = new List<Product>(),
            CanteenId = 1
        };
    
    
        var box2 = new MealBox()
        {
            Id = 2,
            MealBoxName = "box2",
            City = City.Den_Bosch,
            PickupDateTime = new DateTime(),
            ExpireTime = new DateTime(),
            EighteenPlus = false,
            Price = 5.45m,
            Type = MealType.Box,
            Products = new List<Product>(),
            CanteenId = 1
    
        };
        
    
    
        base.OnModelCreating(modelBuilder);
    
        modelBuilder.Entity<Student>().HasData(
            student1, student2
        );
    
        modelBuilder.Entity<Product>().HasData(
            broodje, heineken
        );
    
    
        modelBuilder.Entity<Canteen>().HasData(
            LD, KantineTilburg
        );
    
        modelBuilder.Entity<MealBox>().HasData(
            box1, box2
        );
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.EnableSensitiveDataLogging();
        options.UseSqlServer(
            "Server=tcp:avansvoedselverspillingdb.database.windows.net,1433;Initial Catalog=AvoedselverspillingDB;Persist Security Info=False;User ID=avansvoedselverspillingdb;Password=yac7PJqE@X95!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
    }
}