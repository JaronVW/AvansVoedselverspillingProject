using Core.Domain;
using Core.Domain.Enums;
using Core.Domain.Exceptions;
using Core.DomainServices;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace PersistenceTest;

public class MealboxRepoUnitTest
{
    IQueryable<MealBox> mealBoxData = new List<MealBox>
    {
        new MealBox()
        {
            MealBoxName = "Pilsener verzameling",
            City = City.Breda,
            PickupDateTime = DateTime.Today.AddDays(3),
            ExpireTime = DateTime.Today.AddDays(3).AddHours(2),
            EighteenPlus = true,
            Price = 5.45m,
            Type = MealType.Box,
            CanteenId = 1,
            WarmMeals = false
        },
        new MealBox()
        {
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
        },
        new MealBox()
        {
            MealBoxName = "Deze is gereserveerd",
            City = City.Den_Bosch,
            PickupDateTime = DateTime.Today,
            ExpireTime = DateTime.Today.AddHours(2),
            EighteenPlus = false,
            Price = 5.45m,
            Type = MealType.Box,
            Products = new List<Product>(),
            CanteenId = 4,
            WarmMeals = true,
            StudentId = 1,
            Student = new Student()
            {
            }
        }
    }.AsQueryable();

    IQueryable<Product> productData = new List<Product>
    {
        new Product()
        {
            Id = 1, Name = "Broodje", ContainsAlcohol = false,
            Photo = "https://gezinoverdekook.nl/wp-content/uploads/Broodje-gezond-recept.jpeg",
            MealBoxes = new List<MealBox>()
        },
        new Product()
        {
            Id = 2, Name = "Broodje mozzarella", ContainsAlcohol = false,
            Photo =
                "https://www.modernhoney.com/wp-content/uploads/2019/01/Pesto-Panini-with-Fresh-Mozzarella-and-Tomato-1-crop.jpg",
            MealBoxes = new List<MealBox>()
        }
    }.AsQueryable();

    

}