using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    CIP = table.Column<int>(type: "int", nullable: false)
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
                    PharmacyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescription", x => x.PrescriptionId);
                    table.ForeignKey(
                        name: "FK_Prescription_Pharmacy_PharmacyId",
                        column: x => x.PharmacyId,
                        principalTable: "Pharmacy",
                        principalColumn: "PharmacyId",
                        onDelete: ReferentialAction.Cascade);
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
