﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pi_ETicaret_Sitesi.Contexts;

namespace Pi_ETicaret_Sitesi.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("Pi_ETicaret_Sitesi.Entities.Kategori", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ad")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("id");

                    b.ToTable("Kategoriler");
                });

            modelBuilder.Entity("Pi_ETicaret_Sitesi.Entities.Urun", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ad")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("fiyat")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("resim")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("id");

                    b.ToTable("Urunler");
                });

            modelBuilder.Entity("Pi_ETicaret_Sitesi.Entities.UrunKategori", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("kategoriId")
                        .HasColumnType("int");

                    b.Property<int>("urunId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("urunId");

                    b.HasIndex("kategoriId", "urunId")
                        .IsUnique();

                    b.ToTable("UrunKategoriler");
                });

            modelBuilder.Entity("Pi_ETicaret_Sitesi.Entities.UrunKategori", b =>
                {
                    b.HasOne("Pi_ETicaret_Sitesi.Entities.Kategori", "kategori")
                        .WithMany("UrunKategoriler")
                        .HasForeignKey("kategoriId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pi_ETicaret_Sitesi.Entities.Urun", "urun")
                        .WithMany("UrunKategoriler")
                        .HasForeignKey("urunId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("kategori");

                    b.Navigation("urun");
                });

            modelBuilder.Entity("Pi_ETicaret_Sitesi.Entities.Kategori", b =>
                {
                    b.Navigation("UrunKategoriler");
                });

            modelBuilder.Entity("Pi_ETicaret_Sitesi.Entities.Urun", b =>
                {
                    b.Navigation("UrunKategoriler");
                });
#pragma warning restore 612, 618
        }
    }
}
