using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class seed_data_fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Canteens",
                keyColumn: "Id",
                keyValue: 2,
                column: "CanteenName",
                value: "Kantine Tilburg");

            migrationBuilder.InsertData(
                table: "Canteens",
                columns: new[] { "Id", "Address", "CanteenName", "City", "PostalCode", "WarmMealsprovided" },
                values: new object[,]
                {
                    { 3, "Lovensdijkstraat 35", "LA", 2, "98765", true },
                    { 4, "laan 120", "HA", 1, "01023", true }
                });

            migrationBuilder.InsertData(
                table: "MealBoxProduct",
                columns: new[] { "MealBoxesId", "ProductsId" },
                values: new object[,]
                {
                    { 5, 1 },
                    { 4, 3 },
                    { 5, 3 }
                });

            migrationBuilder.UpdateData(
                table: "MealBoxes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "MealBoxName", "WarmMeals" },
                values: new object[] { "Pilsener verzameling", false });

            migrationBuilder.UpdateData(
                table: "MealBoxes",
                keyColumn: "Id",
                keyValue: 3,
                column: "MealBoxName",
                value: "verse producten week 10");

            migrationBuilder.UpdateData(
                table: "MealBoxes",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "EighteenPlus", "WarmMeals" },
                values: new object[] { true, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Broodje mozzarella");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Verse salade");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Broodje ei");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Fanta");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Kaasplankje");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2,
                column: "email",
                value: "henkvries@mail.com");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 5,
                column: "email",
                value: "adres@mail.com");

            migrationBuilder.UpdateData(
                table: "MealBoxes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CanteenId", "MealBoxName" },
                values: new object[] { 4, "Een beetje van alles!" });

            migrationBuilder.UpdateData(
                table: "MealBoxes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CanteenId", "MealBoxName", "WarmMeals" },
                values: new object[] { 3, "verse producten week 15", false });

            migrationBuilder.UpdateData(
                table: "MealBoxes",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CanteenId", "EighteenPlus" },
                values: new object[] { 4, false });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Canteens",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Canteens",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MealBoxProduct",
                keyColumns: new[] { "MealBoxesId", "ProductsId" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                table: "MealBoxProduct",
                keyColumns: new[] { "MealBoxesId", "ProductsId" },
                keyValues: new object[] { 4, 3 });

            migrationBuilder.DeleteData(
                table: "MealBoxProduct",
                keyColumns: new[] { "MealBoxesId", "ProductsId" },
                keyValues: new object[] { 5, 3 });

            migrationBuilder.UpdateData(
                table: "Canteens",
                keyColumn: "Id",
                keyValue: 2,
                column: "CanteenName",
                value: "KantineTilburg");

            migrationBuilder.UpdateData(
                table: "MealBoxes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "MealBoxName", "WarmMeals" },
                values: new object[] { "box1", true });

            migrationBuilder.UpdateData(
                table: "MealBoxes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CanteenId", "MealBoxName" },
                values: new object[] { 1, "box2" });

            migrationBuilder.UpdateData(
                table: "MealBoxes",
                keyColumn: "Id",
                keyValue: 3,
                column: "MealBoxName",
                value: "verse producten");

            migrationBuilder.UpdateData(
                table: "MealBoxes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CanteenId", "MealBoxName", "WarmMeals" },
                values: new object[] { 2, "verse producten", true });

            migrationBuilder.UpdateData(
                table: "MealBoxes",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "EighteenPlus", "WarmMeals" },
                values: new object[] { false, true });

            migrationBuilder.UpdateData(
                table: "MealBoxes",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CanteenId", "EighteenPlus" },
                values: new object[] { 1, true });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "broodje mozzarella");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "verse salade");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "broodje ei");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "fanta");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "kaasplankje");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2,
                column: "email",
                value: "henk@mail.com");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 5,
                column: "email",
                value: "denaam@mail.com");
        }
    }
}
