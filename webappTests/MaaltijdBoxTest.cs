// using Core.DomainServices;
// using Domain;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Mvc;
// using Moq;
// using VoedselVerspillingWebApp.Controllers;
//
// namespace webappTests;
//
// public class MaaltijdBoxTest
// {
//     private MealBox box1 = new MealBox()
//     {
//         Id = 1,
//         MealBoxName = "box1",
//         City = City.Breda,
//         PickupDateTime = DateTime.Today.AddDays(3),
//         ExpireTime = DateTime.Today.AddDays(3).AddHours(2),
//         EighteenPlus = true,
//         Price = 5.45m,
//         Type = MealType.Box,
//         Products = new List<Product>(),
//         CanteenId = 1
//     };
//
//
//     private MealBox box2 = new MealBox()
//     {
//         Id = 2,
//         MealBoxName = "box2",
//         City = City.Den_Bosch,
//         PickupDateTime = DateTime.Today,
//         ExpireTime = DateTime.Today.AddHours(2),
//         EighteenPlus = false,
//         Price = 5.45m,
//         Type = MealType.Box,
//         Products = new List<Product>(),
//         CanteenId = 1
//     };
//
//     [Fact]
//     public void MaaltijdBoxController_Should_return_non_reserved_meal_page()
//     {
//         var MealBoxMock = new Mock<IMealBoxRepository>();
//         var CanteenMock = new Mock<ICanteenRepository>();
//         var StudentMock = new Mock<IStudentRepository>();
//         var EmployeeMock = new Mock<IEmployeeRepository>();
//         var ProductMock = new Mock<IProductRepository>();
//
//         var controller = new MaaltijdboxController(
//             MealBoxMock.Object,
//             CanteenMock.Object,
//             StudentMock.Object,
//             EmployeeMock.Object,
//             ProductMock.Object
//         );
//
//         MealBoxMock.Setup(repository => repository.GetMealBoxes()).Returns(new List<MealBox>()
//         {
//             box1, box2
//         });
//
//         var result = controller.Index() as ViewResult;
//
//         Assert.Null((result.ViewName));
//     }
//
//     [Fact]
//     public void MaaltijdBoxController_Should_return_reserved_meal_page()
//     {
//         var MealBoxMock = new Mock<IMealBoxRepository>();
//         var CanteenMock = new Mock<ICanteenRepository>();
//         var StudentMock = new Mock<IStudentRepository>();
//         var EmployeeMock = new Mock<IEmployeeRepository>();
//         var ProductMock = new Mock<IProductRepository>();
//         var mockUser = new Mock<UserManager<IdentityUser>>();
//         
//         mockUser.Setup(userManager => userManager.FindByIdAsync(It.IsAny<string>()))
//             .ReturnsAsync(new IdentityUser { Id = "1234", UserName = "mail@mail.com", Email = "mail@mail.com" });
//         mockUser.Setup(userManager => userManager.IsInRoleAsync(It.IsAny<IdentityUser>(), "student"))
//             .ReturnsAsync(true);
//
//         var controller = new MaaltijdboxController(
//             MealBoxMock.Object,
//             CanteenMock.Object,
//             StudentMock.Object,
//             EmployeeMock.Object,
//             ProductMock.Object
//             
//         );
//         
//         
//
//         MealBoxMock.Setup(repository => repository.GetMealBoxes()).Returns(new List<MealBox>()
//         {
//             box1, box2
//         });
//
//
//         var result = controller.Gereserveerd() as ViewResult;
//        
//         Assert.Equal("Gereserveerd", (result.ViewName));
//     }
// }