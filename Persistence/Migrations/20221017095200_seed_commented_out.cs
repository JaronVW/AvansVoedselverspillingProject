using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seedcommentedout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Canteens",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MealBoxes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MealBoxes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Canteens",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Canteens",
                columns: new[] { "Id", "Address", "City", "PostalCode", "WarmMealsprovided" },
                values: new object[,]
                {
                    { 1, "straat 2", 2, "12345", true },
                    { 2, "straat 5", 0, "54321", false }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "ContainsAlcohol", "Name", "Photo" },
                values: new object[,]
                {
                    { 1, true, "Broodje", "test" },
                    { 2, true, "Heiniken", "BIER" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "BirthDate", "FirstName", "LastName", "PhoneNumber", "StudentNumber", "StudyCity", "email" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jaron", "lastname", "12345", 12345, 2, "mai@mail.com" },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "henk", "vries", "54321", 12345, 0, "mai@mail.com" }
                });

            migrationBuilder.InsertData(
                table: "MealBoxes",
                columns: new[] { "Id", "CanteenId", "City", "EighteenPlus", "ExpireTime", "MealBoxName", "PickupDateTime", "Price", "StudentId", "Type" },
                values: new object[,]
                {
                    { 1, 1, 2, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "box1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5.45m, 1, 0 },
                    { 2, 1, 1, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "box2", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5.45m, null, 0 }
                });
        }
    }
}
