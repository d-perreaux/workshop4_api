using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using api.Models;
using System.Runtime.Intrinsics.X86;
using Microsoft.Identity.Client.AppConfig;

namespace api.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Pharmacy> Pharmacy { get; set; }

        public DbSet<Product> Product { get; set; }
        public DbSet<Advice> Advice { get; set; }
        public DbSet<Prescription> Prescription { get; set; }
        public DbSet<ProductAdvice> ProductAdvice { get; set; }
        public DbSet<PrescriptionProduct> PrescriptionProducts { get; set; }
        public DbSet<PrescriptionProductAdvice> PrescriptionProductAdvice { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ProductAdvice (many-to-many)
            modelBuilder.Entity<ProductAdvice>().HasKey(pc => new { pc.ProductId, pc.AdviceId });

            modelBuilder.Entity<ProductAdvice>().HasOne(pc => pc.Product).WithMany(pc => pc.ProductAdvices).HasForeignKey(pc => pc.ProductId);

            modelBuilder.Entity<ProductAdvice>().HasOne(pc => pc.Advice).WithMany(a => a.ProductAdvices).HasForeignKey(pc => pc.AdviceId);

            // PrescriptionProduct
            modelBuilder.Entity<PrescriptionProduct>().HasKey(pp => new { pp.PrescriptionId, pp.ProductId });

            modelBuilder.Entity<PrescriptionProduct>().HasOne(pp => pp.Prescription).WithMany(p => p.PrescriptionProducts).HasForeignKey(pp => pp.PrescriptionId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PrescriptionProduct>().HasOne(pp => pp.Product).WithMany(p => p.PrescriptionProducts).HasForeignKey(pp => pp.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

            // PrescriptionProductAdvice
            modelBuilder.Entity<PrescriptionProductAdvice>().HasKey(ppa => new { ppa.PrescriptionId, ppa.ProductId, ppa.AdviceId });

            modelBuilder.Entity<PrescriptionProductAdvice>()
            .HasOne(ppa => ppa.PrescriptionProduct)
            .WithMany(pp => pp.PrescriptionProductAdvices)
            .HasForeignKey(ppa => new { ppa.PrescriptionId, ppa.ProductId })
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PrescriptionProductAdvice>()
                .HasOne(ppa => ppa.Advice)
                .WithMany(a => a.PrescriptionProductAdvices)
                .HasForeignKey(ppa => ppa.AdviceId)
                .OnDelete(DeleteBehavior.Restrict);


            // SEEDING
            modelBuilder.Entity<Pharmacy>().HasData(
                new Pharmacy { PharmacyId = 1, Name = "Pharmacie de l'Eurotéléport" },
                new Pharmacy { PharmacyId = 2, Name = "Pharmacie de la Mitterie" },
                new Pharmacy { PharmacyId = 3, Name = "Pharmacie de la Vigne" },
                new Pharmacy { PharmacyId = 4, Name = "Pharmacie Art Nouveau" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, Name = "Doliprane", CIP = 1234567, DCI = "Paracetamol", Dosage = "500mg", FlagIsDelete = false },
                new Product { ProductId = 2, Name = "Advil", CIP = 2345678, DCI = "Ibuprofen", Dosage = "400mg", FlagIsDelete = false }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}