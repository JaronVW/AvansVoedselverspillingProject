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
    [Fact]
    public void GetMealBoxes()
    {
        Mock<IMealBoxRepository> mockMealBoxRepo = new Mock<IMealBoxRepository>();
        mockMealBoxRepo.Setup(m => m.GetMealBoxes()).Returns(new List<MealBox>());
        var mealBoxRepo = mockMealBoxRepo.Object;
        var mealBoxes = mealBoxRepo.GetMealBoxes();
        Assert.NotNull(mealBoxes);
    }

    [Fact]
    public void GetMealBoxById()
    {
        Mock<IMealBoxRepository> mockMealBoxRepo = new Mock<IMealBoxRepository>();
        mockMealBoxRepo.Setup(m => m.GetMealBoxById(1)).Returns(new MealBox());
        var mealBoxRepo = mockMealBoxRepo.Object;
        var mealBox = mealBoxRepo.GetMealBoxById(1);
        Assert.NotNull(mealBox);
    }
}