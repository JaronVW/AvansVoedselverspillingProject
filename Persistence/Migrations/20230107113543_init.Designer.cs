﻿// <auto-generated />
using System;
using Infrastructure;
using Infrastructure.ContextClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20230107113543_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Domain.Canteen", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CanteenName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("City")
                        .HasColumnType("int");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("WarmMealsprovided")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Canteens");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "straat 2",
                            CanteenName = "LD",
                            City = 2,
                            PostalCode = "12345",
                            WarmMealsprovided = true
                        },
                        new
                        {
                            Id = 2,
                            Address = "straat 5",
                            CanteenName = "KantineTilburg",
                            City = 0,
                            PostalCode = "54321",
                            WarmMealsprovided = false
                        });
                });

            modelBuilder.Entity("Domain.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CanteenId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EmployeeNumber")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CanteenId");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CanteenId = 1,
                            Email = "email@email.com",
                            EmployeeNumber = 1,
                            FirstName = "mede",
                            LastName = "werker"
                        });
                });

            modelBuilder.Entity("Domain.MealBox", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CanteenId")
                        .HasColumnType("int");

                    b.Property<int>("City")
                        .HasColumnType("int");

                    b.Property<bool>("EighteenPlus")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ExpireTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("MealBoxName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PickupDateTime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<bool>("WarmMeals")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CanteenId");

                    b.HasIndex("StudentId");

                    b.ToTable("MealBoxes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CanteenId = 1,
                            City = 2,
                            EighteenPlus = true,
                            ExpireTime = new DateTime(2023, 1, 10, 2, 0, 0, 0, DateTimeKind.Local),
                            MealBoxName = "box1",
                            PickupDateTime = new DateTime(2023, 1, 10, 0, 0, 0, 0, DateTimeKind.Local),
                            Price = 5.45m,
                            StudentId = 1,
                            Type = 0,
                            WarmMeals = true
                        },
                        new
                        {
                            Id = 2,
                            CanteenId = 1,
                            City = 1,
                            EighteenPlus = false,
                            ExpireTime = new DateTime(2023, 1, 7, 2, 0, 0, 0, DateTimeKind.Local),
                            MealBoxName = "box2",
                            PickupDateTime = new DateTime(2023, 1, 7, 0, 0, 0, 0, DateTimeKind.Local),
                            Price = 5.45m,
                            Type = 0,
                            WarmMeals = true
                        },
                        new
                        {
                            Id = 3,
                            CanteenId = 1,
                            City = 2,
                            EighteenPlus = false,
                            ExpireTime = new DateTime(2023, 1, 7, 2, 0, 0, 0, DateTimeKind.Local),
                            MealBoxName = "verse producten",
                            PickupDateTime = new DateTime(2023, 1, 7, 0, 0, 0, 0, DateTimeKind.Local),
                            Price = 6.50m,
                            Type = 0,
                            WarmMeals = true
                        },
                        new
                        {
                            Id = 4,
                            CanteenId = 2,
                            City = 2,
                            EighteenPlus = false,
                            ExpireTime = new DateTime(2023, 1, 7, 2, 0, 0, 0, DateTimeKind.Local),
                            MealBoxName = "verse producten",
                            PickupDateTime = new DateTime(2023, 1, 7, 0, 0, 0, 0, DateTimeKind.Local),
                            Price = 6.50m,
                            Type = 0,
                            WarmMeals = true
                        },
                        new
                        {
                            Id = 5,
                            CanteenId = 2,
                            City = 0,
                            EighteenPlus = false,
                            ExpireTime = new DateTime(2023, 1, 7, 2, 0, 0, 0, DateTimeKind.Local),
                            MealBoxName = "nog versere producten",
                            PickupDateTime = new DateTime(2023, 1, 7, 0, 0, 0, 0, DateTimeKind.Local),
                            Price = 6.50m,
                            Type = 0,
                            WarmMeals = true
                        },
                        new
                        {
                            Id = 6,
                            CanteenId = 1,
                            City = 1,
                            EighteenPlus = true,
                            ExpireTime = new DateTime(2023, 1, 7, 2, 0, 0, 0, DateTimeKind.Local),
                            MealBoxName = "oude producten",
                            PickupDateTime = new DateTime(2023, 1, 7, 0, 0, 0, 0, DateTimeKind.Local),
                            Price = 6.50m,
                            Type = 0,
                            WarmMeals = true
                        });
                });

            modelBuilder.Entity("Domain.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("ContainsAlcohol")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Photo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ContainsAlcohol = false,
                            Name = "Broodje",
                            Photo = "https://gezinoverdekook.nl/wp-content/uploads/Broodje-gezond-recept.jpeg"
                        },
                        new
                        {
                            Id = 8,
                            ContainsAlcohol = true,
                            Name = "Heineken",
                            Photo = "https://static.ah.nl/dam/product/AHI_43545239383731303039?revLabel=1&rendition=800x800_JPG_Q90&fileType=binary"
                        },
                        new
                        {
                            Id = 4,
                            ContainsAlcohol = false,
                            Name = "broodje ei",
                            Photo = "https://www.acouplecooks.com/wp-content/uploads/2020/07/Egg-Salad-Sandwich-001.jpg"
                        },
                        new
                        {
                            Id = 6,
                            ContainsAlcohol = false,
                            Name = "kaasplankje",
                            Photo = "https://bettyskitchen.nl/wp-content/uploads/2013/12/zelf_kaasplankje_samenstellen_shutterstock_749650144.jpg"
                        },
                        new
                        {
                            Id = 7,
                            ContainsAlcohol = true,
                            Name = "Hertog Jan",
                            Photo = "https://www.drankuwel.nl/media/catalog/product/cache/d6a5bc6be806788c48ed774973599767/h/e/hertogjan-8packjpg.jpg"
                        },
                        new
                        {
                            Id = 2,
                            ContainsAlcohol = false,
                            Name = "broodje mozzarella",
                            Photo = "https://www.modernhoney.com/wp-content/uploads/2019/01/Pesto-Panini-with-Fresh-Mozzarella-and-Tomato-1-crop.jpg"
                        },
                        new
                        {
                            Id = 3,
                            ContainsAlcohol = false,
                            Name = "verse salade",
                            Photo = "https://www.thespruceeats.com/thmb/Z6IWF7c9zywuU9maSIimGLbHoI4=/3000x2000/filters:fill(auto,1)/classic-caesar-salad-recipe-996054-Hero_01-33c94cc8b8e841ee8f2a815816a0af95.jpg"
                        },
                        new
                        {
                            Id = 5,
                            ContainsAlcohol = false,
                            Name = "fanta",
                            Photo = "https://cdn11.bigcommerce.com/s-2fq65jrvsu/images/stencil/1280x1280/products/528/7297/fanta_orange-1__30340.1664974218.jpg?c=1"
                        });
                });

            modelBuilder.Entity("Domain.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StudentNumber")
                        .HasColumnType("int");

                    b.Property<int>("StudyCity")
                        .HasColumnType("int");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Students");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BirthDate = new DateTime(2002, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Jaron",
                            LastName = "lastname",
                            PhoneNumber = "12345",
                            StudentNumber = 12345,
                            StudyCity = 2,
                            email = "student@email.com"
                        },
                        new
                        {
                            Id = 2,
                            BirthDate = new DateTime(2010, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "henk",
                            LastName = "vries",
                            PhoneNumber = "54321",
                            StudentNumber = 12345,
                            StudyCity = 0,
                            email = "henk@mail.com"
                        },
                        new
                        {
                            Id = 4,
                            BirthDate = new DateTime(1970, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Meneer",
                            LastName = "student",
                            PhoneNumber = "54321",
                            StudentNumber = 12345,
                            StudyCity = 0,
                            email = "studentmeneer@mail.com"
                        },
                        new
                        {
                            Id = 5,
                            BirthDate = new DateTime(2001, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Lucas",
                            LastName = "naam",
                            PhoneNumber = "54321",
                            StudentNumber = 12345,
                            StudyCity = 0,
                            email = "denaam@mail.com"
                        },
                        new
                        {
                            Id = 3,
                            BirthDate = new DateTime(2010, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "henk",
                            LastName = "das",
                            PhoneNumber = "54321",
                            StudentNumber = 12345,
                            StudyCity = 0,
                            email = "henkd@mail.com"
                        });
                });

            modelBuilder.Entity("MealBoxProduct", b =>
                {
                    b.Property<int>("ProductsId")
                        .HasColumnType("int");

                    b.Property<int>("MealBoxesId")
                        .HasColumnType("int");

                    b.HasKey("ProductsId", "MealBoxesId");

                    b.HasIndex("MealBoxesId");

                    b.ToTable("MealBoxProduct");

                    b.HasData(
                        new
                        {
                            ProductsId = 7,
                            MealBoxesId = 1
                        },
                        new
                        {
                            ProductsId = 7,
                            MealBoxesId = 5
                        },
                        new
                        {
                            ProductsId = 1,
                            MealBoxesId = 2
                        },
                        new
                        {
                            ProductsId = 2,
                            MealBoxesId = 2
                        },
                        new
                        {
                            ProductsId = 5,
                            MealBoxesId = 2
                        },
                        new
                        {
                            ProductsId = 6,
                            MealBoxesId = 2
                        },
                        new
                        {
                            ProductsId = 2,
                            MealBoxesId = 6
                        });
                });

            modelBuilder.Entity("Domain.Employee", b =>
                {
                    b.HasOne("Domain.Canteen", "Canteen")
                        .WithMany()
                        .HasForeignKey("CanteenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Canteen");
                });

            modelBuilder.Entity("Domain.MealBox", b =>
                {
                    b.HasOne("Domain.Canteen", "Canteen")
                        .WithMany()
                        .HasForeignKey("CanteenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId");

                    b.Navigation("Canteen");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("MealBoxProduct", b =>
                {
                    b.HasOne("Domain.MealBox", null)
                        .WithMany()
                        .HasForeignKey("MealBoxesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
