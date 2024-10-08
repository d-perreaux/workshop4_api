﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using api.Data;

#nullable disable

namespace Workshop_API.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20241008202345_SeedProductData")]
    partial class SeedProductData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("api.Models.Advice", b =>
                {
                    b.Property<int?>("AdviceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("AdviceId"));

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AdviceId");

                    b.ToTable("Advice");
                });

            modelBuilder.Entity("api.Models.Pharmacy", b =>
                {
                    b.Property<int>("PharmacyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PharmacyId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PharmacyId");

                    b.ToTable("Pharmacy");

                    b.HasData(
                        new
                        {
                            PharmacyId = 1,
                            Name = "Pharmacie de l'Eurotéléport"
                        },
                        new
                        {
                            PharmacyId = 2,
                            Name = "Pharmacie de la Mitterie"
                        },
                        new
                        {
                            PharmacyId = 3,
                            Name = "Pharmacie de la Vigne"
                        },
                        new
                        {
                            PharmacyId = 4,
                            Name = "Pharmacie Art Nouveau"
                        });
                });

            modelBuilder.Entity("api.Models.Prescription", b =>
                {
                    b.Property<int?>("PrescriptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("PrescriptionId"));

                    b.Property<int>("PharmacyId")
                        .HasColumnType("int");

                    b.HasKey("PrescriptionId");

                    b.HasIndex("PharmacyId");

                    b.ToTable("Prescription");
                });

            modelBuilder.Entity("api.Models.PrescriptionProduct", b =>
                {
                    b.Property<int>("PrescriptionId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("PrescriptionId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("PrescriptionProducts");
                });

            modelBuilder.Entity("api.Models.PrescriptionProductAdvice", b =>
                {
                    b.Property<int>("PrescriptionId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("AdviceId")
                        .HasColumnType("int");

                    b.HasKey("PrescriptionId", "ProductId", "AdviceId");

                    b.HasIndex("AdviceId");

                    b.ToTable("PrescriptionProductAdvice");
                });

            modelBuilder.Entity("api.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<int>("CIP")
                        .HasColumnType("int");

                    b.Property<string>("DCI")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Dosage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("FlagIsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductId");

                    b.ToTable("Product");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            CIP = 1234567,
                            DCI = "Paracetamol",
                            Dosage = "500mg",
                            FlagIsDelete = false,
                            Name = "Doliprane"
                        },
                        new
                        {
                            ProductId = 2,
                            CIP = 2345678,
                            DCI = "Ibuprofen",
                            Dosage = "400mg",
                            FlagIsDelete = false,
                            Name = "Advil"
                        });
                });

            modelBuilder.Entity("api.Models.ProductAdvice", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("AdviceId")
                        .HasColumnType("int");

                    b.HasKey("ProductId", "AdviceId");

                    b.HasIndex("AdviceId");

                    b.ToTable("ProductAdvice");
                });

            modelBuilder.Entity("api.Models.Prescription", b =>
                {
                    b.HasOne("api.Models.Pharmacy", "Pharmacy")
                        .WithMany("Prescriptions")
                        .HasForeignKey("PharmacyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pharmacy");
                });

            modelBuilder.Entity("api.Models.PrescriptionProduct", b =>
                {
                    b.HasOne("api.Models.Prescription", "Prescription")
                        .WithMany("PrescriptionProducts")
                        .HasForeignKey("PrescriptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Models.Product", "Product")
                        .WithMany("PrescriptionProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Prescription");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("api.Models.PrescriptionProductAdvice", b =>
                {
                    b.HasOne("api.Models.Advice", "Advice")
                        .WithMany("PrescriptionProductAdvices")
                        .HasForeignKey("AdviceId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("api.Models.PrescriptionProduct", "PrescriptionProduct")
                        .WithMany("PrescriptionProductAdvices")
                        .HasForeignKey("PrescriptionId", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Advice");

                    b.Navigation("PrescriptionProduct");
                });

            modelBuilder.Entity("api.Models.ProductAdvice", b =>
                {
                    b.HasOne("api.Models.Advice", "Advice")
                        .WithMany("ProductAdvices")
                        .HasForeignKey("AdviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Models.Product", "Product")
                        .WithMany("ProductAdvices")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Advice");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("api.Models.Advice", b =>
                {
                    b.Navigation("PrescriptionProductAdvices");

                    b.Navigation("ProductAdvices");
                });

            modelBuilder.Entity("api.Models.Pharmacy", b =>
                {
                    b.Navigation("Prescriptions");
                });

            modelBuilder.Entity("api.Models.Prescription", b =>
                {
                    b.Navigation("PrescriptionProducts");
                });

            modelBuilder.Entity("api.Models.PrescriptionProduct", b =>
                {
                    b.Navigation("PrescriptionProductAdvices");
                });

            modelBuilder.Entity("api.Models.Product", b =>
                {
                    b.Navigation("PrescriptionProducts");

                    b.Navigation("ProductAdvices");
                });
#pragma warning restore 612, 618
        }
    }
}
