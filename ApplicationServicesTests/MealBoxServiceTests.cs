using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using ApplicationServices;
using Core.Domain;
using Core.Domain.Enums;
using Core.Domain.Exceptions;
using Core.DomainServices;
using Moq;

namespace ApplicationServicesTests;

[SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
public class MealBoxServiceTests
{
    private readonly List<MealBox> _mealBoxList = new List<MealBox>()
    {
        new()
        {
            MealBoxName = "Mealbox die gereserveerd is",
            City = City.Breda,
            PickupDateTime = DateTime.Today,
            StudentId = 1,
            ExpireTime = DateTime.Today.AddDays(1),
            EighteenPlus = false,
            Price = 5.45m,
            Type = MealType.Box,
            Products = new List<Product>(),
            CanteenId = 4,
            WarmMeals = false
        },
        new()
        {
            MealBoxName = "Mealbox die niet gereserveerd is 1",
            City = City.Den_Bosch,
            PickupDateTime = DateTime.Today,
            ExpireTime = DateTime.Today.AddDays(2),
            EighteenPlus = false,
            Price = 5.45m,
            Type = MealType.Box,
            Products = new List<Product>(),
            CanteenId = 4,
            WarmMeals = false
        },
        new()
        {
            MealBoxName = "Mealbox die niet gereserveerd is 2",
            City = City.Den_Bosch,
            PickupDateTime = DateTime.Today.AddDays(1),
            ExpireTime = DateTime.Today.AddDays(2),
            EighteenPlus = false,
            Price = 5.45m,
            Type = MealType.Box,
            Products = new List<Product>(),
            CanteenId = 4,
            WarmMeals = false
        },
        new()
        {
            MealBoxName = "Mealbox die niet gereserveerd is 3",
            City = City.Den_Bosch,
            PickupDateTime = DateTime.Today,
            ExpireTime = DateTime.Today.AddDays(1),
            EighteenPlus = false,
            Price = 5.45m,
            Type = MealType.Box,
            Products = new List<Product>(),
            CanteenId = 3,
            WarmMeals = false
        },
        new()
        {
            MealBoxName = "Mealbox die niet gereserveerd is 4",
            City = City.Den_Bosch,
            PickupDateTime = DateTime.Today.AddDays(1),
            ExpireTime = DateTime.Today.AddDays(2),
            EighteenPlus = false,
            Price = 5.45m,
            Type = MealType.Box,
            Products = new List<Product>(),
            CanteenId = 3,
            WarmMeals = false
        }
    };

    [Fact]
    public void Get_MealBox_Non_Reserved()
    {
        var mealBoxRepoMock = new Mock<IMealBoxRepository>();
        var canteenRepoMock = new Mock<ICanteenRepository>();
        var studentRepoMock = new Mock<IStudentRepository>();

        var mealBoxService = new MealBoxService(mealBoxRepoMock.Object,  canteenRepoMock.Object,
            studentRepoMock.Object);
        mealBoxRepoMock.Setup(c => c.GetMealBoxes()).Returns(_mealBoxList);

        var mealBoxes = mealBoxService.GetMealBoxesNonReserved();
        Assert.Equal(4, mealBoxes.Count());
        Assert.Equal("Mealbox die niet gereserveerd is 1", mealBoxes.ElementAt(0).MealBoxName);
        Assert.Equal("Mealbox die niet gereserveerd is 3", mealBoxes.ElementAt(1).MealBoxName);
        Assert.Equal("Mealbox die niet gereserveerd is 2", mealBoxes.ElementAt(2).MealBoxName);
    }

    [Fact]
    public void Get_MealBox_Reserved()
    {
        var mealBoxRepoMock = new Mock<IMealBoxRepository>();
        var canteenRepoMock = new Mock<ICanteenRepository>();
        var studentRepoMock = new Mock<IStudentRepository>();

        var mealBoxService = new MealBoxService(mealBoxRepoMock.Object,  canteenRepoMock.Object,
            studentRepoMock.Object);
        mealBoxRepoMock.Setup(c => c.GetMealBoxes()).Returns(_mealBoxList);

        var mealBoxes = mealBoxService.GetMealBoxesReserved(1);
        Assert.Single(mealBoxes);
        Assert.Equal("Mealbox die gereserveerd is", mealBoxes.ElementAt(0).MealBoxName);
    }

    [Fact]
    public void Get_MealBox_Own_Canteen_Date_Ascending()
    {
        var mealBoxRepoMock = new Mock<IMealBoxRepository>();
        var canteenRepoMock = new Mock<ICanteenRepository>();
        var studentRepoMock = new Mock<IStudentRepository>();

        var mealBoxService = new MealBoxService(mealBoxRepoMock.Object,  canteenRepoMock.Object,
            studentRepoMock.Object);
        mealBoxRepoMock.Setup(c => c.GetMealBoxes()).Returns(_mealBoxList);

        var mealBoxes = mealBoxService.GetMealBoxesOwnCanteen(3);
        Assert.Equal(2, mealBoxes.Count());
        Assert.Equal("Mealbox die niet gereserveerd is 3", mealBoxes.ElementAt(0).MealBoxName);
        Assert.True(mealBoxes.ElementAt(0).PickupDateTime.Date < mealBoxes.ElementAt(1).PickupDateTime.Date);
    }

    [Fact]
    public void Get_MealBox_Other_Canteens_Date_Ascending()
    {
        var mealBoxRepoMock = new Mock<IMealBoxRepository>();
        var canteenRepoMock = new Mock<ICanteenRepository>();
        var studentRepoMock = new Mock<IStudentRepository>();

        var mealBoxService = new MealBoxService(mealBoxRepoMock.Object,  canteenRepoMock.Object,
            studentRepoMock.Object);
        mealBoxRepoMock.Setup(c => c.GetMealBoxes()).Returns(_mealBoxList);

        var mealBoxes = mealBoxService.GetMealBoxesOtherCanteens(3);
        Assert.Equal(2, mealBoxes.Count());
        Assert.Equal("Mealbox die niet gereserveerd is 1", mealBoxes.ElementAt(0).MealBoxName);
        Assert.Equal("Mealbox die niet gereserveerd is 2", mealBoxes.ElementAt(1).MealBoxName);
        Assert.True(mealBoxes.ElementAt(0).PickupDateTime.Date < mealBoxes.ElementAt(1).PickupDateTime.Date);
    }


    [Fact]
    public void MakeMealBoxValid()
    {
        var mealBoxRepoMock = new Mock<IMealBoxRepository>();
        var canteenRepoMock = new Mock<ICanteenRepository>();
        var studentRepoMock = new Mock<IStudentRepository>();

        var testCanteen = new Canteen()
        {
            Id = 1, CanteenName = "TestCanteen", Address = "TestAddress", City = City.Breda, PostalCode = "1234AB",
            WarmMealsprovided = true
        };

        canteenRepoMock.Setup(c => c.GetCanteenById(It.IsAny<int>()))
            .Returns(testCanteen);

        var sut = new MealBoxService(mealBoxRepoMock.Object,  canteenRepoMock.Object,
            studentRepoMock.Object);

        var mealBox = new MealBox()
        {
            MealBoxName = "Een beetje van alles!",
            PickupDateTime = DateTime.Today,
            ExpireTime = DateTime.Today.AddHours(2),
            EighteenPlus = false,
            Price = 5.45m,
            Type = MealType.Box,
            Products = new List<Product>(),
            WarmMeals = true
        };

        var product = new Product()
        {
            Id = 1,
            Name = "Kip",
            ContainsAlcohol = false,
            Photo = ""
        };

        var result = sut.AddMealBox(mealBox, new List<Product>() { product }, 1);
        Assert.NotNull(result);
        Assert.Equal(0, result.Id);
        Assert.Equal("Een beetje van alles!", result.MealBoxName);
        Assert.Equal(City.Breda, result.City);
        Debug.Assert(result.Canteen != null, "result.Canteen != null");
        Assert.Equal(testCanteen.CanteenName, result.Canteen.CanteenName);
    }

    [Fact]
    public void MakeMealBoxInvLid_WarmMealsNotProvided()
    {
        var mealBoxRepoMock = new Mock<IMealBoxRepository>();
        var canteenRepoMock = new Mock<ICanteenRepository>();
        var studentRepoMock = new Mock<IStudentRepository>();

        canteenRepoMock.Setup(c => c.GetCanteenById(It.IsAny<int>()))
            .Returns(new Canteen()
            {
                Id = 1, CanteenName = "TestCanteen", Address = "TestAddress", City = City.Breda, PostalCode = "1234AB",
                WarmMealsprovided = false
            });

        var sut = new MealBoxService(mealBoxRepoMock.Object,  canteenRepoMock.Object,
            studentRepoMock.Object);

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

        Assert.Throws<InvalidFormdataException>(() => sut.AddMealBox(mealBox, new List<Product>() { product }, 1));
    }

    [Fact]
    public void MakeMealBoxInvLid_PickupDateTooLate()
    {
        var mealBoxRepoMock = new Mock<IMealBoxRepository>();
        var canteenRepoMock = new Mock<ICanteenRepository>();
        var studentRepoMock = new Mock<IStudentRepository>();

        canteenRepoMock.Setup(c => c.GetCanteenById(It.IsAny<int>()))
            .Returns(new Canteen()
            {
                Id = 1, CanteenName = "TestCanteen", Address = "TestAddress", City = City.Breda, PostalCode = "1234AB",
                WarmMealsprovided = true
            });

        var sut = new MealBoxService(mealBoxRepoMock.Object,  canteenRepoMock.Object,
            studentRepoMock.Object);

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

        Assert.Throws<InvalidFormdataException>(() => sut.AddMealBox(mealBox, new List<Product>() { product }, 1));
    }

    [Fact]
    public void MakeMealBoxInvLid_ExpirationDate_before_PickupDate()
    {
        var mealBoxRepoMock = new Mock<IMealBoxRepository>();
        var canteenRepoMock = new Mock<ICanteenRepository>();
        var studentRepoMock = new Mock<IStudentRepository>();

        canteenRepoMock.Setup(c => c.GetCanteenById(It.IsAny<int>()))
            .Returns(new Canteen()
            {
                Id = 1, CanteenName = "TestCanteen", Address = "TestAddress", City = City.Breda, PostalCode = "1234AB",
                WarmMealsprovided = true
            });

        var sut = new MealBoxService(mealBoxRepoMock.Object,  canteenRepoMock.Object,
            studentRepoMock.Object);

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

        Assert.Throws<InvalidFormdataException>(() => sut.AddMealBox(mealBox, new List<Product>() { product }, 1));
    }

    [Fact]
    public void MakeMealBoxValid_No_EighteenPlus()
    {
        var mealBoxRepoMock = new Mock<IMealBoxRepository>();
        var canteenRepoMock = new Mock<ICanteenRepository>();
        var studentRepoMock = new Mock<IStudentRepository>();

        canteenRepoMock.Setup(c => c.GetCanteenById(It.IsAny<int>()))
            .Returns(new Canteen()
            {
                Id = 1, CanteenName = "TestCanteen", Address = "TestAddress", City = City.Breda, PostalCode = "1234AB",
                WarmMealsprovided = true
            });

        var sut = new MealBoxService(mealBoxRepoMock.Object,  canteenRepoMock.Object,
            studentRepoMock.Object);

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

        var result = sut.AddMealBox(mealBox, new List<Product>() { product }, 1);
        Assert.NotNull(result);
        Assert.Equal(0, result.Id);
        Assert.Equal("Een beetje van alles!", result.MealBoxName);
        Assert.False(result.EighteenPlus);
    }

    [Fact]
    public void MakeMealBoxValid_EighteenPlus()
    {
        var mealBoxRepoMock = new Mock<IMealBoxRepository>();
        var canteenRepoMock = new Mock<ICanteenRepository>();
        var studentRepoMock = new Mock<IStudentRepository>();

        canteenRepoMock.Setup(c => c.GetCanteenById(It.IsAny<int>()))
            .Returns(new Canteen()
            {
                Id = 1, CanteenName = "TestCanteen", Address = "TestAddress", City = City.Breda, PostalCode = "1234AB",
                WarmMealsprovided = true
            });

        var sut = new MealBoxService(mealBoxRepoMock.Object,  canteenRepoMock.Object,
            studentRepoMock.Object);

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

        var result = sut.AddMealBox(mealBox, new List<Product>() { product }, 1);
        Assert.NotNull(result);
        Assert.Equal(0, result.Id);
        Assert.Equal("Een beetje van alles!", result.MealBoxName);
        Assert.True(result.EighteenPlus);
    }

    [Fact]
    public void UpdateMealBoxValid()
    {
        var mealBoxRepoMock = new Mock<IMealBoxRepository>();
        var canteenRepoMock = new Mock<ICanteenRepository>();
        var studentRepoMock = new Mock<IStudentRepository>();

       

        var testMealBox = new MealBox()
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

        var testMealBoxUpdated = new MealBox()
        {
            MealBoxName = "update!",
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

        mealBoxRepoMock.Setup(c => c.AddMealBox(testMealBox)).Returns(
            testMealBox);

        canteenRepoMock.Setup(c => c.GetCanteenById(It.IsAny<int>()))
            .Returns(new Canteen()
            {
                Id = 1, CanteenName = "TestCanteen", Address = "TestAddress", City = City.Breda, PostalCode = "1234AB",
                WarmMealsprovided = true
            });

        mealBoxRepoMock.Setup(c => c.DeleteMealBoxProducts(testMealBox));

        var sut = new MealBoxService(mealBoxRepoMock.Object,  canteenRepoMock.Object,
            studentRepoMock.Object);
        var result = sut.UpdateMealBox(testMealBoxUpdated, new List<Product>()
        {
            new()
            {
                Id = 1,
                Name = "Salade",
                ContainsAlcohol = false,
                Photo = ""
            }
        });
        Assert.NotNull(result);
        Assert.Equal(0, result.Id);
        Assert.Equal("update!", result.MealBoxName);
        Debug.Assert(result.Products != null, "result.Products != null");
        Assert.Single(result.Products);
        Assert.Equal("Salade", result.Products.ElementAt(0).Name);
    }

    [Fact]
    public void updateMealBox_InValid_Reserved()
    {
        var mealBoxRepoMock = new Mock<IMealBoxRepository>();
        var canteenRepoMock = new Mock<ICanteenRepository>();
        var studentRepoMock = new Mock<IStudentRepository>();

        var product = new Product()
        {
            Id = 1,
            Name = "Kip",
            ContainsAlcohol = false,
            Photo = ""
        };

        var testMealBox = new MealBox()
        {
            MealBoxName = "Een beetje van alles!",
            City = City.Den_Bosch,
            PickupDateTime = DateTime.Today,
            ExpireTime = DateTime.Today.AddDays(1),
            EighteenPlus = false,
            StudentId = 1,
            Price = 5.45m,
            Type = MealType.Box,
            Products = new List<Product>()
            {
                product
            },
            CanteenId = 4,
            WarmMeals = false
        };

        var testMealBoxUpdated = new MealBox()
        {
            MealBoxName = "update!",
            City = City.Den_Bosch,
            PickupDateTime = DateTime.Today,
            ExpireTime = DateTime.Today.AddDays(1),
            EighteenPlus = false,
            StudentId = 1,
            Price = 5.45m,
            Type = MealType.Box,
            Products = new List<Product>()
            {
                product
            },
            CanteenId = 4,
            WarmMeals = false
        };

        mealBoxRepoMock.Setup(c => c.AddMealBox(testMealBox)).Returns(
            testMealBox);

        canteenRepoMock.Setup(c => c.GetCanteenById(It.IsAny<int>()))
            .Returns(new Canteen()
            {
                Id = 1, CanteenName = "TestCanteen", Address = "TestAddress", City = City.Breda, PostalCode = "1234AB",
                WarmMealsprovided = true
            });

        mealBoxRepoMock.Setup(c => c.DeleteMealBoxProducts(testMealBox));

        var sut = new MealBoxService(mealBoxRepoMock.Object,  canteenRepoMock.Object,
            studentRepoMock.Object);
        Assert.Throws<InvalidFormdataException>(() =>
            sut.UpdateMealBox(testMealBoxUpdated, new List<Product>() { product }));
    }

    [Fact]
    public void updateMealBoxInvalid_WarmMealsNotProvided()
    {
        var mealBoxRepoMock = new Mock<IMealBoxRepository>();
        var canteenRepoMock = new Mock<ICanteenRepository>();
        var studentRepoMock = new Mock<IStudentRepository>();

        var product = new Product()
        {
            Id = 1,
            Name = "Kip",
            ContainsAlcohol = false,
            Photo = ""
        };

        var testMealBox = new MealBox()
        {
            MealBoxName = "Een beetje van alles!",
            City = City.Den_Bosch,
            PickupDateTime = DateTime.Today,
            ExpireTime = DateTime.Today.AddDays(1),
            EighteenPlus = false,
            Price = 5.45m,
            Type = MealType.Box,
            Products = new List<Product>()
            {
                product
            },
            CanteenId = 4,
            WarmMeals = false
        };

        var testMealBoxUpdated = new MealBox()
        {
            MealBoxName = "update!",
            City = City.Den_Bosch,
            PickupDateTime = DateTime.Today,
            ExpireTime = DateTime.Today.AddDays(1),
            EighteenPlus = false,
            Price = 5.45m,
            Type = MealType.Box,
            Products = new List<Product>()
            {
                product
            },
            CanteenId = 4,
            WarmMeals = true
        };

        mealBoxRepoMock.Setup(c => c.AddMealBox(testMealBox)).Returns(
            testMealBox);

        canteenRepoMock.Setup(c => c.GetCanteenById(It.IsAny<int>()))
            .Returns(new Canteen()
            {
                Id = 1, CanteenName = "TestCanteen", Address = "TestAddress", City = City.Breda, PostalCode = "1234AB",
                WarmMealsprovided = false
            });

        mealBoxRepoMock.Setup(c => c.DeleteMealBoxProducts(testMealBox));

        var sut = new MealBoxService(mealBoxRepoMock.Object,  canteenRepoMock.Object,
            studentRepoMock.Object);
        Assert.Throws<InvalidFormdataException>(() =>
            sut.UpdateMealBox(testMealBoxUpdated, new List<Product>() { product }));
    }

    [Fact]
    public void updateMealBoxInvalid_ExpirationDate_before_PickupDate()
    {
        var mealBoxRepoMock = new Mock<IMealBoxRepository>();
        var canteenRepoMock = new Mock<ICanteenRepository>();
        var studentRepoMock = new Mock<IStudentRepository>();

        var product = new Product()
        {
            Id = 1,
            Name = "Kip",
            ContainsAlcohol = false,
            Photo = ""
        };

        var testMealBox = new MealBox()
        {
            MealBoxName = "Een beetje van alles!",
            City = City.Den_Bosch,
            PickupDateTime = DateTime.Today,
            ExpireTime = DateTime.Today.AddDays(1),
            EighteenPlus = false,
            Price = 5.45m,
            Type = MealType.Box,
            Products = new List<Product>()
            {
                product
            },
            CanteenId = 4,
            WarmMeals = false
        };

        var testMealBoxUpdated = new MealBox()
        {
            MealBoxName = "update!",
            City = City.Den_Bosch,
            PickupDateTime = DateTime.Today,
            ExpireTime = DateTime.Today.AddDays(-6),
            EighteenPlus = false,
            Price = 5.45m,
            Type = MealType.Box,
            Products = new List<Product>()
            {
                product
            },
            CanteenId = 4,
            WarmMeals = false
        };

        mealBoxRepoMock.Setup(c => c.AddMealBox(testMealBox)).Returns(
            testMealBox);

        canteenRepoMock.Setup(c => c.GetCanteenById(It.IsAny<int>()))
            .Returns(new Canteen()
            {
                Id = 1, CanteenName = "TestCanteen", Address = "TestAddress", City = City.Breda, PostalCode = "1234AB",
                WarmMealsprovided = false
            });

        mealBoxRepoMock.Setup(c => c.DeleteMealBoxProducts(testMealBox));

        var sut = new MealBoxService(mealBoxRepoMock.Object,  canteenRepoMock.Object,
            studentRepoMock.Object);
        Assert.Throws<InvalidFormdataException>(() =>
            sut.UpdateMealBox(testMealBoxUpdated, new List<Product>() { product }));
    }


    [Fact]
    public void updateMealBoxInvalid_PickupDateTooLate()
    {
        var mealBoxRepoMock = new Mock<IMealBoxRepository>();
        var canteenRepoMock = new Mock<ICanteenRepository>();
        var studentRepoMock = new Mock<IStudentRepository>();

        var product = new Product()
        {
            Id = 1,
            Name = "Kip",
            ContainsAlcohol = false,
            Photo = ""
        };

        var testMealBox = new MealBox()
        {
            MealBoxName = "Een beetje van alles!",
            City = City.Den_Bosch,
            PickupDateTime = DateTime.Today.AddDays(5),
            ExpireTime = DateTime.Today.AddDays(10),
            EighteenPlus = false,
            Price = 5.45m,
            Type = MealType.Box,
            Products = new List<Product>()
            {
                product
            },
            CanteenId = 4,
            WarmMeals = false
        };

        var testMealBoxUpdated = new MealBox()
        {
            MealBoxName = "update!",
            City = City.Den_Bosch,
            PickupDateTime = DateTime.Today.AddDays(3),
            ExpireTime = DateTime.Today.AddDays(7),
            EighteenPlus = false,
            Price = 5.45m,
            Type = MealType.Box,
            Products = new List<Product>()
            {
                product
            },
            CanteenId = 4,
            WarmMeals = false
        };

        mealBoxRepoMock.Setup(c => c.AddMealBox(testMealBox)).Returns(
            testMealBox);

        canteenRepoMock.Setup(c => c.GetCanteenById(It.IsAny<int>()))
            .Returns(new Canteen()
            {
                Id = 1, CanteenName = "TestCanteen", Address = "TestAddress", City = City.Breda, PostalCode = "1234AB",
                WarmMealsprovided = false
            });

        mealBoxRepoMock.Setup(c => c.DeleteMealBoxProducts(testMealBox));

        var sut = new MealBoxService(mealBoxRepoMock.Object,  canteenRepoMock.Object,
            studentRepoMock.Object);
        Assert.Throws<InvalidFormdataException>(() =>
            sut.UpdateMealBox(testMealBoxUpdated, new List<Product>() { product }));
    }

    [Fact]
    public void updateMealBoxValid_EighteenPlus()
    {
        var mealBoxRepoMock = new Mock<IMealBoxRepository>();
        var canteenRepoMock = new Mock<ICanteenRepository>();
        var studentRepoMock = new Mock<IStudentRepository>();

        var product = new Product()
        {
            Id = 1,
            Name = "rode wijn",
            ContainsAlcohol = true,
            Photo = ""
        };

        var testMealBox = new MealBox()
        {
            MealBoxName = "Een beetje van alles!",
            City = City.Den_Bosch,
            PickupDateTime = DateTime.Today,
            ExpireTime = DateTime.Today.AddDays(1),
            EighteenPlus = false,
            Price = 5.45m,
            Type = MealType.Box,
            Products = new List<Product>()
            {
                product
            },
            CanteenId = 4,
            WarmMeals = false
        };

        var testMealBoxUpdated = new MealBox()
        {
            MealBoxName = "update!",
            City = City.Den_Bosch,
            PickupDateTime = DateTime.Today,
            ExpireTime = DateTime.Today.AddDays(1),
            EighteenPlus = false,
            Price = 5.45m,
            Type = MealType.Box,
            Products = new List<Product>()
            {
                product
            },
            CanteenId = 4,
            WarmMeals = false
        };

        mealBoxRepoMock.Setup(c => c.AddMealBox(testMealBox)).Returns(
            testMealBox);

        canteenRepoMock.Setup(c => c.GetCanteenById(It.IsAny<int>()))
            .Returns(new Canteen()
            {
                Id = 1, CanteenName = "TestCanteen", Address = "TestAddress", City = City.Breda, PostalCode = "1234AB",
                WarmMealsprovided = true
            });

        mealBoxRepoMock.Setup(c => c.DeleteMealBoxProducts(testMealBox));

        var sut = new MealBoxService(mealBoxRepoMock.Object,  canteenRepoMock.Object,
            studentRepoMock.Object);
        var result = sut.UpdateMealBox(testMealBoxUpdated, new List<Product>() { product });
        Assert.NotNull(result);
        Assert.Equal(0, result.Id);
        Assert.Equal("update!", result.MealBoxName);
        Assert.True(result.EighteenPlus);
    }

    [Fact]
    public void updateMealBoxValid_No_EighteenPlus()
    {
        var mealBoxRepoMock = new Mock<IMealBoxRepository>();
        var canteenRepoMock = new Mock<ICanteenRepository>();
        var studentRepoMock = new Mock<IStudentRepository>();


        var product = new Product()
        {
            Id = 1,
            Name = "Ontbijtkoek",
            ContainsAlcohol = false,
            Photo = ""
        };

        var testMealBox = new MealBox()
        {
            MealBoxName = "Een beetje van alles!",
            City = City.Den_Bosch,
            PickupDateTime = DateTime.Today,
            ExpireTime = DateTime.Today.AddDays(1),
            EighteenPlus = false,
            Price = 5.45m,
            Type = MealType.Box,
            Products = new List<Product>()
            {
                product
            },
            CanteenId = 4,
            WarmMeals = false
        };

        var testMealBoxUpdated = new MealBox()
        {
            MealBoxName = "update!",
            City = City.Den_Bosch,
            PickupDateTime = DateTime.Today,
            ExpireTime = DateTime.Today.AddDays(1),
            EighteenPlus = false,
            Price = 5.45m,
            Type = MealType.Box,
            Products = new List<Product>()
            {
                product
            },
            CanteenId = 4,
            WarmMeals = false
        };

        mealBoxRepoMock.Setup(c => c.AddMealBox(testMealBox)).Returns(
            testMealBox);

        canteenRepoMock.Setup(c => c.GetCanteenById(It.IsAny<int>()))
            .Returns(new Canteen()
            {
                Id = 1, CanteenName = "TestCanteen", Address = "TestAddress", City = City.Breda, PostalCode = "1234AB",
                WarmMealsprovided = true
            });

        mealBoxRepoMock.Setup(c => c.DeleteMealBoxProducts(testMealBox));

        var sut = new MealBoxService(mealBoxRepoMock.Object,  canteenRepoMock.Object,
            studentRepoMock.Object);
        var result = sut.UpdateMealBox(testMealBoxUpdated, new List<Product>() { product });
        Assert.NotNull(result);
        Assert.Equal(0, result.Id);
        Assert.Equal("update!", result.MealBoxName);
        Assert.False(result.EighteenPlus);
    }

    [Fact]
    public void DeleteMealBoxValid()
    {
        var mealBoxRepoMock = new Mock<IMealBoxRepository>();
        var canteenRepoMock = new Mock<ICanteenRepository>();
        var studentRepoMock = new Mock<IStudentRepository>();


        var testMealBox = new MealBox()
        {
            MealBoxName = "Een beetje van alles!",
            City = City.Den_Bosch,
            PickupDateTime = DateTime.Today,
            ExpireTime = DateTime.Today.AddDays(1),
            EighteenPlus = false,
            Price = 5.45m,
            Type = MealType.Box,
            Products = new List<Product>()

            {
                new Product()
                {
                    Id = 1,
                    Name = "rode wijn",
                    ContainsAlcohol = true,
                    Photo = ""
                }
            },
            CanteenId = 4,
            WarmMeals = false
        };

        mealBoxRepoMock.Setup(c => c.GetMealBoxById(It.IsAny<int>()))
            .Returns(testMealBox);

        var sut = new MealBoxService(mealBoxRepoMock.Object,  canteenRepoMock.Object,
            studentRepoMock.Object);
        var result = sut.DeleteMealBox(0);
        Assert.True(result);
    }

    [Fact]
    public void deleteMealBox_InValid_Reserved()
    {
        var mealBoxRepoMock = new Mock<IMealBoxRepository>();
        var canteenRepoMock = new Mock<ICanteenRepository>();
        var studentRepoMock = new Mock<IStudentRepository>();

        var testMealBox = new MealBox()
        {
            MealBoxName = "Een beetje van alles!",
            City = City.Den_Bosch,
            PickupDateTime = DateTime.Today,
            ExpireTime = DateTime.Today.AddDays(1),
            EighteenPlus = false,
            Price = 5.45m,
            StudentId = 1,
            Type = MealType.Box,
            Products = new List<Product>()
            {
                new Product()
                {
                    Id = 1,
                    Name = "rode wijn",
                    ContainsAlcohol = true,
                    Photo = ""
                }
            },
            CanteenId = 4,
            WarmMeals = false
        };
        
        mealBoxRepoMock.Setup(c => c.GetMealBoxById(It.IsAny<int>()))
            .Returns(testMealBox);

        var sut = new MealBoxService(mealBoxRepoMock.Object,  canteenRepoMock.Object,
            studentRepoMock.Object);
        var result = sut.DeleteMealBox(0);
        Assert.False(result);
    }

    [Fact]
    public void ReserveMealBoxValid()
    {
        var mealBoxRepoMock = new Mock<IMealBoxRepository>();
        var canteenRepoMock = new Mock<ICanteenRepository>();
        var studentRepoMock = new Mock<IStudentRepository>();

        var sut = new MealBoxService(mealBoxRepoMock.Object,  canteenRepoMock.Object,
            studentRepoMock.Object);

        var testMealBox = new MealBox()
        {
            MealBoxName = "Een beetje van alles!",
            City = City.Den_Bosch,
            PickupDateTime = DateTime.Today,
            ExpireTime = DateTime.Today.AddDays(1),
            EighteenPlus = false,
            Price = 5.45m,
            Type = MealType.Box,
            Products = new List<Product>()
            {
                new Product()
                {
                    Id = 1,
                    Name = "rode wijn",
                    ContainsAlcohol = true,
                    Photo = ""
                }
            },
            CanteenId = 4,
            WarmMeals = false
        };

        var testStudent = new Student()
        {
            Id = 1,
            FirstName = "Test",
            LastName = "Test",
            email = "email",
            BirthDate = DateTime.Today.AddYears(-20),
            StudentNumber = 12345,
            StudyCity = City.Breda
        };


        mealBoxRepoMock.Setup(c => c.GetMealBoxById(It.IsAny<int>()))
            .Returns(testMealBox);

        studentRepoMock.Setup(c => c.GetStudentById(It.IsAny<int>())).Returns(testStudent);

        var result = sut.ReserveMealBox(1, 1);
        Assert.True(result);
        Assert.Equal(1, testMealBox.StudentId);
    }

    [Fact]
    public void ReserveMealBoxInValid_Already_Reserved()
    {
        var mealBoxRepoMock = new Mock<IMealBoxRepository>();
        var canteenRepoMock = new Mock<ICanteenRepository>();
        var studentRepoMock = new Mock<IStudentRepository>();

        var sut = new MealBoxService(mealBoxRepoMock.Object,  canteenRepoMock.Object,
            studentRepoMock.Object);

        var testMealBox = new MealBox()
        {
            MealBoxName = "Een beetje van alles!",
            City = City.Den_Bosch,
            PickupDateTime = DateTime.Today,
            ExpireTime = DateTime.Today.AddDays(1),
            StudentId = 1,
            EighteenPlus = false,
            Price = 5.45m,
            Type = MealType.Box,
            Products = new List<Product>()
            {
                new Product()
                {
                    Id = 1,
                    Name = "rode wijn",
                    ContainsAlcohol = true,
                    Photo = ""
                }
            },
            CanteenId = 4,
            WarmMeals = false
        };
        var testStudent = new Student()
        {
            Id = 1,
            FirstName = "Test",
            LastName = "Test",
            email = "email",
            BirthDate = DateTime.Today.AddYears(-20),
            StudentNumber = 12345,
            StudyCity = City.Breda
        };


        mealBoxRepoMock.Setup(c => c.GetMealBoxById(It.IsAny<int>()))
            .Returns(testMealBox);


        studentRepoMock.Setup(c => c.GetStudentById(It.IsAny<int>())).Returns(testStudent);


        var result = sut.ReserveMealBox(1, 1);
        Assert.False(result);
    }

    [Fact]
    public void ReserveMealBoxInValid_EighteenPlus()
    {
        var mealBoxRepoMock = new Mock<IMealBoxRepository>();
        var canteenRepoMock = new Mock<ICanteenRepository>();
        var studentRepoMock = new Mock<IStudentRepository>();

        var sut = new MealBoxService(mealBoxRepoMock.Object,  canteenRepoMock.Object,
            studentRepoMock.Object);

        var testMealBox = new MealBox()
        {
            MealBoxName = "Een beetje van alles!",
            City = City.Den_Bosch,
            PickupDateTime = DateTime.Today,
            ExpireTime = DateTime.Today.AddDays(1),
            EighteenPlus = true,
            Price = 5.45m,
            Type = MealType.Box,
            Products = new List<Product>()
            {
                new Product()
                {
                    Id = 1,
                    Name = "rode wijn",
                    ContainsAlcohol = true,
                    Photo = ""
                }
            },
            CanteenId = 4,
            WarmMeals = false
        };
        
        var testStudent = new Student()
        {
            Id = 1,
            FirstName = "Test",
            LastName = "Test",
            email = "email",
            BirthDate = DateTime.Today,
            StudentNumber = 12345,
            StudyCity = City.Breda
        };
        
        mealBoxRepoMock.Setup(c => c.GetMealBoxById(It.IsAny<int>()))
            .Returns(testMealBox);
        
        studentRepoMock.Setup(c => c.GetStudentById(It.IsAny<int>())).Returns(testStudent);
        
        Assert.Throws<InvalidReservationException>(() => sut.ReserveMealBox(1, 1));
    }


    [Fact]
    public void ReserveMealBoxInValid_Already_Reservation_for_the_Day()
    {
        var mealBoxRepoMock = new Mock<IMealBoxRepository>();
        var canteenRepoMock = new Mock<ICanteenRepository>();
        var studentRepoMock = new Mock<IStudentRepository>();

        var sut = new MealBoxService(mealBoxRepoMock.Object,  canteenRepoMock.Object,
            studentRepoMock.Object);

        var testMealBox = new MealBox()
        {
            MealBoxName = "Een beetje van alles!",
            City = City.Den_Bosch,
            PickupDateTime = DateTime.Today,
            ExpireTime = DateTime.Today.AddDays(1),
            EighteenPlus = true,
            Price = 5.45m,
            Type = MealType.Box,
            Products = new List<Product>()
            {
                new Product()
                {
                    Id = 1,
                    Name = "rode wijn",
                    ContainsAlcohol = true,
                    Photo = ""
                }
            },
            CanteenId = 4,
            WarmMeals = false
        };

        var testMealBoxReserved = new MealBox()
        {
            MealBoxName = "Een beetje van alles!",
            City = City.Den_Bosch,
            PickupDateTime = DateTime.Today,
            ExpireTime = DateTime.Today.AddDays(1),
            EighteenPlus = false,
            Price = 5.45m,
            StudentId = 1,
            Type = MealType.Box,
            Products = new List<Product>()
            {
                new Product()
                {
                    Id = 1,
                    Name = "rode wijn",
                    ContainsAlcohol = true,
                    Photo = ""
                }
            },
            CanteenId = 4,
            WarmMeals = false
        };

        var testStudent = new Student()
        {
            Id = 1,
            FirstName = "Test",
            LastName = "Test",
            email = "email",
            BirthDate = DateTime.Today,
            StudentNumber = 12345,
            StudyCity = City.Breda
        };

        mealBoxRepoMock.Setup(c => c.GetMealBoxById(It.IsAny<int>()))
            .Returns(testMealBox);
        mealBoxRepoMock.Setup(c => c.GetReservedMealBoxToday(It.IsAny<int>(), It.IsAny<DateTime>()))
            .Returns(testMealBoxReserved);
        studentRepoMock.Setup(c => c.GetStudentById(It.IsAny<int>())).Returns(testStudent);
        Assert.Throws<InvalidReservationException>(() => sut.ReserveMealBox(1, 1));
    }
}