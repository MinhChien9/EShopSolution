using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopSolution.Data.Migrations
{
    public partial class UpdateAdminPassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("e24409cb-0a1c-4d7c-a43a-25fb9d47ba65"),
                column: "ConcurrencyStamp",
                value: "494df2d1-4019-422e-9ac7-dd9a0a3dcf7a");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("bae8863e-1029-4e70-8056-5ba1379dde32"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f8c5f888-85b5-444b-9c15-9b839844aa4b", "AQAAAAEAACcQAAAAENZFyEiEyUql5U23IDEIfe/kEk3/c0A3VQ92ugpTMKyZYBazcCQk91Tn8o1Kp+Zipw==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 1, 21, 1, 2, 38, 275, DateTimeKind.Local).AddTicks(9251));
        }
    }
}
