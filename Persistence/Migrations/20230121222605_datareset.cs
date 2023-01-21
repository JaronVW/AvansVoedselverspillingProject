using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class datareset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MealBoxes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ExpireTime", "PickupDateTime" },
                values: new object[] { new DateTime(2023, 1, 24, 2, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 1, 24, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "MealBoxes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ExpireTime", "PickupDateTime" },
                values: new object[] { new DateTime(2023, 1, 21, 2, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 1, 21, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "MealBoxes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ExpireTime", "PickupDateTime", "WarmMeals" },
                values: new object[] { new DateTime(2023, 1, 21, 2, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 1, 21, 0, 0, 0, 0, DateTimeKind.Local), false });

            migrationBuilder.UpdateData(
                table: "MealBoxes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ExpireTime", "PickupDateTime" },
                values: new object[] { new DateTime(2023, 1, 21, 2, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 1, 21, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "MealBoxes",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ExpireTime", "PickupDateTime" },
                values: new object[] { new DateTime(2023, 1, 21, 2, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 1, 21, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "MealBoxes",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ExpireTime", "PickupDateTime" },
                values: new object[] { new DateTime(2023, 1, 21, 2, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 1, 21, 0, 0, 0, 0, DateTimeKind.Local) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MealBoxes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ExpireTime", "PickupDateTime" },
                values: new object[] { new DateTime(2023, 1, 10, 2, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 1, 10, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "MealBoxes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ExpireTime", "PickupDateTime" },
                values: new object[] { new DateTime(2023, 1, 7, 2, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 1, 7, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "MealBoxes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ExpireTime", "PickupDateTime", "WarmMeals" },
                values: new object[] { new DateTime(2023, 1, 7, 2, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 1, 7, 0, 0, 0, 0, DateTimeKind.Local), true });

            migrationBuilder.UpdateData(
                table: "MealBoxes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ExpireTime", "PickupDateTime" },
                values: new object[] { new DateTime(2023, 1, 7, 2, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 1, 7, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "MealBoxes",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ExpireTime", "PickupDateTime" },
                values: new object[] { new DateTime(2023, 1, 7, 2, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 1, 7, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "MealBoxes",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ExpireTime", "PickupDateTime" },
                values: new object[] { new DateTime(2023, 1, 7, 2, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 1, 7, 0, 0, 0, 0, DateTimeKind.Local) });
        }
    }
}
