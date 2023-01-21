using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations.AppIdentityDB
{
    public partial class newStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "1a5f8565-f0ad-482a-af9a-fd2340760494");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "1c0b253d-56ad-4040-b494-3b90ce82be81");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5e8a6266-b71d-4376-b599-e38fae7a78c8", "AQAAAAEAACcQAAAAEFZ/sEKLdn+HKAuXtO+I3i9YoGewiR4WHiY1DpQep/QPdEAp6oLMouDA2kPLefEKTQ==", "cd7a9e3b-2b7e-4d0b-9a9d-2b15f29f6a75" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "87fbb40b-77d9-4d10-8265-6d692f284192", "AQAAAAEAACcQAAAAEMbOvi0+dAoXwPWaKB6Ta11uWP6/yT3j/zUQeVTqCOHKrl3jabVoVicO4VWitcy8MQ==", "c2b2c4f6-5a6a-41ac-ac0c-c7a62b7ee270" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "6fcb4b84-c036-43a3-9e77-7d6a75d1cc09", 0, "41c483af-f3a1-4f1f-88fe-e7f0ea294b4b", null, false, false, null, null, "HENKVRIES@GMAIL.COM", "AQAAAAEAACcQAAAAEDRDw5J7EfD+iqpwTy9Ysv0uxbKacYxlI4ZKseDN958/YnMXQeMWyVEWXFWxZ1ezcA==", null, false, "51fba549-68f3-4c0a-acc5-a7ad430ab9ac", false, "henkvries@mail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "4c5e174e-3b0e-446f-86af-483d56fd7210", "6fcb4b84-c036-43a3-9e77-7d6a75d1cc09" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "4c5e174e-3b0e-446f-86af-483d56fd7210", "6fcb4b84-c036-43a3-9e77-7d6a75d1cc09" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6fcb4b84-c036-43a3-9e77-7d6a75d1cc09");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "af8f4eac-9893-4d47-80d3-6195ad548fbb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "01835eea-e1ae-42f2-ac51-794fe1d135fb");

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
        }
    }
}
