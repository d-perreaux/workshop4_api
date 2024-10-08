using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Workshop_API.Migrations
{
    /// <inheritdoc />
    public partial class SeedProductData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DCI",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Dosage",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "FlagIsDelete",
                table: "Product",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "CIP", "DCI", "Dosage", "FlagIsDelete", "Name" },
                values: new object[,]
                {
                    { 1, 1234567, "Paracetamol", "500mg", false, "Doliprane" },
                    { 2, 2345678, "Ibuprofen", "400mg", false, "Advil" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "DCI",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Dosage",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "FlagIsDelete",
                table: "Product");
        }
    }
}
