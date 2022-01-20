using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopSolution.Data.Migrations
{
    public partial class AddProductImageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    FileSize = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("e24409cb-0a1c-4d7c-a43a-25fb9d47ba65"),
                column: "ConcurrencyStamp",
                value: "02302444-ebd4-4978-8cd0-f3a3783b21ea");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("bae8863e-1029-4e70-8056-5ba1379dde32"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "aab77ae3-47e4-471f-adbd-07ea27a620a0", "AQAAAAEAACcQAAAAEDcsXsMksLgo2h8T5wzSTaJgfPng9b6394vQBk1tJYYIIYtQJ0A+cC8IC9oM7yUhyw==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 1, 20, 17, 55, 49, 356, DateTimeKind.Local).AddTicks(5429));

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("e24409cb-0a1c-4d7c-a43a-25fb9d47ba65"),
                column: "ConcurrencyStamp",
                value: "d09aa5c5-26bb-44e7-aeee-9e2165d6789d");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("bae8863e-1029-4e70-8056-5ba1379dde32"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "016f5b73-71a0-45a7-97fa-0dff7fcf0c12", "AQAAAAEAACcQAAAAEGz8Wu49opecIlrU+43DR0DtX84AbdgqM0Oln11thEk9TxEshkzc3KXFPZZOlhXoDw==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 1, 15, 20, 21, 57, 445, DateTimeKind.Local).AddTicks(2203));
        }
    }
}
