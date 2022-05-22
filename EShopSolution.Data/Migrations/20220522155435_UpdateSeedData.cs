using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopSolution.Data.Migrations
{
    public partial class UpdateSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("e24409cb-0a1c-4d7c-a43a-25fb9d47ba65"),
                column: "ConcurrencyStamp",
                value: "20507332-0767-4acd-a333-e9e99bdccf0f");

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("534dbe06-ba7a-481e-a2a7-7b2724427d7c"), "b742dcd2-1c83-4c61-8d6a-e47e57911f7b", "User Role", "user", "user" });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("534dbe06-ba7a-481e-a2a7-7b2724427d7c"), new Guid("5aadc56f-8eaa-4fe0-9a23-bf8c4320024c") });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("bae8863e-1029-4e70-8056-5ba1379dde32"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e9e98e53-aa07-48d5-bd75-14f0aaabb7b2", "AQAAAAEAACcQAAAAEMiwAFajdmDUkcjSGf3vcO2/mg7jREP42LZAoxJ8hABPiSHxFI9xWFBI+1yLtuoybw==" });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Dob", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("5aadc56f-8eaa-4fe0-9a23-bf8c4320024c"), 0, "2b4069d1-0740-4d07-9b72-daa83fe2515e", new DateTime(1999, 11, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "user1@gmail.com", true, "User", "01", false, null, "user1@gmail.com", "user1", "AQAAAAEAACcQAAAAEHQuqVchjdEkXKbqealBuE0jl4W0RRpdg1jC7R3xkyz0CJX0WSLcRDPKHCmoBi4zOw==", null, false, "", false, "user1" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 5, 22, 22, 54, 34, 342, DateTimeKind.Local).AddTicks(8875));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("534dbe06-ba7a-481e-a2a7-7b2724427d7c"));

            migrationBuilder.DeleteData(
                table: "AppUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("534dbe06-ba7a-481e-a2a7-7b2724427d7c"), new Guid("5aadc56f-8eaa-4fe0-9a23-bf8c4320024c") });

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("5aadc56f-8eaa-4fe0-9a23-bf8c4320024c"));

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("e24409cb-0a1c-4d7c-a43a-25fb9d47ba65"),
                column: "ConcurrencyStamp",
                value: "208675ec-98e9-47c4-90d3-12c2d845d44a");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("bae8863e-1029-4e70-8056-5ba1379dde32"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b0e1de79-f998-4228-a2e8-52f9b9c370dd", "AQAAAAEAACcQAAAAEF+9UMmJEOPg/7amNmonohxNoA/i2a3i7OvJU1n1TJhkYCNzFF2QIwj/YcnDgaYk+Q==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 5, 20, 21, 32, 36, 365, DateTimeKind.Local).AddTicks(5968));
        }
    }
}
