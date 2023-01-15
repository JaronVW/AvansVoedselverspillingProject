using Core.Domain.Exceptions;
using Core.DomainServices;
using Domain;
using Moq;

namespace ApplicationServicesTests;

public class UnitTest1
{
    [Fact]
    public void MakeMealBoxValid()
    {
        var mealBoxRepoMock = new Mock<IMealBoxRepository>();
        var productRepoMock = new Mock<IProductRepository>();
        var canteenRepoMock = new Mock<ICanteenRepository>();

        canteenRepoMock.Setup(c => c.GetCanteenById(It.IsAny<int>()))
            .Returns(new Canteen()
            {
                Id = 1, CanteenName = "TestCanteen", Address = "TestAddress", City = City.Breda, PostalCode = "1234AB",
                WarmMealsprovided = true
            });

        var sut = new MealBoxService(mealBoxRepoMock.Object, productRepoMock.Object, canteenRepoMock.Object);

        var mealBox = new MealBox()
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
        };

        var product = new Product()
        {
            Id = 1,
            Name = "Kip",
            ContainsAlcohol = false,
            Photo = ""
        };

        var result = sut.AddMealBox(mealBox, new List<Product>() { product });
        Assert.NotNull(result);
        Assert.Equal(0, result.Id);
        Assert.Equal("Een beetje van alles!", result.MealBoxName);
        
    }

    [Fact]
    public void MakeMealBoxInvLid_WarmMealsNotProvided()
    {
        var mealBoxRepoMock = new Mock<IMealBoxRepository>();
        var productRepoMock = new Mock<IProductRepository>();
        var canteenRepoMock = new Mock<ICanteenRepository>();

        canteenRepoMock.Setup(c => c.GetCanteenById(It.IsAny<int>()))
            .Returns(new Canteen()
            {
                Id = 1, CanteenName = "TestCanteen", Address = "TestAddress", City = City.Breda, PostalCode = "1234AB",
                WarmMealsprovided = false
            });

        var sut = new MealBoxService(mealBoxRepoMock.Object, productRepoMock.Object, canteenRepoMock.Object);

        var mealBox = new MealBox()
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
        };

        var product = new Product()
        {
            Id = 1,
            Name = "Kip",
            ContainsAlcohol = false,
            Photo = ""
        };

        Assert.Throws<InvalidFormdataException>(() => sut.AddMealBox(mealBox, new List<Product>() { product }));
    }
    
    [Fact]
    public void MakeMealBoxInvLid_PickupDateTooLate()
    {
        var mealBoxRepoMock = new Mock<IMealBoxRepository>();
        var productRepoMock = new Mock<IProductRepository>();
        var canteenRepoMock = new Mock<ICanteenRepository>();

        canteenRepoMock.Setup(c => c.GetCanteenById(It.IsAny<int>()))
            .Returns(new Canteen()
            {
                Id = 1, CanteenName = "TestCanteen", Address = "TestAddress", City = City.Breda, PostalCode = "1234AB",
                WarmMealsprovided = true
            });

        var sut = new MealBoxService(mealBoxRepoMock.Object, productRepoMock.Object, canteenRepoMock.Object);

        var mealBox = new MealBox()
        {
            MealBoxName = "Een beetje van alles!",
            City = City.Den_Bosch,
            PickupDateTime = DateTime.Today.AddDays(5),
            ExpireTime = DateTime.Today.AddDays(7),
            EighteenPlus = false,
            Price = 5.45m,
            Type = MealType.Box,
            Products = new List<Product>(),
            CanteenId = 4,
            WarmMeals = false
        };

        var product = new Product()
        {
            Id = 1,
            Name = "Kip",
            ContainsAlcohol = false,
            Photo = ""
        };

        Assert.Throws<InvalidFormdataException>(() => sut.AddMealBox(mealBox, new List<Product>() { product }));
    }
    
    [Fact]
    public void MakeMealBoxInvLid_ExpirationDate_before_PickupDate()
    {
        var mealBoxRepoMock = new Mock<IMealBoxRepository>();
        var productRepoMock = new Mock<IProductRepository>();
        var canteenRepoMock = new Mock<ICanteenRepository>();

        canteenRepoMock.Setup(c => c.GetCanteenById(It.IsAny<int>()))
            .Returns(new Canteen()
            {
                Id = 1, CanteenName = "TestCanteen", Address = "TestAddress", City = City.Breda, PostalCode = "1234AB",
                WarmMealsprovided = true
            });

        var sut = new MealBoxService(mealBoxRepoMock.Object, productRepoMock.Object, canteenRepoMock.Object);

        var mealBox = new MealBox()
        {
            MealBoxName = "Een beetje van alles!",
            City = City.Den_Bosch,
            PickupDateTime = DateTime.Today,
            ExpireTime = DateTime.Today.AddDays(-4),
            EighteenPlus = false,
            Price = 5.45m,
            Type = MealType.Box,
            Products = new List<Product>(),
            CanteenId = 4,
            WarmMeals = false
        };

        var product = new Product()
        {
            Id = 1,
            Name = "Kip",
            ContainsAlcohol = false,
            Photo = ""
        };

        Assert.Throws<InvalidFormdataException>(() => sut.AddMealBox(mealBox, new List<Product>() { product }));
    }
    
    [Fact]
    public void MakeMealBoxValid_No_EighteenPlus()
    {
        var mealBoxRepoMock = new Mock<IMealBoxRepository>();
        var productRepoMock = new Mock<IProductRepository>();
        var canteenRepoMock = new Mock<ICanteenRepository>();

        canteenRepoMock.Setup(c => c.GetCanteenById(It.IsAny<int>()))
            .Returns(new Canteen()
            {
                Id = 1, CanteenName = "TestCanteen", Address = "TestAddress", City = City.Breda, PostalCode = "1234AB",
                WarmMealsprovided = true
            });

        var sut = new MealBoxService(mealBoxRepoMock.Object, productRepoMock.Object, canteenRepoMock.Object);

        var mealBox = new MealBox()
        {
            MealBoxName = "Een beetje van alles!",
            City = City.Den_Bosch,
            PickupDateTime = DateTime.Today,
            ExpireTime = DateTime.Today.AddDays(1),
            EighteenPlus = false,
            Price = 5.45m,
            Type = MealType.Box,
            Products = new List<Product>(),
            CanteenId = 4,
            WarmMeals = false
        };

        var product = new Product()
        {
            Id = 1,
            Name = "Kip",
            ContainsAlcohol = false,
            Photo = ""
        };

        var result = sut.AddMealBox(mealBox, new List<Product>() { product });
        Assert.NotNull(result);
        Assert.Equal(0, result.Id);
        Assert.Equal("Een beetje van alles!", result.MealBoxName);
        Assert.False(result.EighteenPlus);
        
    }
    
    [Fact]
    public void MakeMealBoxValid_EighteenPlus()
    {
        var mealBoxRepoMock = new Mock<IMealBoxRepository>();
        var productRepoMock = new Mock<IProductRepository>();
        var canteenRepoMock = new Mock<ICanteenRepository>();

        canteenRepoMock.Setup(c => c.GetCanteenById(It.IsAny<int>()))
            .Returns(new Canteen()
            {
                Id = 1, CanteenName = "TestCanteen", Address = "TestAddress", City = City.Breda, PostalCode = "1234AB",
                WarmMealsprovided = true
            });

        var sut = new MealBoxService(mealBoxRepoMock.Object, productRepoMock.Object, canteenRepoMock.Object);

        var mealBox = new MealBox()
        {
            MealBoxName = "Een beetje van alles!",
            City = City.Den_Bosch,
            PickupDateTime = DateTime.Today,
            ExpireTime = DateTime.Today.AddDays(1),
            EighteenPlus = false,
            Price = 5.45m,
            Type = MealType.Box,
            Products = new List<Product>(),
            CanteenId = 4,
            WarmMeals = false
        };

        var product = new Product()
        {
            Id = 1,
            Name = "Kip",
            ContainsAlcohol = true,
            Photo = ""
        };

        var result = sut.AddMealBox(mealBox, new List<Product>() { product });
        Assert.NotNull(result);
        Assert.Equal(0, result.Id);
        Assert.Equal("Een beetje van alles!", result.MealBoxName);
        Assert.True(result.EighteenPlus);
        
    }
}