using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopSolution.Data.Migrations
{
    public partial class SeedDataIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("e24409cb-0a1c-4d7c-a43a-25fb9d47ba65"), "d09aa5c5-26bb-44e7-aeee-9e2165d6789d", "Admin Role", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("e24409cb-0a1c-4d7c-a43a-25fb9d47ba65"), new Guid("bae8863e-1029-4e70-8056-5ba1379dde32") });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Dob", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("bae8863e-1029-4e70-8056-5ba1379dde32"), 0, "016f5b73-71a0-45a7-97fa-0dff7fcf0c12", new DateTime(1999, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "chien17099@gmail.com", true, "Chien", "Huynh", false, null, "chien17099@gmail.com", "admin", "AQAAAAEAACcQAAAAEGz8Wu49opecIlrU+43DR0DtX84AbdgqM0Oln11thEk9TxEshkzc3KXFPZZOlhXoDw==", null, false, "", false, "admin" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 1, 15, 20, 21, 57, 445, DateTimeKind.Local).AddTicks(2203));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("e24409cb-0a1c-4d7c-a43a-25fb9d47ba65"));

            migrationBuilder.DeleteData(
                table: "AppUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("e24409cb-0a1c-4d7c-a43a-25fb9d47ba65"), new Guid("bae8863e-1029-4e70-8056-5ba1379dde32") });

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("bae8863e-1029-4e70-8056-5ba1379dde32"));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 1, 15, 20, 12, 57, 443, DateTimeKind.Local).AddTicks(8228));
        }
    }
}
