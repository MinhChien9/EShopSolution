using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopSolution.Data.Migrations
{
    public partial class ChangeFileLengthType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "FileSize",
                table: "ProductImages",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FileSize",
                table: "ProductImages",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("e24409cb-0a1c-4d7c-a43a-25fb9d47ba65"),
                column: "ConcurrencyStamp",
                value: "99d552db-566c-48d6-8f12-a0bdee9c1d32");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("bae8863e-1029-4e70-8056-5ba1379dde32"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e7f77bfe-1e9c-46e1-b24e-481d96053bba", "AQAAAAEAACcQAAAAEAWcb0A7kpuKfHaBob8Zwz3PTl411+DaMba9v4Qu6viKXenvFU3p7nwb7eWiZLwXjA==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 1, 20, 19, 4, 14, 765, DateTimeKind.Local).AddTicks(5842));
        }
    }
}
