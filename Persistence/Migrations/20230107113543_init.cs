using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Canteens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WarmMealsprovided = table.Column<bool>(type: "bit", nullable: true),
                    CanteenName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Canteens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContainsAlcohol = table.Column<bool>(type: "bit", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StudentNumber = table.Column<int>(type: "int", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudyCity = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeNumber = table.Column<int>(type: "int", nullable: false),
                    CanteenId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Canteens_CanteenId",
                        column: x => x.CanteenId,
                        principalTable: "Canteens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MealBoxes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MealBoxName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<int>(type: "int", nullable: false),
                    PickupDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpireTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EighteenPlus = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: true),
                    CanteenId = table.Column<int>(type: "int", nullable: false),
                    WarmMeals = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealBoxes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MealBoxes_Canteens_CanteenId",
                        column: x => x.CanteenId,
                        principalTable: "Canteens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealBoxes_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MealBoxProduct",
                columns: table => new
                {
                    ProductsId = table.Column<int>(type: "int", nullable: false),
                    MealBoxesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealBoxProduct", x => new { x.ProductsId, x.MealBoxesId });
                    table.ForeignKey(
                        name: "FK_MealBoxProduct_MealBoxes_MealBoxesId",
                        column: x => x.MealBoxesId,
                        principalTable: "MealBoxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealBoxProduct_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Canteens",
                columns: new[] { "Id", "Address", "CanteenName", "City", "PostalCode", "WarmMealsprovided" },
                values: new object[,]
                {
                    { 1, "straat 2", "LD", 2, "12345", true },
                    { 2, "straat 5", "KantineTilburg", 0, "54321", false }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "ContainsAlcohol", "Name", "Photo" },
                values: new object[,]
                {
                    { 1, false, "Broodje", "https://gezinoverdekook.nl/wp-content/uploads/Broodje-gezond-recept.jpeg" },
                    { 2, false, "broodje mozzarella", "https://www.modernhoney.com/wp-content/uploads/2019/01/Pesto-Panini-with-Fresh-Mozzarella-and-Tomato-1-crop.jpg" },
                    { 3, false, "verse salade", "https://www.thespruceeats.com/thmb/Z6IWF7c9zywuU9maSIimGLbHoI4=/3000x2000/filters:fill(auto,1)/classic-caesar-salad-recipe-996054-Hero_01-33c94cc8b8e841ee8f2a815816a0af95.jpg" },
                    { 4, false, "broodje ei", "https://www.acouplecooks.com/wp-content/uploads/2020/07/Egg-Salad-Sandwich-001.jpg" },
                    { 5, false, "fanta", "https://cdn11.bigcommerce.com/s-2fq65jrvsu/images/stencil/1280x1280/products/528/7297/fanta_orange-1__30340.1664974218.jpg?c=1" },
                    { 6, false, "kaasplankje", "https://bettyskitchen.nl/wp-content/uploads/2013/12/zelf_kaasplankje_samenstellen_shutterstock_749650144.jpg" },
                    { 7, true, "Hertog Jan", "https://www.drankuwel.nl/media/catalog/product/cache/d6a5bc6be806788c48ed774973599767/h/e/hertogjan-8packjpg.jpg" },
                    { 8, true, "Heineken", "https://static.ah.nl/dam/product/AHI_43545239383731303039?revLabel=1&rendition=800x800_JPG_Q90&fileType=binary" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "BirthDate", "FirstName", "LastName", "PhoneNumber", "StudentNumber", "StudyCity", "email" },
                values: new object[,]
                {
                    { 1, new DateTime(2002, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jaron", "lastname", "12345", 12345, 2, "student@email.com" },
                    { 2, new DateTime(2010, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "henk", "vries", "54321", 12345, 0, "henk@mail.com" },
                    { 3, new DateTime(2010, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "henk", "das", "54321", 12345, 0, "henkd@mail.com" },
                    { 4, new DateTime(1970, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Meneer", "student", "54321", 12345, 0, "studentmeneer@mail.com" },
                    { 5, new DateTime(2001, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lucas", "naam", "54321", 12345, 0, "denaam@mail.com" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CanteenId", "Email", "EmployeeNumber", "FirstName", "LastName" },
                values: new object[] { 1, 1, "email@email.com", 1, "mede", "werker" });

            migrationBuilder.InsertData(
                table: "MealBoxes",
                columns: new[] { "Id", "CanteenId", "City", "EighteenPlus", "ExpireTime", "MealBoxName", "PickupDateTime", "Price", "StudentId", "Type", "WarmMeals" },
                values: new object[,]
                {
                    { 1, 1, 2, true, new DateTime(2023, 1, 10, 2, 0, 0, 0, DateTimeKind.Local), "box1", new DateTime(2023, 1, 10, 0, 0, 0, 0, DateTimeKind.Local), 5.45m, 1, 0, true },
                    { 2, 1, 1, false, new DateTime(2023, 1, 7, 2, 0, 0, 0, DateTimeKind.Local), "box2", new DateTime(2023, 1, 7, 0, 0, 0, 0, DateTimeKind.Local), 5.45m, null, 0, true },
                    { 3, 1, 2, false, new DateTime(2023, 1, 7, 2, 0, 0, 0, DateTimeKind.Local), "verse producten", new DateTime(2023, 1, 7, 0, 0, 0, 0, DateTimeKind.Local), 6.50m, null, 0, true },
                    { 4, 2, 2, false, new DateTime(2023, 1, 7, 2, 0, 0, 0, DateTimeKind.Local), "verse producten", new DateTime(2023, 1, 7, 0, 0, 0, 0, DateTimeKind.Local), 6.50m, null, 0, true },
                    { 5, 2, 0, false, new DateTime(2023, 1, 7, 2, 0, 0, 0, DateTimeKind.Local), "nog versere producten", new DateTime(2023, 1, 7, 0, 0, 0, 0, DateTimeKind.Local), 6.50m, null, 0, true },
                    { 6, 1, 1, true, new DateTime(2023, 1, 7, 2, 0, 0, 0, DateTimeKind.Local), "oude producten", new DateTime(2023, 1, 7, 0, 0, 0, 0, DateTimeKind.Local), 6.50m, null, 0, true }
                });

            migrationBuilder.InsertData(
                table: "MealBoxProduct",
                columns: new[] { "MealBoxesId", "ProductsId" },
                values: new object[,]
                {
                    { 2, 1 },
                    { 2, 2 },
                    { 6, 2 },
                    { 2, 5 },
                    { 2, 6 },
                    { 1, 7 },
                    { 5, 7 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CanteenId",
                table: "Employees",
                column: "CanteenId");

            migrationBuilder.CreateIndex(
                name: "IX_MealBoxes_CanteenId",
                table: "MealBoxes",
                column: "CanteenId");

            migrationBuilder.CreateIndex(
                name: "IX_MealBoxes_StudentId",
                table: "MealBoxes",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_MealBoxProduct_MealBoxesId",
                table: "MealBoxProduct",
                column: "MealBoxesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "MealBoxProduct");

            migrationBuilder.DropTable(
                name: "MealBoxes");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Canteens");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
