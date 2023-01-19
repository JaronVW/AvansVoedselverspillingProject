using Core.Domain;
using Core.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.ContextClasses;

public class ApplicationDBContext : DbContext
{
    public virtual DbSet<MealBox> MealBoxes { get; set; }

    public virtual DbSet<Canteen> Canteens { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Student> Students { get; set; }


    public ApplicationDBContext()
    {
    }

    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        var student1 = new Student()
        {
            Id = 1,
            FirstName = "Jaron",
            LastName = "lastname",
            BirthDate = new DateTime(2002, 1, 19),
            StudentNumber = 12345,
            email = "student@email.com",
            StudyCity = City.Breda,
            PhoneNumber = "12345"
        };
        var student2 = new Student()
        {
            Id = 2,
            FirstName = "henk",
            LastName = "vries",
            BirthDate = new DateTime(2010, 5, 20),
            StudentNumber = 12345,
            email = "henkvries@mail.com",
            StudyCity = City.Tilburg,
            PhoneNumber = "54321"
        };

        var student5 = new Student()
        {
            Id = 3,
            FirstName = "henk",
            LastName = "das",
            BirthDate = new DateTime(2010, 5, 20),
            StudentNumber = 12345,
            email = "henkd@mail.com",
            StudyCity = City.Tilburg,
            PhoneNumber = "54321"
        };

        var student3 = new Student()
        {
            Id = 4,
            FirstName = "Meneer",
            LastName = "student",
            BirthDate = new DateTime(1970, 12, 1),
            StudentNumber = 12345,
            email = "studentmeneer@mail.com",
            StudyCity = City.Tilburg,
            PhoneNumber = "54321"
        };

        var student4 = new Student()
        {
            Id = 5,
            FirstName = "Lucas",
            LastName = "naam",
            BirthDate = new DateTime(2001, 5, 20),
            StudentNumber = 12345,
            email = "adres@mail.com",
            StudyCity = City.Tilburg,
            PhoneNumber = "54321",
        };


        var LD = new Canteen()
        {
            Id = 1, City = City.Breda, Address = "straat 2", PostalCode = "12345", WarmMealsprovided = true,
            CanteenName = "LD"
        };
        var KantineTilburg = new Canteen()
        {
            Id = 2, City = City.Tilburg, Address = "straat 5", PostalCode = "54321", WarmMealsprovided = false,
            CanteenName = "Kantine Tilburg"
        };

        var LA = new Canteen()
        {
            Id = 3, City = City.Breda, Address = "Lovensdijkstraat 35", PostalCode = "98765", WarmMealsprovided = true,
            CanteenName = "LA"
        };
        var HA = new Canteen()
        {
            Id = 4, City = City.Den_Bosch, Address = "laan 120", PostalCode = "01023", WarmMealsprovided = true,
            CanteenName = "HA"
        };

        var broodje = new Product()
        {
            Id = 1, Name = "Broodje", ContainsAlcohol = false,
            Photo = "https://gezinoverdekook.nl/wp-content/uploads/Broodje-gezond-recept.jpeg",
            MealBoxes = new List<MealBox>()
        };

        var broodjeMozzarella = new Product()
        {
            Id = 2, Name = "Broodje mozzarella", ContainsAlcohol = false,
            Photo =
                "https://www.modernhoney.com/wp-content/uploads/2019/01/Pesto-Panini-with-Fresh-Mozzarella-and-Tomato-1-crop.jpg",
            MealBoxes = new List<MealBox>()
        };

        var salade = new Product()
        {
            Id = 3, Name = "Verse salade", ContainsAlcohol = false,
            Photo =
                "https://www.thespruceeats.com/thmb/Z6IWF7c9zywuU9maSIimGLbHoI4=/3000x2000/filters:fill(auto,1)/classic-caesar-salad-recipe-996054-Hero_01-33c94cc8b8e841ee8f2a815816a0af95.jpg",
            MealBoxes = new List<MealBox>()
        };

        var broodjeEi = new Product()
        {
            Id = 4, Name = "Broodje ei", ContainsAlcohol = false,
            Photo = "https://www.acouplecooks.com/wp-content/uploads/2020/07/Egg-Salad-Sandwich-001.jpg",
            MealBoxes = new List<MealBox>()
        };

        var fanta = new Product()
        {
            Id = 5, Name = "Fanta", ContainsAlcohol = false,
            Photo =
                "https://cdn11.bigcommerce.com/s-2fq65jrvsu/images/stencil/1280x1280/products/528/7297/fanta_orange-1__30340.1664974218.jpg?c=1",
            MealBoxes = new List<MealBox>()
        };

        var kaasplankje = new Product()
        {
            Id = 6, Name = "Kaasplankje", ContainsAlcohol = false,
            Photo =
                "https://bettyskitchen.nl/wp-content/uploads/2013/12/zelf_kaasplankje_samenstellen_shutterstock_749650144.jpg",
            MealBoxes = new List<MealBox>()
        };

        var HertogJan = new Product()
        {
            Id = 7, Name = "Hertog Jan", ContainsAlcohol = true,
            Photo =
                "https://www.drankuwel.nl/media/catalog/product/cache/d6a5bc6be806788c48ed774973599767/h/e/hertogjan-8packjpg.jpg",
            MealBoxes = new List<MealBox>()
        };

        var heineken = new Product()
        {
            Id = 8, Name = "Heineken", ContainsAlcohol = true,
            Photo =
                "https://static.ah.nl/dam/product/AHI_43545239383731303039?revLabel=1&rendition=800x800_JPG_Q90&fileType=binary",
            MealBoxes = new List<MealBox>()
        };


        var employee1 = new Employee()
        {
            Id = 1,
            Email = "email@email.com",
            FirstName = "mede",
            LastName = "werker",
            EmployeeNumber = 1,
            CanteenId = 1
        };


        var box1 = new MealBox()
        {
            Id = 1,
            MealBoxName = "Pilsener verzameling",
            City = City.Breda,
            PickupDateTime = DateTime.Today.AddDays(3),
            ExpireTime = DateTime.Today.AddDays(3).AddHours(2),
            EighteenPlus = true,
            Price = 5.45m,
            Type = MealType.Box,
            StudentId = 1,
            Products = new List<Product>(),
            CanteenId = 1,
            WarmMeals = false
        };


        var box2 = new MealBox()
        {
            Id = 2,
            MealBoxName = "Een beetje van alles!",
            City = City.Den_Bosch,
            PickupDateTime = DateTime.Today,
            ExpireTime = DateTime.Today.AddHours(2),
            EighteenPlus = false,
            Price = 5.45m,
            Type = MealType.Box,
            Products = new List<Product>(),
            CanteenId = 4,
            WarmMeals = true
        };

        var box3 = new MealBox()
        {
            Id = 3,
            MealBoxName = "verse producten week 10",
            City = City.Breda,
            PickupDateTime = DateTime.Today,
            ExpireTime = DateTime.Today.AddHours(2),
            EighteenPlus = false,
            Price = 6.50m,
            Type = MealType.Box,
            Products = new List<Product>(),
            CanteenId = 1
        };

        var box4 = new MealBox()
        {
            Id = 4,
            MealBoxName = "verse producten week 15",
            City = City.Breda,
            PickupDateTime = DateTime.Today,
            ExpireTime = DateTime.Today.AddHours(2),
            EighteenPlus = false,
            Price = 6.50m,
            Type = MealType.Box,
            Products = new List<Product>(),
            CanteenId = 3,
            WarmMeals = false
        };

        var box5 = new MealBox()
        {
            Id = 5,
            MealBoxName = "nog versere producten",
            City = City.Tilburg,
            PickupDateTime = DateTime.Today,
            ExpireTime = DateTime.Today.AddHours(2),
            EighteenPlus = true,
            Price = 6.50m,
            Type = MealType.Box,
            Products = new List<Product>(),
            CanteenId = 2,
            WarmMeals = false
        };

        var box6 = new MealBox()
        {
            Id = 6,
            MealBoxName = "oude producten",
            City = City.Den_Bosch,
            PickupDateTime = DateTime.Today,
            ExpireTime = DateTime.Today.AddHours(2),
            EighteenPlus = false,
            Price = 6.50m,
            Type = MealType.Box,
            Products = new List<Product>(),
            CanteenId = 4,
            WarmMeals = true
        };


        modelBuilder.Entity<Student>().HasData(
            student1, student2, student3, student4, student5
        );

        modelBuilder.Entity<Product>().HasData(
            broodje, heineken, broodjeEi, kaasplankje, HertogJan, broodjeMozzarella, salade, fanta
        );


        modelBuilder.Entity<Canteen>().HasData(
            LD, KantineTilburg, LA, HA
        );

        modelBuilder.Entity<Employee>().HasData(
            employee1
        );

        modelBuilder.Entity<MealBox>().HasData(
            box1, box2, box3, box4, box5, box6
        );

        modelBuilder.Entity<MealBox>()
            .HasMany(p => p.Products)
            .WithMany(t => t.MealBoxes)
            .UsingEntity<Dictionary<string, object>>(
                "MealBoxProduct",
                r => r.HasOne<Product>().WithMany().HasForeignKey("ProductsId"),
                l => l.HasOne<MealBox>().WithMany().HasForeignKey("MealBoxesId"),
                je =>
                {
                    je.HasKey("ProductsId", "MealBoxesId");
                    je.HasData(
                        new { ProductsId = 7, MealBoxesId = 1 },
                        new { ProductsId = 7, MealBoxesId = 5 },
                        new { ProductsId = 1, MealBoxesId = 2 },
                        new { ProductsId = 2, MealBoxesId = 2 },
                        new { ProductsId = 5, MealBoxesId = 2 },
                        new { ProductsId = 6, MealBoxesId = 2 },
                        new { ProductsId = 2, MealBoxesId = 6 },
                        new { ProductsId = 1, MealBoxesId = 5 },
                        new { ProductsId = 3, MealBoxesId = 5 },
                        new { ProductsId = 3, MealBoxesId = 4 });
                });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // options.EnableSensitiveDataLogging();
        options.EnableSensitiveDataLogging();
        options.UseSqlServer("Server=aei-sql2.avans.nl,1443;Initial Catalog=VerspillingDB;Persist Security Info=False;User ID=jaron;Password=jNi^jw$5%UqN5pVB;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;TrustServerCertificate=true;");
    }
}