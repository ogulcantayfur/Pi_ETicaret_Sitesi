using Microsoft.EntityFrameworkCore;
using Pi_ETicaret_Sitesi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pi_ETicaret_Sitesi.Contexts
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=LAPTOP-0SK1I346;Database=PiETicaretSitesi;integrated security=true");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Urun>().HasMany(I => I.UrunKategoriler).WithOne(I => I.urun).HasForeignKey(I => I.urunId);
            modelBuilder.Entity<Kategori>().HasMany(I => I.UrunKategoriler).WithOne(I => I.kategori).HasForeignKey(I => I.kategoriId);

            modelBuilder.Entity<UrunKategori>().HasIndex(I => new
            {
                I.kategoriId,
                I.urunId
            }).IsUnique(); //veri tekrarını önledik.
            base.OnModelCreating(modelBuilder);

        }
        public DbSet<UrunKategori> UrunKategoriler { get; set; }
        public DbSet<Urun> Urunler { get; set; }
        public DbSet<Kategori> Kategoriler { get; set; }


    }
}
