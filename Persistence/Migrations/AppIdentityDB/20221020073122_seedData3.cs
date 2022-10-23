using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations.AppIdentityDB
{
    /// <inheritdoc />
    public partial class seedData3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "af8f4eac-9893-4d47-80d3-6195ad548fbb");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4c5e174e-3b0e-446f-86af-483d56fd7210", "01835eea-e1ae-42f2-ac51-794fe1d135fb", "student", "STUDENT" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8d321519-7ae3-46a8-950a-e0d5bb553357", "AQAAAAEAACcQAAAAECkVRfcwphEBKI+vZ8OKL7MTqvwGWNlDrlU4RBeKGDJAmoFNksCOwp8GUpLuItn+jQ==", "cd5fc7c5-7658-4263-89a8-b89924596705" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8cc2989b-61e1-47ce-a0d8-4eff58c219d6", "AQAAAAEAACcQAAAAEB8KHu2HmavjEJPNs2fsXpvF1S3zwW3KHjKVgQdzHNPWskEUrh+5I6W7FQl1ooLUMA==", "588ca3d9-a79d-4d54-a9c6-598e577728d9" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "4c5e174e-3b0e-446f-86af-483d56fd7210", "9e445865-a24d-4543-a6c6-9443d048cdb9" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "4c5e174e-3b0e-446f-86af-483d56fd7210", "9e445865-a24d-4543-a6c6-9443d048cdb9" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4c5e174e-3b0e-446f-86af-483d56fd7210");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "fd15bd2d-1e1d-472a-abad-20ba1b5cba0d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "99d1e398-0d7a-4a72-8d4f-98f4235ba3a9", "AQAAAAEAACcQAAAAEIwqhLwlgvUhU/6mhlgLPIno7uQ75y7MK9jEWUYITH9Y5jqomlzX10HPUxURWfUamA==", "f86c6385-86b1-43f9-bb46-080b171eb5d5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e710148f-0141-4ab3-a573-f030f6c304d1", "AQAAAAEAACcQAAAAEMVvYPJY/dOXfgT811MiRTBXBEmAHIMv/uAUKeYj3EjGVMXOoolGESmNtKXqd5RF6w==", "e3915428-968a-43af-baad-8d34be404453" });
        }
    }
}
