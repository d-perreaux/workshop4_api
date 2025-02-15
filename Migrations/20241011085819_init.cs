﻿using System;
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
                    CIP = table.Column<long>(type: "bigint", nullable: false),
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
                    { 1, "Pendant les repas", null, new DateTime(2024, 10, 11, 10, 58, 19, 183, DateTimeKind.Local).AddTicks(9234), false, "Posologie" },
                    { 2, "Se rincer la bouche après utilisation", null, new DateTime(2024, 10, 11, 10, 58, 19, 183, DateTimeKind.Local).AddTicks(9241), false, "Précaution" },
                    { 3, "Pas d'activité physique intense", null, new DateTime(2024, 10, 11, 10, 58, 19, 183, DateTimeKind.Local).AddTicks(9246), false, "Précaution" },
                    { 4, "Uniquement le matin", null, new DateTime(2024, 10, 11, 10, 58, 19, 183, DateTimeKind.Local).AddTicks(9252), false, "Précaution" },
                    { 5, "4/jour max, 1 toutes les 6h", null, new DateTime(2024, 10, 11, 10, 58, 19, 183, DateTimeKind.Local).AddTicks(9258), false, "Précaution" },
                    { 9, "Ne pas boire de thé dans l'heure qui suit", null, new DateTime(2024, 10, 11, 10, 58, 19, 183, DateTimeKind.Local).AddTicks(9264), false, "Précaution" },
                    { 10, "A prendre avec un grand verre d'eau du robinet", null, new DateTime(2024, 10, 11, 10, 58, 19, 183, DateTimeKind.Local).AddTicks(9270), false, "Précaution" },
                    { 11, "Ne pas s'allonger dans les 30 minutes", null, new DateTime(2024, 10, 11, 10, 58, 19, 183, DateTimeKind.Local).AddTicks(9275), false, "Précaution" },
                    { 12, "Ne pas écraser, ne pas croquer, ne pas dissoudre", null, new DateTime(2024, 10, 11, 10, 58, 19, 183, DateTimeKind.Local).AddTicks(9282), false, "Précaution" },
                    { 13, "4 CàS/jour, la dernière avant 17h", null, new DateTime(2024, 10, 11, 10, 58, 19, 183, DateTimeKind.Local).AddTicks(9288), false, "Précaution" },
                    { 14, "Espacer de 2h la prise des autres médicaments", null, new DateTime(2024, 10, 11, 10, 58, 19, 183, DateTimeKind.Local).AddTicks(9293), false, "Précaution" }
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
                    { 1, 1234567L, "Paracetamol", "500mg", false, "Doliprane" },
                    { 2, 2345678L, "Ibuprofen", "400mg", false, "Advil" },
                    { 3, 2345679L, "Propionate de fluticasone", "250µg", false, "Flixotide" },
                    { 4, 2345677L, "Prednisolone", "20mg", false, "Solupred" },
                    { 5, 2345677L, "Lévofloxaxine", "500mg", false, "Tavanic" },
                    { 6, 3400933518004L, "Sulfate ferreux", "80mg", false, "Tardyferon" },
                    { 7, 3400935956378L, "Acide alendronique", "70mg", false, "Fosamax" },
                    { 8, 3400935956878L, "Carbocistéine ", "5%", false, "Bronchokod" },
                    { 9, 3400931923077L, "Diosmectite ", "3g", false, "Smecta" }
                });

            migrationBuilder.InsertData(
                table: "Prescription",
                columns: new[] { "PrescriptionId", "Date", "PharmacyId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 11, 10, 58, 19, 183, DateTimeKind.Local).AddTicks(9384), 1 },
                    { 2, new DateTime(2024, 10, 11, 10, 58, 19, 183, DateTimeKind.Local).AddTicks(9389), 2 },
                    { 3, new DateTime(2024, 10, 11, 10, 58, 19, 183, DateTimeKind.Local).AddTicks(9392), 1 }
                });

            migrationBuilder.InsertData(
                table: "ProductAdvice",
                columns: new[] { "AdviceId", "ProductId" },
                values: new object[,]
                {
                    { 5, 1 },
                    { 1, 4 },
                    { 4, 4 },
                    { 3, 5 },
                    { 9, 6 },
                    { 10, 7 },
                    { 11, 7 },
                    { 12, 7 },
                    { 13, 8 },
                    { 14, 9 }
                });

            migrationBuilder.InsertData(
                table: "PrescriptionProducts",
                columns: new[] { "PrescriptionId", "ProductId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 4 },
                    { 1, 5 },
                    { 1, 7 }
                });

            migrationBuilder.InsertData(
                table: "PrescriptionProductAdvice",
                columns: new[] { "AdviceId", "PrescriptionId", "ProductId" },
                values: new object[,]
                {
                    { 5, 1, 1 },
                    { 4, 1, 4 },
                    { 3, 1, 5 },
                    { 10, 1, 7 },
                    { 11, 1, 7 },
                    { 12, 1, 7 }
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
