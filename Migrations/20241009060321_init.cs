using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Workshop_API.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Advice",
                columns: table => new
                {
                    AdviceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FlagIsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advice", x => x.AdviceId);
                });

            migrationBuilder.CreateTable(
                name: "Pharmacy",
                columns: table => new
                {
                    PharmacyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pharmacy", x => x.PharmacyId);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CIP = table.Column<int>(type: "int", nullable: false),
                    DCI = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dosage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FlagIsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "Prescription",
                columns: table => new
                {
                    PrescriptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PharmacyId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescription", x => x.PrescriptionId);
                    table.ForeignKey(
                        name: "FK_Prescription_Pharmacy_PharmacyId",
                        column: x => x.PharmacyId,
                        principalTable: "Pharmacy",
                        principalColumn: "PharmacyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductAdvice",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    AdviceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAdvice", x => new { x.ProductId, x.AdviceId });
                    table.ForeignKey(
                        name: "FK_ProductAdvice_Advice_AdviceId",
                        column: x => x.AdviceId,
                        principalTable: "Advice",
                        principalColumn: "AdviceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductAdvice_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrescriptionProducts",
                columns: table => new
                {
                    PrescriptionId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescriptionProducts", x => new { x.PrescriptionId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_PrescriptionProducts_Prescription_PrescriptionId",
                        column: x => x.PrescriptionId,
                        principalTable: "Prescription",
                        principalColumn: "PrescriptionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrescriptionProducts_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PrescriptionProductAdvice",
                columns: table => new
                {
                    PrescriptionId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    AdviceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescriptionProductAdvice", x => new { x.PrescriptionId, x.ProductId, x.AdviceId });
                    table.ForeignKey(
                        name: "FK_PrescriptionProductAdvice_Advice_AdviceId",
                        column: x => x.AdviceId,
                        principalTable: "Advice",
                        principalColumn: "AdviceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrescriptionProductAdvice_PrescriptionProducts_PrescriptionId_ProductId",
                        columns: x => new { x.PrescriptionId, x.ProductId },
                        principalTable: "PrescriptionProducts",
                        principalColumns: new[] { "PrescriptionId", "ProductId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Advice",
                columns: new[] { "AdviceId", "Content", "DateEnd", "DateStart", "FlagIsDeleted", "Type" },
                values: new object[,]
                {
                    { 1, "Pendant les repas", null, new DateTime(2024, 10, 9, 8, 3, 21, 569, DateTimeKind.Local).AddTicks(6371), false, "Posologie" },
                    { 2, "Se rincer la bouche après utilisation", null, new DateTime(2024, 10, 9, 8, 3, 21, 569, DateTimeKind.Local).AddTicks(6375), false, "Précaution" },
                    { 3, "Pas d'activité physique intense", null, new DateTime(2024, 10, 9, 8, 3, 21, 569, DateTimeKind.Local).AddTicks(6378), false, "Précaution" },
                    { 4, "Uniquement le matin", null, new DateTime(2024, 10, 9, 8, 3, 21, 569, DateTimeKind.Local).AddTicks(6381), false, "Précaution" }
                });

            migrationBuilder.InsertData(
                table: "Pharmacy",
                columns: new[] { "PharmacyId", "Name" },
                values: new object[,]
                {
                    { 1, "Pharmacie de l'Eurotéléport" },
                    { 2, "Pharmacie de la Mitterie" },
                    { 3, "Pharmacie de la Vigne" },
                    { 4, "Pharmacie Art Nouveau" }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "CIP", "DCI", "Dosage", "FlagIsDelete", "Name" },
                values: new object[,]
                {
                    { 1, 1234567, "Paracetamol", "500mg", false, "Doliprane" },
                    { 2, 2345678, "Ibuprofen", "400mg", false, "Advil" },
                    { 3, 2345679, "Propionate de fluticasone", "250µg", false, "Flixotide" },
                    { 4, 2345677, "Prednisolone", "20mg", false, "Solupred" },
                    { 5, 2345677, "Lévofloxaxine ", "500mg", false, "Tavanic" }
                });

            migrationBuilder.InsertData(
                table: "Prescription",
                columns: new[] { "PrescriptionId", "Date", "PharmacyId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 9, 8, 3, 21, 569, DateTimeKind.Local).AddTicks(6413), 1 },
                    { 2, new DateTime(2024, 10, 9, 8, 3, 21, 569, DateTimeKind.Local).AddTicks(6415), 2 },
                    { 3, new DateTime(2024, 10, 9, 8, 3, 21, 569, DateTimeKind.Local).AddTicks(6417), 1 }
                });

            migrationBuilder.InsertData(
                table: "ProductAdvice",
                columns: new[] { "AdviceId", "ProductId" },
                values: new object[,]
                {
                    { 1, 4 },
                    { 4, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_PharmacyId",
                table: "Prescription",
                column: "PharmacyId");

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionProductAdvice_AdviceId",
                table: "PrescriptionProductAdvice",
                column: "AdviceId");

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionProducts_ProductId",
                table: "PrescriptionProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAdvice_AdviceId",
                table: "ProductAdvice",
                column: "AdviceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrescriptionProductAdvice");

            migrationBuilder.DropTable(
                name: "ProductAdvice");

            migrationBuilder.DropTable(
                name: "PrescriptionProducts");

            migrationBuilder.DropTable(
                name: "Advice");

            migrationBuilder.DropTable(
                name: "Prescription");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Pharmacy");
        }
    }
}
