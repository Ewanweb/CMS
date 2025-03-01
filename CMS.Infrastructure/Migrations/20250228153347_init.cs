using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Decription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductsGallery",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsGallery", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductsGallery_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedTime", "Name", "Slug", "UpDateTime" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 2, 28, 19, 3, 46, 364, DateTimeKind.Local).AddTicks(5324), "Shirts", "shirts", null },
                    { 2, new DateTime(2025, 2, 28, 19, 3, 46, 365, DateTimeKind.Local).AddTicks(7433), "Fruits", "fruits", null }
                });

            migrationBuilder.InsertData(
                table: "Pages",
                columns: new[] { "Id", "Body", "CreatedTime", "Order", "Slug", "Title", "UpDateTime" },
                values: new object[,]
                {
                    { 1, "This is the Home Page", new DateTime(2025, 2, 28, 19, 3, 46, 367, DateTimeKind.Local).AddTicks(9607), 100, "home", "Home", null },
                    { 2, "This is the About Page", new DateTime(2025, 2, 28, 19, 3, 46, 368, DateTimeKind.Local).AddTicks(1142), 100, "about", "About", null },
                    { 3, "This is the Services Page", new DateTime(2025, 2, 28, 19, 3, 46, 368, DateTimeKind.Local).AddTicks(1149), 100, "services", "Services", null },
                    { 4, "This is the Contact Page", new DateTime(2025, 2, 28, 19, 3, 46, 368, DateTimeKind.Local).AddTicks(1150), 100, "contact", "Contact", null }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedTime", "Decription", "Image", "Name", "Price", "Slug", "UpDateTime" },
                values: new object[,]
                {
                    { 1, 2, new DateTime(2025, 2, 28, 19, 3, 46, 367, DateTimeKind.Local).AddTicks(1645), "Juicy Appels", "apple1.jpg", "Appel", 1.50m, "appel", null },
                    { 2, 2, new DateTime(2025, 2, 28, 19, 3, 46, 367, DateTimeKind.Local).AddTicks(7727), "Juicy Banana", "grapes3.jpg", "Banana", 2m, "banana", null },
                    { 3, 2, new DateTime(2025, 2, 28, 19, 3, 46, 367, DateTimeKind.Local).AddTicks(7761), "Juicy Grapers", "mand1.jpg", "Grapers", 2.5m, "grapers", null },
                    { 4, 2, new DateTime(2025, 2, 28, 19, 3, 46, 367, DateTimeKind.Local).AddTicks(7764), "Juicy Oranges", "mand2.jpg", "Oranges", 25.5m, "oranges", null },
                    { 5, 1, new DateTime(2025, 2, 28, 19, 3, 46, 367, DateTimeKind.Local).AddTicks(7765), "Nice Blue Shirt", "blue1.jpg", "Blue Shirt", 15.5m, "blue-shirt", null },
                    { 6, 1, new DateTime(2025, 2, 28, 19, 3, 46, 367, DateTimeKind.Local).AddTicks(7767), "Nice Yellow Shirt", "yellow1.png", "Yellow Shirt", 55.5m, "yellow-shirt", null },
                    { 7, 1, new DateTime(2025, 2, 28, 19, 3, 46, 367, DateTimeKind.Local).AddTicks(7768), "Nice green Shirt", "green4.png", "Green Shirt", 65.5m, "green-shirt", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsGallery_ProductId",
                table: "ProductsGallery",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropTable(
                name: "ProductsGallery");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
