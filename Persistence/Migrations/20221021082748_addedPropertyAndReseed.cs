using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedPropertyAndReseed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "WarmMeals",
                table: "MealBoxes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "MealBoxes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ExpireTime", "PickupDateTime", "WarmMeals" },
                values: new object[] { new DateTime(2022, 10, 24, 2, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 10, 24, 0, 0, 0, 0, DateTimeKind.Local), true });

            migrationBuilder.UpdateData(
                table: "MealBoxes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ExpireTime", "PickupDateTime", "WarmMeals" },
                values: new object[] { new DateTime(2022, 10, 21, 2, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 10, 21, 0, 0, 0, 0, DateTimeKind.Local), true });

            migrationBuilder.UpdateData(
                table: "MealBoxes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ExpireTime", "PickupDateTime", "WarmMeals" },
                values: new object[] { new DateTime(2022, 10, 21, 2, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 10, 21, 0, 0, 0, 0, DateTimeKind.Local), true });

            migrationBuilder.UpdateData(
                table: "MealBoxes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ExpireTime", "PickupDateTime", "WarmMeals" },
                values: new object[] { new DateTime(2022, 10, 21, 2, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 10, 21, 0, 0, 0, 0, DateTimeKind.Local), true });

            migrationBuilder.UpdateData(
                table: "MealBoxes",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ExpireTime", "PickupDateTime", "WarmMeals" },
                values: new object[] { new DateTime(2022, 10, 21, 2, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 10, 21, 0, 0, 0, 0, DateTimeKind.Local), true });

            migrationBuilder.UpdateData(
                table: "MealBoxes",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ExpireTime", "PickupDateTime", "WarmMeals" },
                values: new object[] { new DateTime(2022, 10, 21, 2, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 10, 21, 0, 0, 0, 0, DateTimeKind.Local), true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WarmMeals",
                table: "MealBoxes");

            migrationBuilder.UpdateData(
                table: "MealBoxes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ExpireTime", "PickupDateTime" },
                values: new object[] { new DateTime(2022, 10, 23, 2, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 10, 23, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "MealBoxes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ExpireTime", "PickupDateTime" },
                values: new object[] { new DateTime(2022, 10, 20, 2, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 10, 20, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "MealBoxes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ExpireTime", "PickupDateTime" },
                values: new object[] { new DateTime(2022, 10, 20, 2, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 10, 20, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "MealBoxes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ExpireTime", "PickupDateTime" },
                values: new object[] { new DateTime(2022, 10, 20, 2, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 10, 20, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "MealBoxes",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ExpireTime", "PickupDateTime" },
                values: new object[] { new DateTime(2022, 10, 20, 2, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 10, 20, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "MealBoxes",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ExpireTime", "PickupDateTime" },
                values: new object[] { new DateTime(2022, 10, 20, 2, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 10, 20, 0, 0, 0, 0, DateTimeKind.Local) });
        }
    }
}
