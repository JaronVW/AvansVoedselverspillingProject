using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations.AppIdentityDB
{
    /// <inheritdoc />
    public partial class seedData2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "99d1e398-0d7a-4a72-8d4f-98f4235ba3a9", "EMAIL@EMAIL.COM", "AQAAAAEAACcQAAAAEIwqhLwlgvUhU/6mhlgLPIno7uQ75y7MK9jEWUYITH9Y5jqomlzX10HPUxURWfUamA==", "f86c6385-86b1-43f9-bb46-080b171eb5d5" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "9e445865-a24d-4543-a6c6-9443d048cdb9", 0, "e710148f-0141-4ab3-a573-f030f6c304d1", null, false, false, null, null, "STUDENT@EMAIL.COM", "AQAAAAEAACcQAAAAEMVvYPJY/dOXfgT811MiRTBXBEmAHIMv/uAUKeYj3EjGVMXOoolGESmNtKXqd5RF6w==", null, false, "e3915428-968a-43af-baad-8d34be404453", false, "student@email.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e445865-a24d-4543-a6c6-9443d048cdb9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "dcb063b5-e1a1-442f-b653-8a9d5caf9d9c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a4f970eb-3c63-41ec-8687-db1801eb8550", "MYUSER", "AQAAAAEAACcQAAAAEO7uQhIbRlNU04NUfOALEcvPoK+EEbgcwOU7YwZ7z7FUA7MqS4nJ4UXWc6femcfmKw==", "ba0958d4-5f43-454d-a833-111f3af4fa4b" });
        }
    }
}
